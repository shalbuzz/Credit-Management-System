using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credit_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class adddedd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Loans");

            migrationBuilder.AddColumn<int>(
                name: "StatusForLoan",
                table: "Loans",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusForLoan",
                table: "Loans");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Loans",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
