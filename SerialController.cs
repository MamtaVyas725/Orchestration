using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrchestrationDemo.Controllers
{
    //[ApiController]
    // [Route("api/[controller]")]
    public class SerialController : ControllerBase
    {
        public HttpClient _httpclient;
        public List<string> links = new List<string>();
        private RootObject currentSearilNo;
        public SerialController()
        {
            _httpclient = new HttpClient();
        }
        [HttpGet]
        [Route("GetAsyncData")]
        public async Task<IEnumerable<RootObject>> GetAsyncData()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\mamta.b.vyas\Desktop\poc\input500.json"))
            {
                //  var watch = System.Diagnostics.Stopwatch.StartNew();
                // Creating a file
                string jsonData = r.ReadToEnd();
                var webclient = new WebClient();
                var jsonString = webclient.DownloadString(@"C:\Users\mamta.b.vyas\Desktop\poc\input500.json");
                var result = JsonConvert.DeserializeObject<RootObject>(jsonString);
                Token token = await GetElibilityToken();
               
                _httpclient.DefaultRequestHeaders.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpclient.DefaultRequestHeaders.Add("Authorization", ("Bearer " + token.AccessToken));
                _httpclient.DefaultRequestHeaders.ConnectionClose = true;

                var batchSize = 20;
                int numberOfBatches = (int)Math.Ceiling((double)result.Input.Count() / batchSize);
                var tasks = new List<Task<IEnumerable<RootObject>>>();
               
                for (int i = 0; i < numberOfBatches; i++)
                {
                     var currentSearilNo1 =result.Input.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                     tasks.Add(GetUsers(currentSearilNo1));

                }

                return (await Task.WhenAll(tasks)).SelectMany(u => u);
            }
        }


        public async Task<IEnumerable<RootObject>> GetUsers(List<Person> tasks)
        {
            // Creating a file
            string myfile = @"C:\Users\mamta.b.vyas\Desktop\poc\Output.json";

            var watch = new Stopwatch();
            watch.Start();

            RootObject wrapper = new RootObject { Input = tasks };
            string json = JsonConvert.SerializeObject(wrapper);

         
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpclient
             .PostAsync(
                 "https://apis-sandbox.intel.com/sc/warranty-entitlement/v1/warranty-details",
                 byteContent)
             .ConfigureAwait(false);

           var resultContent = response.Content.ReadAsStringAsync().Result;
            
            //// Appending the given texts
            using (StreamWriter sw = System.IO.File.AppendText(myfile))
            {
                // sw.WriteLine("actual processing " + elapsedMs);
                sw.WriteLine(Environment.NewLine);
                sw.WriteLine(resultContent);
                watch.Stop();
                sw.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }

            var users = JsonConvert.DeserializeObject<IEnumerable<RootObject>>(JsonConvert.SerializeObject(resultContent)).ToList();
            return users;
        }


        [HttpGet]
        [Route("GetElibilityToken")]
        public async Task<Token> GetElibilityToken()
        {
            HttpClient client = new HttpClient();
            string baseAddress = @"https://apis-sandbox.intel.com/v1/auth/token";

            string grant_type = "client_credentials";
            string client_id = "1033a1f3-6808-4b85-91a8-1f7e69fecea7";
            string client_secret = "r3e8Q~7SoNCuh1CO1FXbHMcl5~4a~JHLCwYkAbES";

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

        public class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }




    }
}
