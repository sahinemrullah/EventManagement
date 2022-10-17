using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Persistence.DataSeeds
{
    internal static class CategorySeed
    {
        public static object[] Objects => new object[] {
                new {
                    Id = 1,
                    Name = "Müzik"
                },
                new {
                    Id = 2,
                    Name = "Sinema"
                },
                new {
                    Id = 3,
                    Name = "Spor"
                },
                new {
                    Id = 4,
                    Name = "Eğlence"
                },
                new {
                    Id = 5,
                    Name = "Aile"
                },
            };
    }
}
