using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.ApplicaitionModels
{
    public interface AuthenticationTokens
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsRefreshTokenValid { get; set; }
    }
}
