using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDB32.Model
{
    [Table("tb_m_transaction")]
    public class Transaction
    {
        
        [Key]
        public int Id { get; set; }
        public int TotalPrice { get; set; }

        public DateTimeOffset OrderDate { get; set; }
        //public ICollection<Items> Items { get; set; }

        public Transaction() {
            this.OrderDate = DateTimeOffset.Now.LocalDateTime;

        }
        public Transaction( int totalprice) {
            this.OrderDate = DateTimeOffset.Now.LocalDateTime;
            this.TotalPrice = totalprice;
        }
        
    }
}
