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
        public string InputDataJsonBodyIRC = string.Empty;
        public string InputJsonBodyIRC = string.Empty;
        public string InputSNJson = string.Empty;
        public string InputSNJsonFirst = string.Empty;
        public string InputSNJsonSecond = string.Empty;
        public string Batch_no = string.Empty;
        public string SN = string.Empty;
        public string Site = string.Empty;
        public string Type = string.Empty;
        public string Fcst_prd_nm = string.Empty;
        public string Work_Order = string.Empty;
        public string Cust_sn = string.Empty;
        public string BUILD_DATE = string.Empty;
        public string Coo_flag = string.Empty;
        public string Mat_id = string.Empty;
        public string Version = string.Empty;
        public string Carton_ID = string.Empty;
        public string Pallet_ID = string.Empty;
        public string Firmware = string.Empty;
        public string Receipt_ID = string.Empty;
        public string intel_pn = string.Empty;
        public string vendor = string.Empty;
        public string mfg_pn = string.Empty;
        public string PART_DESC = string.Empty;
        public string SN_FLAG = string.Empty;
        public string SYSTEM_ASSEMBLY_SITE = string.Empty;
        public string PART_NO = string.Empty;
        public string DATE_CODE = string.Empty;
        public string LOT_CODE = string.Empty;
        public string VENDOR_CODE = string.Empty;
        public string VENDOR_NAME = string.Empty;
        public string PPID = string.Empty;
        public string PART = string.Empty;
        public string LOCATION = string.Empty;
        public string MAKER_DESC = string.Empty;
        public string MAKER_PART_NO = string.Empty;
        public string BOARD_ASSEMBLY_SITE = string.Empty;
        public string REEL_CODE = string.Empty;
        public string SLOT = string.Empty;
        public string UNITQTY = string.Empty;
        public string CUSTOMER_PN = string.Empty;
        public string PPPN = string.Empty;
        public string Online_Offline = string.Empty;
        public string PROGRAM_STATUS = string.Empty;
        public string PROGRAM_TIME = string.Empty;
        public string CHECKSUM = string.Empty;
        public string PROGRAMED_FILE_NAME = string.Empty;
        public string PROGRAM_LOCATION = string.Empty;
        public string dlv_doc_id = string.Empty;
        public string lineitem = string.Empty;
        public string shipsite = string.Empty;
        public string shipdate = string.Empty;
        public string trackingnumber = string.Empty;
        public string sls_ord_id = string.Empty;
        public string Sales_Org = string.Empty;
        public string Dist_Channel = string.Empty;
        public string soldto_id = string.Empty;
        public string shipto_id = string.Empty;
        public string Ship_To_Country = string.Empty;
        public string Last_Update_Date_Time = string.Empty;
        public string Product_Operation_Code = string.Empty;
        public string Base_Product_Name = string.Empty;
        public string Sales_Document_Type_Cd = string.Empty;
        public string Sales_Order_Reason_Code = string.Empty;
        public string Family_Code = string.Empty;
        public string SBS_Code = string.Empty;
        public string Product_Division_Code = string.Empty;
        public string Group_Code = string.Empty;
        public string Material_Item_Type_Code = string.Empty;
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

            //SNTRACE
            InputSNJson = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:InputSNJson");
            InputSNJsonFirst = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:InputSNJsonFirst");
            InputSNJsonSecond = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:InputSNJsonSecond");
            Batch_no = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Batch_no");
            SN = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:SN");
            Site = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Site");
            Type = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Type");
            Fcst_prd_nm = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Fcst_prd_nm");
            Work_Order = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Work_Order");
            Cust_sn = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Cust_sn");
            BUILD_DATE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:BUILD_DATE");
            Coo_flag = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Coo_flag");
            Mat_id = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Mat_id");
            Version = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Version");
            Carton_ID = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Carton_ID");
            Pallet_ID = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Pallet_ID");
            Firmware = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Firmware");
            Receipt_ID = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Receipt_ID");
            intel_pn = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:intel_pn");
            vendor = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:vendor");
            mfg_pn = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:mfg_pn");
            PART_DESC = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PART_DESC");
            SN_FLAG = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:SN_FLAG");
            SYSTEM_ASSEMBLY_SITE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:SYSTEM_ASSEMBLY_SITE");
            PART_NO = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PART_NO");
            DATE_CODE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:DATE_CODE");
            LOT_CODE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:LOT_CODE");
            VENDOR_CODE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:VENDOR_CODE");
            VENDOR_NAME = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:VENDOR_NAME");
            PPID = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PPID");
            PART = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PART");
            LOCATION = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:MAKER_DESC");
            MAKER_DESC = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:VENDOR_NAME");
            MAKER_PART_NO = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:MAKER_PART_NO");
            BOARD_ASSEMBLY_SITE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:BOARD_ASSEMBLY_SITE");
            REEL_CODE = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:REEL_CODE");
            SLOT = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:SLOT");
            UNITQTY = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:UNITQTY");
            CUSTOMER_PN = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:CUSTOMER_PN");
            PPPN = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PPPN");
            Online_Offline = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Online_Offline");
            PROGRAM_STATUS = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PROGRAM_STATUS");
            PROGRAM_TIME = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PROGRAM_TIME");
            CHECKSUM = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:CHECKSUM");
            PROGRAMED_FILE_NAME = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PROGRAMED_FILE_NAME");
            PROGRAM_LOCATION = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:PROGRAM_LOCATION");
            dlv_doc_id = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:dlv_doc_id");
            lineitem = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:lineitem");
            shipsite = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:shipsite");
            shipdate = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:shipdate");
            trackingnumber = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:trackingnumber");
            sls_ord_id = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:sls_ord_id");
            Sales_Org = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Sales_Org");
            Dist_Channel = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Dist_Channel");
            soldto_id = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:soldto_id");
            shipto_id = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:shipto_id");
            Ship_To_Country = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Ship_To_Country");
            Last_Update_Date_Time = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Last_Update_Date_Time");
            Product_Operation_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Product_Operation_Code");
            Base_Product_Name = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Base_Product_Name");
            Sales_Document_Type_Cd = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Sales_Document_Type_Cd");
            Sales_Order_Reason_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Sales_Order_Reason_Code");
            Family_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Family_Code");
            SBS_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:SBS_Code");
            Product_Division_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Product_Division_Code");
            Group_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Group_Code");
            Material_Item_Type_Code = _rootObjectCommon.GetValue<string>("SNTRACEConfiguration:Material_Item_Type_Code");
        }

        public CommonController()
        {
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
        [Route("GetInputPayloadDataForSNTRACE")]
        public IEnumerable<string> GetInputPayloadDataForSNTRACE(List<string> SearialNumberList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var r in SearialNumberList)
            {
                var values = string.Format(InputSNJsonFirst.ToString(), r.ToString());
                sb.AppendLine(values);
                sb.Append(",");
            }
            var InputPayloadFirst = "[" + sb.ToString().TrimEnd(',') + "]";
            var InputPayload = "{" + string.Format(InputSNJson.ToString(), InputPayloadFirst.ToString());
            var InputPayloadSecond = string.Format(InputSNJsonSecond.ToString(), Batch_no, SN, Site, Type, Fcst_prd_nm, Work_Order, Cust_sn, BUILD_DATE, Coo_flag, Mat_id, Version, Carton_ID, Pallet_ID, Firmware, Receipt_ID, intel_pn, vendor, mfg_pn, PART_DESC, SN_FLAG, SYSTEM_ASSEMBLY_SITE, PART_NO, DATE_CODE, LOT_CODE, VENDOR_CODE, VENDOR_NAME, PPID, PART, LOCATION, MAKER_DESC, MAKER_PART_NO, BOARD_ASSEMBLY_SITE, REEL_CODE, SLOT, UNITQTY, CUSTOMER_PN, PPPN, Online_Offline, PROGRAM_STATUS, PROGRAM_TIME, CHECKSUM, PROGRAMED_FILE_NAME, PROGRAM_LOCATION, dlv_doc_id, lineitem, shipsite, shipdate, trackingnumber, sls_ord_id, Sales_Org, Dist_Channel, soldto_id, shipto_id, Ship_To_Country, Last_Update_Date_Time, Product_Operation_Code, Base_Product_Name, Sales_Document_Type_Cd, Sales_Order_Reason_Code, Family_Code, SBS_Code, Product_Division_Code, Group_Code, Material_Item_Type_Code) + "]}";
            var FinalInputPayload = InputPayload + "," + InputPayloadSecond;
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
           // var InputDataFile = System.IO.File.ReadAllLines(@"C:\poc\CATTS_IRC.txt");
            var SearialNumberList = new List<string>(InputDataFile);

            IEnumerable<string> inputPayloadCATTS = GetInputPayloadDataForCATTS(SearialNumberList);
            IEnumerable<string> inputPayloadIBASE = GetInputPayloadDataForIBASE(SearialNumberList);
            IEnumerable<string> inputPayloadIRC = GetInputPayloadDataForIRC(SearialNumberList);
            IEnumerable<string> inputPayloadSNTRACE = GetInputPayloadDataForSNTRACE(SearialNumberList);
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
            //SNTRACE
            //try
            //{
            var jsonResult = JsonConvert.DeserializeObject(((string[])inputPayloadSNTRACE)[0].ToString()).ToString();
            var resultObjSNTRACE = JsonConvert.DeserializeObject<SyntraceRootObject>(jsonResult.ToString());
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}

            var batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)resultObjIBASE.Input.Count() / batchSize);

            var tasksRootObjectIBASE = new List<Task<IEnumerable<RootObjectWarrantyOutputJSON>>>();
            var tasksCattsTcaServiceObjMain = new List<Task<IEnumerable<ResultCATT>>>();
            var tasksIRCServiceObjMain = new List<Task<IEnumerable<ReturnJSON>>>();
            var tasksSNTRACEServiceObjMain = new List<Task<IEnumerable<SyntraceOutputResponse>>>();
            // List<Task<IEnumerable<RootObjectWarrantyOutputJSON>>> objjsonIBASE = new List<Task<IEnumerable<RootObjectWarrantyOutputJSON>>>();
            TimeSpan ts1 = new TimeSpan();
            for (int i = 0; i < numberOfBatches; i++)
            {
               
                //IBASE
                var currentSearilNo = resultObjIBASE.Input.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                //tasksRootObjectIBASE.Add(GetIBASEBatchWiseData(currentSearilNo, start));
                var tasksRootObjectIBASE1 = await GetIBASEBatchWiseData(currentSearilNo, start);

                //CATTS
                var currentVIDNo = resultObjCATTS.CattsTcaServiceRequest.InputData.InputDataItem.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                //tasksCattsTcaServiceObjMain.Add(GetCattsBatchWiseData(docXMLForCATTS, currentVIDNo, start));
                var tasksCattsTcaServiceObjMain1 = await GetCattsBatchWiseData(docXMLForCATTS, currentVIDNo, start);

                //IRC
                var currentIRCVIDNo = resultObjIRC.cpuVids.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
                //tasksIRCServiceObjMain.Add(GetIRCBatchWiseData(currentIRCVIDNo, start));
                var tasksIRCServiceObjMain1 = await GetIRCBatchWiseData(currentIRCVIDNo, start);

               //SNTRACE
                  var currentSNTRACEVIDNo = resultObjSNTRACE.SN.Skip(i * batchSize).Take(batchSize).ToList(); //Here we need to change the logic this this we need to take 100 data on each processing
              //  tasksSNTRACEServiceObjMain.Add(GetSNTRACEBatchWiseData(resultObjSNTRACE, currentSNTRACEVIDNo, start));
                var  tasksSNTRACEServiceObjMain1 = await GetSNTRACEBatchWiseData(resultObjSNTRACE, currentSNTRACEVIDNo, start);

                DateTime end1 = DateTime.Now;
                ts1 = (end1 - start);
                System.IO.File.AppendAllText(@"C:\poc\FinalOutPut.txt", i +" "+ ts1.Minutes.ToString() + " : " + ts1.Seconds.ToString() + Environment.NewLine);
                // return "Total Time taken :" + ts1.Seconds.ToString();
                //foreach (var r in tasksSNTRACEServiceObjMain1.AsEnumerable())
                //{
                //  var t =  r.Result[0];
                //}
                //one single output
                // if  STATUS <> 'NF' then send only ibase no need to check others
                //IF STATUS  = 'NF' then check others 
                //else sintrace,catts and irc

            }
            //(await Task.WhenAll(tasksRootObjectIBASE)).SelectMany(u => u);
            //(await Task.WhenAll(tasksCattsTcaServiceObjMain)).SelectMany(u => u);
            //(await Task.WhenAll(tasksIRCServiceObjMain)).SelectMany(u => u);
            //DateTime end = DateTime.Now;
            //TimeSpan ts = (end - start);

            //return (await Task.WhenAll(tasks)).SelectMany(u => u);
            /// System.IO.File.AppendAllText(@"C:\poc\OutputCATTS.xml", ts.Seconds.ToString() + Environment.NewLine);
            //  System.IO.File.AppendAllText(@"C:\poc\OutputIBASE.json", ts.Seconds + Environment.NewLine);
            //  System.IO.File.AppendAllText(@"C:\poc\OutputIRC.json", ts.Seconds + Environment.NewLine);
            return "Total Time taken :";
        }
        [HttpGet]
        [Route("GetSNTRACEBatchWiseData")]
        public async Task<IEnumerable<SyntraceOutputResponse>> GetSNTRACEBatchWiseData(SyntraceRootObject resultObjSNTRACE, List<string> tasks, DateTime start)
        {
            // Creating a file
            string myfile = @"C:\poc\OutputIRC.json";

            SyntraceRootObject wrapper = new SyntraceRootObject { SN = tasks.ToArray(), OutPutColumn = resultObjSNTRACE.OutPutColumn.ToArray() };
            string json = JsonConvert.SerializeObject(wrapper);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpclient
             .PostAsync(
                 "https://apis-sandbox.intel.com/m2r/warranty-shipments/v1/builds/materials",
                 byteContent)
             .ConfigureAwait(false);

            var resultContent = response.Content.ReadAsStringAsync().Result;
           // WriteTextAsync(myfile, resultContent);
            var users = JsonConvert.DeserializeObject<SyntraceOutputResponse>(resultContent);
            //////// Appending the given texts
            List<SyntraceOutputResponse> objjson = new List<SyntraceOutputResponse>();
            objjson.Add(users);
            return objjson;
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
