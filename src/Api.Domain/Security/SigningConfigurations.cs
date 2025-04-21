using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public SigningConfigurations()
        {
            // using (var provider = new RSACryptoServiceProvider(2048))
            // {
            //     Key = new RsaSecurityKey(provider.ExportParameters(true));
            // }
            
            // SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

            var secret = "chave-super-secreta-para-testes-1234567890"; // pode ser lida de env também
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256); // <- IMPORTANTE
        }
    }
}
