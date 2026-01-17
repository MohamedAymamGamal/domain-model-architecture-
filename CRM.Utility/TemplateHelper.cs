using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Helper
{
    public static class TemplateHelper
    {
        public static string LoadTemplate(string fileName, Dictionary<string, string> values)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", fileName);

            if (!File.Exists(path))
                throw new FileNotFoundException($"Email template not found: {path}");

            string content = File.ReadAllText(path);

            // Replace placeholders like {{FullName}} and {{Code}}
            foreach (var kvp in values)
            {
                content = content.Replace("{{" + kvp.Key + "}}", kvp.Value);
            }

            return content;
        }
    }
}

