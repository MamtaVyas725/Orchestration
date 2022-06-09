using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OrchestrationLayerDemo.Controllers
{
   public  class Item
    {
        public List<string> SerialNumber { get; set; }
    }

    public class Attribute
    {
        public string SerialNumber { get; set; }
    }

   

    public class Person
    {
        public string SerialNumber { get; set; }
       
    }

    public class RootObject
    {
        public List<Person> Input { get; set; }

       
    }

}