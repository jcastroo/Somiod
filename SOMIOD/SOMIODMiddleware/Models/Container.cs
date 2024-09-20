using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization.Metadata;
using System.Web;

namespace SOMIODMiddleware.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Data> Data { get; set; }
        public int Parent_Id { get; set; }
        public DateTime CreationDate { get; set; }

        public List<Subscription> Subscription { get; set; }
    }
}