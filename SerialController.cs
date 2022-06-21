using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrchestrationLayerDemo;
using OrchestrationLayerDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
using System.Xml;
using static OrchestrationLayerDemo.Controllers.Item;

namespace OrchestrationDemo.Controllers
{
    //[ApiController]
    // [Route("api/[controller]")]
    public class SerialController : ControllerBase
    {
        private readonly IConfiguration _rootObjectIBASE;
        public HttpClient _httpclient;
        public string InputDataXMLBody = string.Empty;
        public string InputXMLBody = string.Empty;
        public string ApplicationName = string.Empty;
        public string UserId = string.Empty;
        public string SubOperationName = string.Empty;
        public string TraceLevel = string.Empty;
        public string TraceType = string.Empty;
        public string InputType = string.Empty;
        public string AdditionalReports = string.Empty;
        string grant_type = "";
        string client_id = "";
        string client_secret = "";
        string InputDataJsonBody = string.Empty;
        string InputJsonBody = string.Empty;

        List<Person> listOfSerialNumber = new List<Person>()
            {
                new Person{SerialNumber= "FORMAHEESWOMTST01"},
                new Person{SerialNumber ="FORMAHEESWOMTST02" },
                new Person{SerialNumber= "FORMAHEESWOMTST03"},
                new Person{SerialNumber ="FORMAHEESWOMTST05" },
            };

        public SerialController(IConfiguration rootObjectIBASE)
        {
            _rootObjectIBASE = rootObjectIBASE;
            _httpclient = new HttpClient();
            grant_type = _rootObjectIBASE.GetValue<string>("ClientCredentails:grant_type");
            client_id = _rootObjectIBASE.GetValue<string>("ClientCredentails:client_id");
            client_secret = _rootObjectIBASE.GetValue<string>("ClientCredentails:client_secret");
            InputDataJsonBody = _rootObjectIBASE.GetValue<string>("IBASEConfiguration:InputJsonBody");
            InputJsonBody = _rootObjectIBASE.GetValue<string>("IBASEConfiguration:Input");

        }


        [HttpGet]
        //[Route("GetInutPayloadIBASEData")]
        public IEnumerable<string> GetInutPayloadIBASEData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var r in listOfSerialNumber)
            {
                //  { "SerialNumber": "BQF913400183" }
                var values = string.Format(InputDataJsonBody.ToString(), r.SerialNumber.ToString());
                sb.AppendLine("{" + values + "}");
                sb.Append(",");
            }
            var InputPayload = "[" + sb.ToString().TrimEnd(',') + "]";

            var FinalInputPayload = "{" + string.Format(InputJsonBody.ToString(), InputPayload.ToString()) + "}";

            return new string[] { FinalInputPayload };
        }


        [HttpGet]
        [Route("GetAsyncData")]
        public async Task<IEnumerable<RootObject>> GetAsyncData()
        {
            //using (StreamReader r = new StreamReader(@"C:\poc\input10.json"))
            //{
            IEnumerable<string> data = GetInutPayloadIBASEData();
            DateTime start = DateTime.Now;
            Auth auth = new Auth();

            // string jsonData = r.ReadToEnd();
            // var webclient = new WebClient();
            // var jsonString = webclient.DownloadString(@"C:\poc\input10.json");

            var result = JsonConvert.DeserializeObject<RootObject>(((string[])data)[0].ToString());
            Token token = await auth.GetElibilityToken(grant_type, client_id, client_secret);

            _httpclient.DefaultRequestHeaders.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Add("Authorization", ("Bearer " + token.AccessToken));
            _httpclient.DefaultRequestHeaders.ConnectionClose = true;

            var batchSize = 5;
            int numberOfBatches = (int)Math.Ceiling((double)result.Input.Count() / batchSize);
            var tasks = new List<Task<IEnumerable<RootObject>>>();

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentSearilNo1 = result.Input.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                tasks.Add(GetUsers(currentSearilNo1, start));
            }
            return (await Task.WhenAll(tasks)).SelectMany(u => u);
            // }
        }


        public async Task<IEnumerable<RootObject>> GetUsers(List<Person> tasks, DateTime start)
        {
            // Creating a file
            string myfile = @"C:\poc\Output.json";

            //var watch = new Stopwatch();
            //watch.Start();

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
            var JSONData = JsonConvert.DeserializeObject(resultContent).ToString();

            //var webclient = new WebClient();
            //var jsonString = webclient.DownloadString(@"C:\poc\Output.json");
            //var result11 = JsonConvert.DeserializeObject<RootObject>(JSONData);


            ////// Appending the given texts
            using (StreamWriter sw = System.IO.File.AppendText(myfile))
            {
                // sw.WriteLine("actual processing " + elapsedMs);
                sw.WriteLine(Environment.NewLine);
                sw.WriteLine(resultContent);
                // watch.Stop();
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                sw.WriteLine("Elapsed Time is {0} ms", ts.TotalSeconds);
            }

            var users = JsonConvert.DeserializeObject<IEnumerable<RootObject>>(resultContent).ToList();
            return users.ToList();
        }
    }
}