using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.core.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Product_Category { get; set; }
        public List<Task> Tasks { get; set; }

    }
}
