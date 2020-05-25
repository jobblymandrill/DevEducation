using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ElectronicsStore.API.Auth
{
    public class AuthOptions
    {
        const string KEY = "dfjkaflfh454!%dfdf34675";
        public const int LIFETIME = 60; // tokens lifetime 1 hour
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
