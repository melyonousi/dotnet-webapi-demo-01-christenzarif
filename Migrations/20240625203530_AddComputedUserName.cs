using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_webapi_demo_01_christenzarif.Migrations
{
    /// <inheritdoc />
    public partial class AddComputedUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "SUBSTRING(Email, 1, CHARINDEX('@', Email) - 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Employees");
        }
    }
}
