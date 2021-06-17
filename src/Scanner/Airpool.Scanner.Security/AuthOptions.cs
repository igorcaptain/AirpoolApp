using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "AirpoolAuthServer"; // token publisher
        public const string AUDIENCE = "AirpoolAuthClient"; // token consumer
        const string KEY = "mysupersecret_secretkey!123"; // key
        public const int LIFETIME = 30; // token lifetime - 15 min
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
