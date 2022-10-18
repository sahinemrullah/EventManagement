using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement.Persistence.Migrations
{
    public partial class UserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { 2, "gwendolyn.connelly@ethereal.email", "Gwendolin", "Connely", "$2a$11$GZnQc1ePrcdTGa4sSfpsFeZEIjdxMFMPtuSI5UAN89PvFxJrHv9w." });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { 3, "john.doe@admin.com", "John", "Doe", "$2a$11$uyjFi3yqEJiFpZu4IkCxfOmjL.EKPQEu4pobskKZKM3UU3xYDkwyO" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { 4, "jane.doe@admin.com", "Jane", "Doe", "$2a$11$uyjFi3yqEJiFpZu4IkCxfOmjL.EKPQEu4pobskKZKM3UU3xYDkwyO" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
