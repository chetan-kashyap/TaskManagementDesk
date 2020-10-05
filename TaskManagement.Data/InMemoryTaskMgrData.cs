using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.core.Models;
using static TaskManagement.core.Models.Product;

namespace TaskManagement.Data
{
    public class InMemoryTaskMgrData : IProductData
    {
        public readonly List<Product> Products;
        public readonly List<Task> Tasks;
        public Task task;
        public Product product;
        public InMemoryTaskMgrData()
        {
            Products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Book", Product_Category = Product.Category.Physical, Tasks = new List<Task>() { } },
                new Product() { Id = 2, Name = "Ski_video", Product_Category = Product.Category.Video, Tasks = new List<Task>() { } },
                new Product() { Id = 3, Name = "Activate_membership", Product_Category = Product.Category.Membership, Tasks = new List<Task>() { }},
                new Product() { Id = 4, Name = "Upgrade_membership", Product_Category = Product.Category.Membership, Tasks = new List<Task>() { } },
                new Product() { Id = 5, Name = "Pencil", Product_Category = Product.Category.Physical, Tasks = new List<Task>() { } }
            };

            Tasks = new List<Task>()
            {
                new Task() { Id = 1, Description = "Generate packing slip for shipping" },
                new Task() { Id = 2, Description = "Duplicate packing slip for royalty department" },
                new Task() { Id = 3, Description = "Activate membership" },
                new Task() { Id = 4, Description = "Apply upgrade to membership" },
                new Task() { Id = 5, Description = "Send an email to owner to inform about activation/upgrade" },
                new Task() { Id = 6, Description = "Add free first-aid video" },
                new Task() { Id = 7, Description = "Generate commission payment to agent" }
            };
            DefaultTaskAllocation();
        }

        public Product AddProduct(Product newProduct)
        {
            Products.Add(newProduct);
            newProduct.Id = Products.Max(b => b.Id) + 1;
            return newProduct;
        }
        public IEnumerable<Product> GetProductByName(string name)
        {
            return from p in Products
                   where string.IsNullOrEmpty(name) || p.Name.ToLower().StartsWith(name)
                   orderby p.Id
                   select p;
        }

        public Product GetProductById(int Id)
        {
            return Products.SingleOrDefault(p => p.Id == Id);
        }

        public Task GetTaskById(int Id)
        {
            return Tasks.Single(t => t.Id == Id);
        }
        public List<Task> GetAllTasks()
        {
            return Tasks;
        }
        public Product AddTaskToProductId(int Id, int taskId)
        {
            var product = Products.SingleOrDefault(p => p.Id == Id);
            task = GetTaskById(taskId);
            product.Tasks.Add(task);
            if (product != null)
            {
                product.Name = product.Name;
                product.Tasks[0] = task;
            }
            return product;
        }
        //allocation of the default tasks
        public Product DefaultTaskAllocation()
        {
            foreach (var prod in Products)
            {
                if (prod.Name == "pencil") //pencil instead of physical product
                {
                    return AddTaskToProductId(prod.Id, 1); //task id is 1
                }
                if (prod.Name.ToLower() == "book")
                {
                    return AddTaskToProductId(prod.Id, 2);
                }
                if (prod.Name.ToLower() == "activate_membership")
                {
                    return AddTaskToProductId(prod.Id, 3);
                }
                if (prod.Name.ToLower() == "upgrade_membership")
                {
                    return AddTaskToProductId(prod.Id, 4);
                }
                if (prod.Product_Category == Category.Membership)
                {
                    return AddTaskToProductId(prod.Id, 5);
                }
                if (prod.Product_Category == Category.Video 
                    && prod.Name.ToLower() == "ski_video")
                {
                    return AddTaskToProductId(prod.Id, 6);
                }
                if (prod.Product_Category == Category.Physical)
                {
                    return AddTaskToProductId(prod.Id, 7);
                }
                product = prod;
            }
            return product;
        }

    }
}
