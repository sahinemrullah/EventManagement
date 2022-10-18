namespace EventManagement.Persistence.DataSeeds
{
    internal static class EventParticipantSeed
    {
        public static object[] Objects => new object[] {
            new { UserId = 3, EventId = 1 },
            new { UserId = 2, EventId = 2 },
            new { UserId = 4, EventId = 2 },
            new { UserId = 3, EventId = 3 },
        };
    }
}
