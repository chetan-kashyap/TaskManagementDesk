using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static TaskManagement.core.Models.Product;
using TaskManagement.Data;
using TaskManagement.core.Models;
using Microsoft.Extensions.Configuration;

namespace TaskManagementDesk.Pages
{
    public class TaskManagementConsoleModel : PageModel
    {

        public IEnumerable<SelectListItem> Categories;
        private readonly IProductData productData;
        [BindProperty]
        public Product product { get; set; }
        private readonly IConfiguration config;
        public string Message { get; set; }
        public IEnumerable<Product> products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        public TaskManagementConsoleModel(IConfiguration config, IProductData productData)
        {
            this.config = config;
            this.productData = productData;
        }
        public void OnGet()
        {
            products = productData.GetProductByName(searchTerm);
            Message = config["AllowedHosts"];
        }
    }
}
