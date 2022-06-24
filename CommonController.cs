using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static OrchestrationLayerDemo.Catts;
using static OrchestrationLayerDemo.Controllers.Item;

namespace OrchestrationLayerDemo.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration _rootObjectCommon;
        public HttpClient _httpclient;
        string grant_type = "";
        string client_id = "";
        string client_secret = "";
        string InputDataJsonBody = string.Empty;
        string InputJsonBody = string.Empty;
        public string InputDataXMLBody = string.Empty;
        public string InputXMLBody = string.Empty;
        public string ApplicationName = string.Empty;
        public string UserId = string.Empty;
        public string SubOperationName = string.Empty;
        public string TraceLevel = string.Empty;
        public string TraceType = string.Empty;
        public string InputType = string.Empty;
        public string AdditionalReports = string.Empty;
        string InputDataJsonBodyIRC = string.Empty;
        string InputJsonBodyIRC = string.Empty;

        public CommonController(IConfiguration rootObjectCommon)
        {
            _rootObjectCommon = rootObjectCommon;
            _httpclient = new HttpClient();
            grant_type = _rootObjectCommon.GetValue<string>("ClientCredentails:grant_type");
            client_id = _rootObjectCommon.GetValue<string>("ClientCredentails:client_id");
            client_secret = _rootObjectCommon.GetValue<string>("ClientCredentails:client_secret");
            //IBASE
            InputDataJsonBody = _rootObjectCommon.GetValue<string>("IBASEConfiguration:InputJsonBody");
            InputJsonBody = _rootObjectCommon.GetValue<string>("IBASEConfiguration:Input");

            //CATTS
            InputDataXMLBody = _rootObjectCommon.GetValue<string>("CATTSConfiguration:InputDataXMLBody");
            InputXMLBody = _rootObjectCommon.GetValue<string>("CATTSConfiguration:InputXMLBody");
            ApplicationName = _rootObjectCommon.GetValue<string>("CATTSConfiguration:ApplicationName");
            UserId = _rootObjectCommon.GetValue<string>("CATTSConfiguration:UserId");
            SubOperationName = _rootObjectCommon.GetValue<string>("CATTSConfiguration:SubOperationName");
            TraceLevel = _rootObjectCommon.GetValue<string>("CATTSConfiguration:TraceLevel");
            TraceType = _rootObjectCommon.GetValue<string>("CATTSConfiguration:TraceType");
            InputType = _rootObjectCommon.GetValue<string>("CATTSConfiguration:InputType");
            AdditionalReports = _rootObjectCommon.GetValue<string>("CATTSConfiguration:AdditionalReports");

            //IRC
            InputDataJsonBodyIRC = _rootObjectCommon.GetValue<string>("IRCConfiguration:InputJsonBody");
            InputJsonBodyIRC = _rootObjectCommon.GetValue<string>("IRCConfiguration:Input");
        }

        [HttpGet]
        [Route("GetInputPayloadDataForCATTS")]
        public IEnumerable<string> GetInputPayloadDataForCATTS(List<string> SearialNumberList)
        {
            //  var logFile = System.IO.File.ReadAllLines(@"C:\poc\catts.txt");
            //  var VidList = new List<string>(logFile);
            StringBuilder sb = new StringBuilder();
            foreach (var r in SearialNumberList)
            {
                //  <InputDataItem type='VID'>82GE360604475</InputDataItem>
                var values = string.Format(InputDataXMLBody.ToString(), InputType, r.ToString());
                sb.Append(values);
            }
            var CattsTcaServiceRequest = sb.ToString();
            var FinalCattsTcaServiceRequest = string.Format(InputXMLBody.ToString(), ApplicationName, UserId, SubOperationName, TraceLevel, TraceType, InputType, AdditionalReports, CattsTcaServiceRequest.ToString());
            return new string[] { FinalCattsTcaServiceRequest.Trim() };
        }
       
        [HttpGet]
        [Route("GetInputPayloadDataForIBASE")]
        public IEnumerable<string> GetInputPayloadDataForIBASE(List<string> SearialNumberList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var r in SearialNumberList)
            {
                //  { "SerialNumber": "BQF913400183" }
                var values = string.Format(InputDataJsonBody.ToString(), r.ToString());
                sb.AppendLine("{" + values + "}");
                sb.Append(",");
            }
            var InputPayload = "[" + sb.ToString().TrimEnd(',') + "]";
            var FinalInputPayload = "{" + string.Format(InputJsonBody.ToString(), InputPayload.ToString()) + "}";
            return new string[] { FinalInputPayload };
        }


        [HttpGet]
        [Route("GetInputPayloadDataForIRC")]
        public IEnumerable<string> GetInputPayloadDataForIRC(List<string> SearialNumberList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var r in SearialNumberList)
            {
                var values = string.Format(InputDataJsonBodyIRC.ToString(), r.ToString());
                sb.AppendLine("{" + values + "}");
                sb.Append(",");
            }
            var InputPayload = "[" + sb.ToString().TrimEnd(',') + "]";
            var FinalInputPayload = "{" + string.Format(InputJsonBodyIRC.ToString(), InputPayload.ToString()) + "}";
            return new string[] { FinalInputPayload };
        }


        [HttpGet]
        [Route("GetAsyncData")]
        public async Task<string> GetAsyncData()
        {
            DateTime start = DateTime.Now;
            Auth auth = new Auth();
            Token token = await auth.GetElibilityToken(grant_type, client_id, client_secret);

            _httpclient.DefaultRequestHeaders.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Add("Authorization", ("Bearer " + token.AccessToken));
            _httpclient.DefaultRequestHeaders.ConnectionClose = true;

            //Creating input payload for all process
            var InputDataFile = System.IO.File.ReadAllLines(@"C:\poc\SerialNumber.txt"); //IBASE
            //var InputDataFile = System.IO.File.ReadAllLines(@"C:\poc\CATTS_IRC.txt");
            var SearialNumberList = new List<string>(InputDataFile);

            IEnumerable<string> inputPayloadCATTS = GetInputPayloadDataForCATTS(SearialNumberList);
            IEnumerable<string> inputPayloadIBASE = GetInputPayloadDataForIBASE(SearialNumberList);
            IEnumerable<string> inputPayloadIRC = GetInputPayloadDataForIRC(SearialNumberList);

            //CATTS
            XmlDocument docXMLForCATTS = new XmlDocument();
            var byteArray = inputPayloadCATTS.SelectMany(s => Encoding.UTF8.GetBytes(s)).ToArray();
            MemoryStream stream = new MemoryStream();
            stream.Write(byteArray, 0, byteArray.Length);
            if (stream.Position > 0)
            {
                stream.Position = 0;
            }
            docXMLForCATTS.Load(stream);
            string CattsjsonString = JsonConvert.SerializeXmlNode(docXMLForCATTS);
            var newJson = CattsjsonString.Replace("@", "").Replace("#", "");

            foreach (XmlNode node in docXMLForCATTS.SelectNodes("//InputData/InputDataItem"))
            {
                node.ParentNode.RemoveChild(node);
            }

            //IBASE
            var resultObjIBASE = JsonConvert.DeserializeObject<RootObject>(((string[])inputPayloadIBASE)[0].ToString());
            //CATTS
            var resultObjCATTS = JsonConvert.DeserializeObject<CattsTcaServiceObjMain>(newJson);
            //IRC
            var resultObjIRC = JsonConvert.DeserializeObject<IRCRootObject>(((string[])inputPayloadIRC)[0].ToString());


            var batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)resultObjIBASE.Input.Count() / batchSize);

            var tasksRootObjectIBASE = new List<Task<IEnumerable<RootObjectWarrantyOutputJSON>>>();
            var tasksCattsTcaServiceObjMain = new List<Task<IEnumerable<ResultCATT>>>();
            var tasksIRCServiceObjMain = new List<Task<IEnumerable<ReturnJSON>>>();

            for (int i = 0; i < numberOfBatches; i++)
            {
                //IBASE
                var currentSearilNo = resultObjIBASE.Input.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                tasksRootObjectIBASE.Add(GetIBASEBatchWiseData(currentSearilNo, start));

                //CATTS
                var currentVIDNo = resultObjCATTS.CattsTcaServiceRequest.InputData.InputDataItem.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                tasksCattsTcaServiceObjMain.Add(GetCattsBatchWiseData(docXMLForCATTS, currentVIDNo, start));

                //IRC
                var currentIRCVIDNo = resultObjIRC.cpuVids.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                tasksIRCServiceObjMain.Add(GetIRCBatchWiseData(currentIRCVIDNo, start));

                //IF STATUS IS NF FROM IBASE ignore all other
                //next s
            }
            (await Task.WhenAll(tasksRootObjectIBASE)).SelectMany(u => u);
            (await Task.WhenAll(tasksCattsTcaServiceObjMain)).SelectMany(u => u);
            (await Task.WhenAll(tasksIRCServiceObjMain)).SelectMany(u => u);
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);

            //return (await Task.WhenAll(tasks)).SelectMany(u => u);
            /// System.IO.File.AppendAllText(@"C:\poc\OutputCATTS.xml", ts.Seconds.ToString() + Environment.NewLine);
            //  System.IO.File.AppendAllText(@"C:\poc\OutputIBASE.json", ts.Seconds + Environment.NewLine);
            //  System.IO.File.AppendAllText(@"C:\poc\OutputIRC.json", ts.Seconds + Environment.NewLine);
            return "Total Time taken :" + ts.Seconds.ToString();
        }

        [HttpGet]
        [Route("GetCattsBatchWiseData")]
        public async Task<IEnumerable<ResultCATT>> GetCattsBatchWiseData(XmlDocument doc, List<InputDataItem> tasks, DateTime start)
        {
            // Creating a file
            string myfile = @"C:\poc\OutputCATTS.xml";

            try
            {

                string xmlString = "";
                XmlSerializer xmlSerializer = new XmlSerializer(tasks.GetType());
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    xmlSerializer.Serialize(memoryStream, tasks, ns);
                    memoryStream.Position = 0;
                    xmlString = new StreamReader(memoryStream).ReadToEnd();
                    var newXmlString = xmlString.Replace("<?xml version=\"1.0\"?>", "").Replace("<ArrayOfInputDataItem>", "").Replace("</ArrayOfInputDataItem>", "").Replace("\r\n", "").Trim();
                    XmlDocumentFragment xfrag = doc.CreateDocumentFragment();
                    xfrag.InnerXml = newXmlString.Trim();
                    doc.DocumentElement.LastChild.AppendChild(xfrag);
                }

                var httpContent = new StringContent(doc.OuterXml.Trim().ToString(), Encoding.UTF8, "text/xml");

                var response = await _httpclient
                 .PostAsync(
                     "https://apis-sandbox.intel.com/mis/v1/catts-tca-service",
                     httpContent)
                 .ConfigureAwait(false);

                var resultContent = response.Content.ReadAsStringAsync().Result;
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
              
                WriteTextAsync(myfile, resultContent);


                //////// Appending the given texts
                //await  using (StreamWriter sw = System.IO.File.AppendText(myfile))
                //{

                //    // sw.WriteLine("actual processing " + elapsedMs);
                //    sw.WriteLine(Environment.NewLine);
                //    sw.WriteLine(resultContent);
                //    // watch.Stop();
                //    DateTime end = DateTime.Now;
                //    TimeSpan ts = (end - start);
                //    // sw.WriteLine("Elapsed Time is {0} ms", ts.TotalSeconds);
                //     sw.Close();
                //}
                var Json1 = XmlToJson(resultContent);
                var newJson = Json1.Replace("Transaction Details\":{", "TransactionDetails\":[{").Replace("ErrorMessage\":null}", "ErrorMessage\":null}]").Replace("Transaction Details", "TransactionDetails").Replace("Input Report", "InputReport").Replace("Box Report", "BoxReport").Replace("Unit Trace", "UnitTrace").Replace("Batch Report", "BatchReport");
                var CATTData = JsonConvert.DeserializeObject<ResultCATT>(newJson);
                List<ResultCATT> objjsonCatts = new List<ResultCATT>();
                objjsonCatts.Add(CATTData);
               // var users = JsonConvert.DeserializeObject<IEnumerable<ResultCATT>>(JsonConvert.SerializeObject(resultContent)).ToList();
               return objjsonCatts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("WriteTextAsync")]
        static async Task WriteTextAsync(string filePath, string text)
        {
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }

        [HttpGet]
        [Route("GetIBASEBatchWiseData")]
        public async Task<IEnumerable<RootObjectWarrantyOutputJSON>> GetIBASEBatchWiseData(List<Person> tasks, DateTime start)
        {
            // Creating a file
            string myfile = @"C:\poc\OutputIBASE.json";

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

            ////// Appending the given texts
            try
            {
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                WriteTextAsync(myfile, resultContent);
             //   System.IO.File.AppendAllText(myfile,ts  + Environment.NewLine);
                //using (StreamWriter sw = new StreamWriter(myfile, true))
                ////using (StreamWriter sw = System.IO.File.AppendText(myfile))
                //{
                //    // sw.WriteLine("actual processing " + elapsedMs);
                //    sw.WriteLine(Environment.NewLine);
                //    sw.WriteLine(resultContent);
                //    // watch.Stop();
                //    DateTime end = DateTime.Now;
                //    TimeSpan ts = (end - start);
                //    sw.WriteLine("Elapsed Time is {0} ms", ts.TotalSeconds);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var users = JsonConvert.DeserializeObject<RootObjectWarrantyOutputJSON>(resultContent);
            List<RootObjectWarrantyOutputJSON> objjson = new List<RootObjectWarrantyOutputJSON>();
            objjson.Add(users);
        //    var users = JsonConvert.DeserializeObject<IEnumerable<RootObjectWarrantyOutputJSON>>(JsonConvert.SerializeObject(resultContent)).ToList();
           return objjson;
        }


        [HttpGet]
        [Route("GetIRCBatchWiseData")]
        public async Task<IEnumerable<ReturnJSON>> GetIRCBatchWiseData(List<IRCItem> tasks, DateTime start)
        {
            // Creating a file
            string myfile = @"C:\poc\OutputIRC.json";

            IRCRootObject wrapper = new IRCRootObject { cpuVids = tasks };
            string json = JsonConvert.SerializeObject(wrapper);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpclient
             .PatchAsync(
                 "https://apis-sandbox.intel.com/sc/v1/extended-warranty/claims",
                 byteContent)
             .ConfigureAwait(false);

            var resultContent = response.Content.ReadAsStringAsync().Result;
            WriteTextAsync(myfile, resultContent);
            var users = JsonConvert.DeserializeObject<ReturnJSON>(resultContent);
            ////// Appending the given texts
            List<ReturnJSON> objjson = new List<ReturnJSON>();
            objjson.Add(users);
            return objjson;
        }


        [HttpGet]
        [Route("XmlToJson")]
        public string XmlToJson(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
    }
}
