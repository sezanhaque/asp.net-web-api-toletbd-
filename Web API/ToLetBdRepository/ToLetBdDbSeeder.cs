using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToLetBdEntity;

namespace ToLetBdRepository
{
    public class ToLetBdDbSeeder: DropCreateDatabaseIfModelChanges<ToLetBdDbContext>
    {
        protected override void Seed(ToLetBdDbContext context)
        {
            UserRepository userRepo = new UserRepository();
            UserTypeRepository uTypeRepo = new UserTypeRepository();

            UserType adminType = new UserType();
            adminType.Id = 1;
            adminType.TypeName = "Admin";
            uTypeRepo.Insert(adminType);

            UserType ownerType = new UserType();
            ownerType.Id = 2;
            ownerType.TypeName = "House Owner";
            uTypeRepo.Insert(ownerType);

            UserType renterType = new UserType();
            renterType.Id = 3;
            renterType.TypeName = "Renter";
            uTypeRepo.Insert(renterType);

            User admin = new User();
            admin.Name = "Nahid";
            admin.Email = "nahid@gmail.com";
            admin.Password = "123456";
            admin.Gender = "Male";
            admin.PhnNo = "01828-568150";
            admin.UserTypeId = adminType.Id;
            userRepo.Insert(admin);

            User owner = new User();
            owner.Name = "Belal";
            owner.Email = "belal@gmail.com";
            owner.Password = "123456";
            owner.Gender = "Male";
            owner.PhnNo = "01828-999999";
            owner.UserTypeId = ownerType.Id;
            userRepo.Insert(owner);

            User renter = new User();
            renter.Name = "NK";
            renter.Email = "nk@gmail.com";
            renter.Password = "123456";
            renter.Gender = "Male";
            renter.PhnNo = "01828-123456";
            renter.UserTypeId = renterType.Id;
            userRepo.Insert(renter);



        }
    }
}
