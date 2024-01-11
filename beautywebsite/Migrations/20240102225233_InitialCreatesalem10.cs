using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beautywebsite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatesalem10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "secondname",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "secondname",
                table: "Users");
        }
    }
}
