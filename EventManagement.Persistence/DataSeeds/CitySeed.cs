namespace EventManagement.Persistence.DataSeeds
{
    internal static class CitySeed
    {
        public static object[] Objects => new object[] {
                new {
                    Id = 1,
                    Name = "Ankara"
                },
                new {
                    Id = 2,
                    Name = "İstanbul"
                },
                new {
                    Id = 3,
                    Name = "Konya"
                },
                new {
                    Id = 4,
                    Name = "Eskişehir"
                },
                new {
                    Id = 5,
                    Name = "Kocaeli"
                },
            };
    }
}
