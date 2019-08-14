using DataContract.Models;
using System.Linq;

namespace DAL.Data
{
    public class DbInitializer
    {
        public static void Initialize(TbcFinalExamContext context)
        {
            context.Database.EnsureCreated();
            if (context.Cities.Any()) // Look for any cities.
            {
                return;   // DB has been seeded
            }
            context.Cities.Add(new City { Name = "Tbilisi" });
            context.Cities.Add(new City { Name = "Mtskheta" });
            context.Cities.Add(new City { Name = "Batumi" });
            context.Cities.Add(new City { Name = "Rustavi" });
            context.Cities.Add(new City { Name = "Telavi" });
            context.Cities.Add(new City { Name = "Dusheti" });
            context.Cities.Add(new City { Name = "Kutaisi" });

            context.Genders.Add(new Gender { Name = "Male" });
            context.Genders.Add(new Gender { Name = "Male" });

            context.PhysicalPersonConnectionTypes.Add(new PhysicalPersonConnectionType { Name = "Relative" });
            context.PhysicalPersonConnectionTypes.Add(new PhysicalPersonConnectionType { Name = "Friend" });
            context.PhysicalPersonConnectionTypes.Add(new PhysicalPersonConnectionType { Name = "Family" });

            context.TelephoneTypes.Add(new TelephoneType { Name = "Mobile" });
            context.TelephoneTypes.Add(new TelephoneType { Name = "Office" });
            context.TelephoneTypes.Add(new TelephoneType { Name = "Home" });
            context.SaveChanges();
        }
    }
}
