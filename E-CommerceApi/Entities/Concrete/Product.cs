using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product:IEntity
    {
        public int ProductId { get; set; }
        public bool Status { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public int Price { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

    }
}
