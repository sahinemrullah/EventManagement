using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement.Persistence.Migrations
{
    public partial class EventAndIntegrationServiceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "ApplicationDeadline", "ApprovedForListing", "CategoryId", "CityId", "Description", "Discriminator", "Name", "ParticipantLimit", "Start", "UserId" },
                values: new object[,]
                {
                    { 1, "Address", new DateTime(2022, 10, 23, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5328), true, 3, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean eget nulla nibh. Donec tempus ex quis egestas lacinia. Donec aliquam ultricies massa, at sagittis massa porta vel. Fusce porta augue sed scelerisque placerat. Donec sodales, orci non vulputate ultrices, libero magna tempor dui, commodo ultrices magna nunc ut sapien. Vivamus sollicitudin commodo vehicula. Pellentesque sit amet vivamus.", "Event", "Gwendolin Ankara Spor", 1, new DateTime(2022, 10, 24, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5311), 2 },
                    { 2, "Address", new DateTime(2022, 10, 18, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5330), true, 1, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean eget nulla nibh. Donec tempus ex quis egestas lacinia. Donec aliquam ultricies massa, at sagittis massa porta vel. Fusce porta augue sed scelerisque placerat. Donec sodales, orci non vulputate ultrices, libero magna tempor dui, commodo ultrices magna nunc ut sapien. Vivamus sollicitudin commodo vehicula. Pellentesque sit amet vivamus.", "Event", "John İstanbul Müzik", 2, new DateTime(2022, 10, 19, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5329), 3 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "ApplicationDeadline", "ApprovedForListing", "CategoryId", "CityId", "Description", "Discriminator", "Name", "ParticipantLimit", "Price", "Start", "UserId" },
                values: new object[,]
                {
                    { 3, "Address", new DateTime(2022, 10, 17, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5350), true, 1, 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean eget nulla nibh. Donec tempus ex quis egestas lacinia. Donec aliquam ultricies massa, at sagittis massa porta vel. Fusce porta augue sed scelerisque placerat. Donec sodales, orci non vulputate ultrices, libero magna tempor dui, commodo ultrices magna nunc ut sapien. Vivamus sollicitudin commodo vehicula. Pellentesque sit amet vivamus.", "TicketEvent", "Gwendolin Eskişehir Müzik", 1, 109.00m, new DateTime(2022, 10, 17, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5349), 2 },
                    { 4, "Address", new DateTime(2022, 10, 28, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5355), false, 1, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean eget nulla nibh. Donec tempus ex quis egestas lacinia. Donec aliquam ultricies massa, at sagittis massa porta vel. Fusce porta augue sed scelerisque placerat. Donec sodales, orci non vulputate ultrices, libero magna tempor dui, commodo ultrices magna nunc ut sapien. Vivamus sollicitudin commodo vehicula. Pellentesque sit amet vivamus.", "TicketEvent", "Jane Konya Sinema", 1, 999.99m, new DateTime(2022, 10, 28, 17, 27, 7, 934, DateTimeKind.Local).AddTicks(5354), 4 }
                });

            migrationBuilder.InsertData(
                table: "IntegrationServices",
                columns: new[] { "Id", "CompanyName", "Email", "PasswordHash", "WebDomain" },
                values: new object[] { 1, "Google", "iss@iss.com", "$2a$11$07EUmoZxDMyAeIamjR8aeui2M.WKzZ7NfgHLQVu.ryB8ZkBn3YABK", "https://www.google.com.tr/" });

            migrationBuilder.InsertData(
                table: "EventParticipants",
                columns: new[] { "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "EventParticipants",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IntegrationServices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
