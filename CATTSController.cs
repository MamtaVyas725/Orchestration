﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static OrchestrationLayerDemo.Catts;
using Microsoft.Office.Interop.Excel;
using Microsoft.Extensions.Configuration;
using System.Web;

namespace OrchestrationLayerDemo.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CATTSController : ControllerBase
    {
        private readonly IConfiguration _rootObjectCatts;
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

        List<InputDataItem> items = new List<InputDataItem>()
            {
                new InputDataItem{text= "FORMAHEESWOMTST01"},
                new InputDataItem{text ="FORMAHEESWOMTST02" },
                 new InputDataItem{text= "FORMAHEESWOMTST03"},
                new InputDataItem{text ="FORMAHEESWOMTST05" },
            };


        public CATTSController(IConfiguration rootObjectIBASE)
        {
            _rootObjectCatts = rootObjectIBASE;
            _httpclient = new HttpClient();

            InputDataXMLBody = _rootObjectCatts.GetValue<string>("CATTSConfiguration:InputDataXMLBody");
            InputXMLBody = _rootObjectCatts.GetValue<string>("CATTSConfiguration:InputXMLBody");
            ApplicationName = _rootObjectCatts.GetValue<string>("CATTSConfiguration:ApplicationName");
            UserId = _rootObjectCatts.GetValue<string>("CATTSConfiguration:UserId");
            SubOperationName = _rootObjectCatts.GetValue<string>("CATTSConfiguration:SubOperationName");
            TraceLevel = _rootObjectCatts.GetValue<string>("CATTSConfiguration:TraceLevel");
            TraceType = _rootObjectCatts.GetValue<string>("CATTSConfiguration:TraceType");
            InputType = _rootObjectCatts.GetValue<string>("CATTSConfiguration:InputType");
            AdditionalReports = _rootObjectCatts.GetValue<string>("CATTSConfiguration:AdditionalReports");

        }

        [HttpGet]
       // [Route("GetInputPayloadData")]
        public IEnumerable<string> GetInputPayloadData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var r in items)
            {
                //  <InputDataItem type='VID'>82GE360604475</InputDataItem>
                var values = string.Format(InputDataXMLBody.ToString(), InputType, r.text.ToString());
                sb.Append(values);
            }
            var CattsTcaServiceRequest = sb.ToString();
            var FinalCattsTcaServiceRequest = string.Format(InputXMLBody.ToString(), ApplicationName, UserId, SubOperationName, TraceLevel, TraceType, InputType, AdditionalReports, CattsTcaServiceRequest.ToString());
            return new string[] { FinalCattsTcaServiceRequest.Trim() };
        }



        [HttpGet]
        [Route("GetCATTSAsyncData")]
        public async Task<IEnumerable<CattsTcaServiceObjMain>> GetCATTSAsyncData()
        {
            IEnumerable<string> data = GetInputPayloadData();
            var byteArray = data.SelectMany(s => Encoding.UTF8.GetBytes(s)).ToArray();
            MemoryStream stream = new MemoryStream();
            stream.Write(byteArray, 0, byteArray.Length);
            //using (StreamReader r = new StreamReader(@"C:\poc\CATTS\inputxm.json"))
            //{
                XmlDocument doc = new XmlDocument();
                DateTime start = DateTime.Now;
                Auth auth = new Auth();
                if (stream.Position > 0)
                {
                    stream.Position = 0;
                }

                doc.Load(stream);

                string jsonString11 = JsonConvert.SerializeXmlNode(doc);
                var newJson = jsonString11.Replace("@", "").Replace("#", "");
                var result = JsonConvert.DeserializeObject<CattsTcaServiceObjMain>(newJson);


                foreach (XmlNode node in doc.SelectSingleNode("//InputData/InputDataItem"))
                {
                    node.ParentNode.RemoveChild(node);
                }

                Token token = await auth.GetElibilityToken();

                _httpclient.DefaultRequestHeaders.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpclient.DefaultRequestHeaders.Add("Authorization", ("Bearer " + token.AccessToken));
                _httpclient.DefaultRequestHeaders.ConnectionClose = true;

                var batchSize = 200;
                int numberOfBatches = (int)Math.Ceiling((double)result.CattsTcaServiceRequest.InputData.InputDataItem.Count() / batchSize);
                var tasks = new List<Task<IEnumerable<CattsTcaServiceObjMain>>>();

                for (int i = 0; i < numberOfBatches; i++)
                {
                    var currentSearilNo = result.CattsTcaServiceRequest.InputData.InputDataItem.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                    tasks.Add(GetCattsBatchWiseData(doc, currentSearilNo, start));

                }

                return (await Task.WhenAll(tasks)).SelectMany(u => u);
           // }
        }


        public async Task<IEnumerable<CattsTcaServiceObjMain>> GetCattsBatchWiseData(XmlDocument doc, List<InputDataItem> tasks, DateTime start)
        {
            // Creating a file
            string myfile = @"C:\poc\Output.xml";

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

                var users = JsonConvert.DeserializeObject<IEnumerable<CattsTcaServiceObjMain>>(resultContent).ToList();
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
