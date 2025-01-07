using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Data.Seeds
{
    public static class Seeder
    {
        public static void Seed (this ModelBuilder builder)
        {
            var adminRole = new IdentityRole("Admin");
            adminRole.NormalizedName = adminRole.Name.ToUpper();

            var customerRole = new IdentityRole("Customer");
            customerRole.NormalizedName = customerRole.Name.ToUpper();

            var driverRole = new IdentityRole("Driver");
            driverRole.NormalizedName = driverRole.Name.ToUpper();

            var customerServiceRole = new IdentityRole("CustomerService");
            customerServiceRole.NormalizedName = customerServiceRole.Name.ToUpper();

            List<IdentityRole> IdentityRoleList = new List<IdentityRole>
            {
                adminRole,
                customerRole,
                driverRole,
                customerServiceRole
            };

            builder.Entity<IdentityRole>().HasData(IdentityRoleList);
        }
    }
}
