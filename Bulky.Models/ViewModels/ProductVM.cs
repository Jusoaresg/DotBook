using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Models.ViewModels
{
    public class ProductVM
    {
        public required Product Product {get; set;}
        public required IEnumerable<SelectListItem> CategoryList {get; set;}
    }
}