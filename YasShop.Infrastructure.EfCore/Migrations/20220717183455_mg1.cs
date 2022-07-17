using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasShop.Infrastructure.EfCore.Migrations
{
    public partial class mg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                maxLength: 450,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetRoles_ParentId",
                table: "AspNetRoles",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetRoles_ParentId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AspNetRoles");
        }
    }
}
