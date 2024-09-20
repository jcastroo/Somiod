using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace SOMIODMiddleware.Models
{

    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Container> Container { get; set; }
        public DateTime CreationDate { get; set; } 
    }
}