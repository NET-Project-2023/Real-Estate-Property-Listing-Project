using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesUrls",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImagesUrlsSerialized",
                table: "Properties");

            migrationBuilder.AddColumn<List<byte[]>>(
                name: "Images",
                table: "Properties",
                type: "bytea[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Properties");

            migrationBuilder.AddColumn<List<string>>(
                name: "ImagesUrls",
                table: "Properties",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ImagesUrlsSerialized",
                table: "Properties",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
