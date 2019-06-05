using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarApp
{
    class Program
    {
        public class ApplicationContext:DbContext
        {
            public DbSet<Car> Cars { get; set; }

            public ApplicationContext()
            {
                Database.EnsureCreated();
            }


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDb;database = CarApp;Trusted_Connection = True;");
            }

        }
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Car car1 = new Car { Marka = "Nissan", Color = "Yellow", Price = 1000000, Year = 2018};
                Car car2 = new Car { Marka = "Volvo", Color = "Brown", Price = 80000, Year = 2000};

                db.Cars.Add(car1);
                db.Cars.Add(car2);
                db.SaveChanges();
                Console.WriteLine("Done");

                var cars = db.Cars.ToList();
                foreach (Car c in cars)
                    Console.WriteLine("{0} {1} {2} ({3}) {4}", c.Marka, c.Price, c.Color, c.Year);
            }
            Console.ReadLine();
        }
    }
}
