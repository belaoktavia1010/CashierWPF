﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDB32.Model
{
    [Table("tb_m_items")]
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public Supplier Supplier { get; set; }
        
        public Items() { }
        public Items(string name, int stock, int price, Supplier supplierid)
        {
            this.Name = name;
            this.Stock = stock;
            this.Price = price;
            this.Supplier = supplierid;
        }
    }
}
