using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AliExpress.Context.Migrations
{
    /// <inheritdoc />
    public partial class initaexaex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description_AR",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_AR",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_AR",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description_Ar",
                table: "DeliveryMethods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name__Ar",
                table: "DeliveryMethods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_Ar",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City_AR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country_AR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName_AR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender_AR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName_AR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "National_AR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_AR",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Title_AR",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description_AR",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "Description_Ar",
                table: "DeliveryMethods");

            migrationBuilder.DropColumn(
                name: "Name__Ar",
                table: "DeliveryMethods");

            migrationBuilder.DropColumn(
                name: "Name_Ar",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "City_AR",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country_AR",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FName_AR",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender_AR",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LName_AR",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "National_AR",
                table: "AspNetUsers");
        }
    }
}
