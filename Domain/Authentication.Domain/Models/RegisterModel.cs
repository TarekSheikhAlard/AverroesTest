using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Models
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string IdentityNumber { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string FatherName { get; set; }

        [Required, StringLength(50)]
        public string MotherName { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(128), RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
    }
}
 