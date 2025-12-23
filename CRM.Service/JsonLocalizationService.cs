using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM.API.Service
{
    public class JsonLocalizationService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _languages;
        private readonly IHttpContextAccessor _context;

        public JsonLocalizationService(IHttpContextAccessor context)
        {
            _context = context;
            _languages = LoadLanguages();
        }

        private Dictionary<string, object> ParseFile(string path)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(path))!;
        }

        private Dictionary<string, string> FlattenJson(Dictionary<string, object> nested, string parentKey = "")
        {
            var result = new Dictionary<string, string>();

            foreach (var item in nested)
            {
                string key = string.IsNullOrEmpty(parentKey)
                    ? item.Key
                    : $"{parentKey}.{item.Key}";

                if (item.Value is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.Object)
                    {
                        var child = JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText())!;
                        foreach (var kv in FlattenJson(child, key))
                            result[kv.Key] = kv.Value;
                    }
                    else
                    {
                        result[key] = element.ToString()!;
                    }
                }
            }

            return result;
        }

        private Dictionary<string, Dictionary<string, string>> LoadLanguages()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Localization");

            return Directory
                .GetFiles(path, "*.json")
                .ToDictionary(
                    file => Path.GetFileNameWithoutExtension(file),
                    file =>
                    {
                        var raw = ParseFile(file);
                        return FlattenJson(raw);
                    }
                );
        }

        public string localize(string key)
        {
            var lang = _context.HttpContext?.Request.Headers["Accept-Language"].ToString() ?? "en";

            if (!_languages.ContainsKey(lang))
                lang = "en";

            return _languages[lang].ContainsKey(key)
                ? _languages[lang][key]
                : key;
        }
    }

}