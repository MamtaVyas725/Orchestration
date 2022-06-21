using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OrchestrationLayerDemo.Controllers
{
    public class Item
    {
        public string SerialNumber { get; internal set; }

        public class Person
        {
            public string SerialNumber { get; set; }
            // public List<OrchestrationLayerDemo.Person> Input { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        public class RootObject 
        {
            public List<Person> Input { get; set; }

        }

        public class data
        {
            public string SerialNumber { get; set; }

            public string ProductMM { get; set; }
            public string DeliveryNote { get; set; }
            public string CreditDays { get; set; }
            public string ServiceContractNum { get; set; }
            public string WarrantyType { get; set; }
            public string StartDate { get; set; }
            public string WarrantyMM { get; set; }
            public string EndDate { get; set; }
            public string Status { get; set; }
            public string ComplCoverage { get; set; }
            public string RegInd { get; set; }
            public string AWRAllowed { get; set; }
            public string WarrantySerialNumber { get; set; }

        }
        public class RootObjectOutput
        {
            public RootObjectWarrantyOutput Warranty { get; set; }


        }
        public class RootObjectWarrantyOutput
        {
            public List<data> Output { get; set; }
        }




    }


  

}