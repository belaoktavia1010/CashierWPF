using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDB32.Model
{
    [Table("tb_m_transactionItem")]
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Transaction Transaction { get; set; }
        public Items Item { get; set; }

        public TransactionItem() { }
        public TransactionItem(int quantity, Transaction transaction, Items item)
        {
            this.Quantity = quantity;
            this.Transaction = transaction;
            this.Item = item;
        }
    }
}
