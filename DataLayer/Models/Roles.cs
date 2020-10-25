using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Roles
    {
        [Key]
        public int  RoleID { get; set; }
        [Display(Name ="عنوان نقش")]
        [Required]
        public string RoleTitle { get; set; }
        [Required]
        public string RoleName { get; set; }

        public virtual List<Users> Users { get; set; }
        public Roles()
        {

        }
    }
}
