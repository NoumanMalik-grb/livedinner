using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveDinner.Models
{
    public class ClassMenu
    {
        public List<Category> categories { get; set; }
        public List<Product> products { get; set; }
    }
}