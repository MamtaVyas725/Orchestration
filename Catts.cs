using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrchestrationLayerDemo
{
    public class Catts
    {
        public Catts()
        {

        }

        [XmlRoot(ElementName = "InputDataItem")]
        public class CattsTcaServiceObjMain
        {
            public CattsTcaServiceRequest CattsTcaServiceRequest { get; set; }
        }
        public class CattsTcaServiceRequest
        {
            public string ApplicationName { get; set; }
            public string UserId { get; set; }
            public string SubOperationName { get; set; }
            public string TraceLevel { get; set; }
            public string TraceType { get; set; }
            public string InputType { get; set; }
            public string AdditionalReports { get; set; }
            public InputData InputData { get; set; }
        }


        public class InputData
        {
            [XmlElement(ElementName = "InputDataItem")]
            public List<InputDataItem> InputDataItem { get; set; }
        }


        public class InputDataItem
        {
            [XmlAttribute("type")]
            public string type { get; set; }

            [XmlTextAttribute()]
            public string text { get; set; }

        }


    }
}
