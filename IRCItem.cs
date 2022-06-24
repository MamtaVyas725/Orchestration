using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 namespace OrchestrationLayerDemo
{
    public class IRCItem 
    {
        public string cpuVid { get; set; }
        public string message { get; set; }
    }
    public class IRCRootObject
    {
        public List<IRCItem> cpuVids { get; set; }
    }

    public class ReturnJSON
    {
        public string errorMessage { get; set; }
        public List<IRCItem> data { get; set; }
    }

   
    public class DataItem
    {
        public string cpuVId { get; set; }
        public string message { get; set; }
    }

    public class Root
    {
        public string errorMessage { get; set; }
        public List<DataItem> data { get; set; }
    }

}
