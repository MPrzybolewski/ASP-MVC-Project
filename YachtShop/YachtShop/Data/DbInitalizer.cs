using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YachtShop.Models;

namespace YachtShop.Data
{
    public static class DbInitalizer
    {
        public static void Initalize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if(context.Clients.Any())
            {
                return;
            }

            var clients = new Client[]
            {
                new Client{FirstName="Patryk", SecondName="Matuszka",PhoneNumber="0700880",Email="pluszaczek2012@wp.pl" },
                new Client{FirstName="Michał", SecondName="Niskowski",PhoneNumber="234965192",Email="niskiwosk@wp.pl" },
                new Client{FirstName="Wiktor", SecondName="Korol",PhoneNumber="969444234",Email="karolwiktor@apple.pl" },
                new Client{FirstName="Rafał", SecondName="Gosik",PhoneNumber="949523944",Email="bilard@wp.pl" },
                new Client{FirstName="Patryk", SecondName="Miłek",PhoneNumber="234854172",Email="wiezienie@wp.pl" }

            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var sellers = new Seller[]
            {
                new Seller{FirstName="Jan", SecondName="Kowalski", Salary=4000, PhoneNumber="959696234", Email="janek@gmail.com"},
                new Seller{FirstName="Bolek", SecondName="Nowak", Salary=3000, PhoneNumber="234456123", Email="bolek@gmail.com"},
                new Seller{FirstName="Lolek", SecondName="Kowalski", Salary=5000, PhoneNumber="567334123", Email="lolek@gmail.com"},
                new Seller{FirstName="Zenek", SecondName="Martyniuk", Salary=4600, PhoneNumber="890345123", Email="zenek@gmail.com"},
                new Seller{FirstName="Sławomir", SecondName="Zapała", Salary=4100, PhoneNumber="456234678", Email="slawek@gmail.com"},
            };
            
            foreach (Seller s in sellers)
            {
                context.Sellers.Add(s);
            }
            context.SaveChanges();

            var yachts = new Yacht[]
            {
                new Yacht{Name="Sztum", Price=50000m, Description="Wyposażony w tv"},
                new Yacht{Name="Malbork", Price=60000m, Description="Wyposażony w tv"},
                new Yacht{Name="Tczew", Price=55000m, Description="Wyposażony w tv" },
                new Yacht{Name="Stary Sztum",Price=70000m, Description="Wyposażony w tv"}
            };

            foreach(Yacht y in yachts)
            {
                context.Yachts.Add(y);
            }
            context.SaveChanges();

        }
    }
}
