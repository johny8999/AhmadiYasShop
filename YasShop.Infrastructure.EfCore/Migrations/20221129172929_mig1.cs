using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasShop.Infrastructure.EfCore.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OTPData",
                table: "AspNetUsers",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTPData",
                table: "AspNetUsers");
        }
    }
}
