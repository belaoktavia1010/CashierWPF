using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDB32.Model
{
    [Table("tb_m_roles")]
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Roles() { }
        public Roles(string name)
        {
            this.Name = name;
        }
    }
}
