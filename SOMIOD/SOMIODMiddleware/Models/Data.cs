using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMIODMiddleware.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Parent_Id { get; set; }
    }
}