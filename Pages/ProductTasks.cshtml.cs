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
    public class ProductTasksModel : PageModel
    {

        public IHtmlHelper htmlHelper;
        private readonly IProductData productData;
        [BindProperty(SupportsGet = true)]
        public Product product { get; set; }
        [BindProperty]
        public List<Task> tasks { get; set; }
        [TempData]
        public string Message { get; set; }
        public bool isTaskAdded;
        [BindProperty]
        public List<Task> productTasks { get; set; }
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int taskId { get; set; }
        public IConfiguration config { get; set; }
        public Product productUpdated { get; set; }
        public ProductTasksModel(IProductData productData, IConfiguration config, IHtmlHelper htmlHelper)
        {
            this.productData = productData;
            this.htmlHelper = htmlHelper;
            this.config = config;
            this.tasks = productData.GetAllTasks();
        }
        public IActionResult OnGet(int Id)
        {
            product = productData.GetProductById(Id);
            productTasks = product.Tasks;
            if (product == null)
            {
                return RedirectToPage("./TaskManageMentConsole");
            }
            return Page();
        }
        public void OnClickAddTask(int taskId)
        {
            if (taskId!=0)
            {
                productUpdated = productData.AddTaskToProductId(product.Id, taskId);
                product = productUpdated;
                isTaskAdded = true;
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            OnClickAddTask(taskId);
            TempData["Message"] = "Saved!";
            return RedirectToPage("./ProductDetails", new { Id = productUpdated.Id, taskId = taskId});            
        }
    }
}
