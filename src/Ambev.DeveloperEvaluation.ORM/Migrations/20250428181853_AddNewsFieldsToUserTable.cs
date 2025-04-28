using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsFieldsToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "auth",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                schema: "auth",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                schema: "auth",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                schema: "auth",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                schema: "auth",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                schema: "auth",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                schema: "auth",
                table: "Users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                schema: "auth",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Firstname",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Lastname",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Number",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Street",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                schema: "auth",
                table: "Users");
        }
    }
}
