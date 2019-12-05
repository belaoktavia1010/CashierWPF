using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDB32.Model
{
    [Table("tb_m_users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public User() { }
        public User(string name, string email, string password, Roles role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }
    }
}
