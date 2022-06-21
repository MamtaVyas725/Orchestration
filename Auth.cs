using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrchestrationLayerDemo
{
    public class Auth
    {
        private readonly IConfiguration _ClientSecrets;
    
        public Auth()
        {
        }
        public Auth(IConfiguration ClientSecrets)
        {
            _ClientSecrets = ClientSecrets;
        }

        [HttpGet]
        [Route("GetElibilityToken")]
        public async Task<Token> GetElibilityToken(string grant_type, string client_id, string client_secret)
        {
         
            HttpClient client = new HttpClient();
            string baseAddress = @"https://apis-sandbox.intel.com/v1/auth/token";
            //string grant_type = _ClientSecrets.GetValue<string>("ClientCredentails:grant_type");
            //string client_id = _ClientSecrets.GetValue<string>("CATTSConfiguration:client_id");
            //string client_secret = _ClientSecrets.GetValue<string>("CATTSConfiguration:client_secret");

            var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                };

            HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
            return tok;
        }

    }
}
