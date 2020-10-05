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
using Task = TaskManagement.core.Models.Task;

namespace TaskManagementDesk.Pages
{
    public class ProductDetailsModel : PageModel
    {

        public IHtmlHelper htmlHelper;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int taskId { get; set; }
        private readonly IProductData productData;
        [BindProperty(SupportsGet = true)]
        public Product product { get; set; }
        [BindProperty]
        public IEnumerable<Task> tasks { get; set; }
        [TempData]
        public string Message { get; set; }
        public bool isTaskAdded;
        [BindProperty(SupportsGet = true)]
        public string name { get; set; }
        public IConfiguration config { get; set; }
        public ProductDetailsModel(IProductData productData, IConfiguration config, IHtmlHelper htmlHelper)
        {
            this.productData = productData;
            this.htmlHelper = htmlHelper;
            this.config = config;
        }
        public IActionResult OnGet()
        {
            product = productData.GetProductById(Id);
            tasks = product.Tasks;


            if (product == null)
            {
                return RedirectToPage("./TaskManageMentConsole");
            }
            return Page();
        }
        public IActionResult OnPost(int taskId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TempData["Message"] = "Task Added!";
            return RedirectToPage("./ProductTasks", new { Id = product.Id });
        }
    }
}
