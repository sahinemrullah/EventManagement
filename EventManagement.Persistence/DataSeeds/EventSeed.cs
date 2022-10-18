namespace EventManagement.Persistence.DataSeeds
{
    internal static class EventSeed
    {
        private const string lipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean eget nulla nibh. Donec tempus ex quis egestas lacinia. Donec aliquam ultricies massa, at sagittis massa porta vel. Fusce porta augue sed scelerisque placerat. Donec sodales, orci non vulputate ultrices, libero magna tempor dui, commodo ultrices magna nunc ut sapien. Vivamus sollicitudin commodo vehicula. Pellentesque sit amet vivamus.";
        public static object[] Events => new object[] {
            new { 
                Id = 1,
                Name = "Gwendolin Ankara Spor",
                Address = "Address",
                Description = lipsum,
                Start = DateTime.Now.AddDays(6),
                ApplicationDeadline = DateTime.Now.AddDays(5),
                ApprovedForListing = true,
                ParticipantLimit = 1,
                CategoryId = 3,
                CityId = 1,
                UserId = 2,
                Discriminator = "Event"
            },
            new { 
                Id = 2,
                Name = "John İstanbul Müzik",
                Address = "Address",
                Description = lipsum,
                Start = DateTime.Now.AddDays(1),
                ApplicationDeadline = DateTime.Now,
                ApprovedForListing = true,
                ParticipantLimit = 2,
                CategoryId = 1,
                CityId = 2,
                UserId = 3,
                Discriminator = "Event"
            },
        };
        public static object[] TicketEvents => new object[] {

            new {
                Id = 3,
                Name = "Gwendolin Eskişehir Müzik",
                Address = "Address",
                Description = lipsum,
                Start = DateTime.Now.AddDays(-1),
                ApplicationDeadline = DateTime.Now.AddDays(-1),
                ApprovedForListing = true,
                ParticipantLimit = 1,
                CategoryId = 1,
                CityId = 4,
                UserId = 2,
                Discriminator = "TicketEvent",
                Price = 109.00m
            },
            new {
                Id = 4,
                Name = "Jane Konya Sinema",
                Address = "Address",
                Description = lipsum,
                Start = DateTime.Now.AddDays(10),
                ApplicationDeadline = DateTime.Now.AddDays(10),
                ApprovedForListing = false,
                ParticipantLimit = 1,
                CategoryId = 1,
                CityId = 3,
                UserId = 4,
                Discriminator = "TicketEvent",
                Price = 999.99m
            },
        };
    }
}
