using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDB32.Model;

namespace CRUDB32.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyContext") { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
