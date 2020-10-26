using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Display(Name = "نقش کاربر")]
        public int RoleID { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350)]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        [MaxLength(400)]

        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [MaxLength(350)]

        public string Password { get; set; }

        [Required]
        [MaxLength(200)]

        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت")]
        [Required]


        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        [Required]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}")]
        public DateTime RegisterDate { get; set; }

        public virtual Roles Roles { get; set; }
        public Users()
        {

        }
    }
}
