﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beautywebsite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatesalem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagepath",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagepath",
                table: "Users");
        }
    }
}
