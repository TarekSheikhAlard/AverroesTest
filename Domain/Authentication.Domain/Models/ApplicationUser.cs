using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Infrastructure.Domain.Consts;

namespace Authentication.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string MotherName { get; set; }

        [MaxLength(50)]
        public string FatherName { get; set; }

        [MaxLength(50)]
        public string IdentityNumber { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public Gender Gender { get; set; }

    }
}
