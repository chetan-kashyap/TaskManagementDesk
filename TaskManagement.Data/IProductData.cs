using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.core.Models;

namespace TaskManagement.Data
{
    public interface IProductData
    {
        IEnumerable<Product> GetProductByName(string name);
        Product AddProduct(Product product);
        Product GetProductById(int Id);
        Task GetTaskById(int Id);
        List<Task> GetAllTasks();
        Product AddTaskToProductId(int Id, int taskId);

    }
}
