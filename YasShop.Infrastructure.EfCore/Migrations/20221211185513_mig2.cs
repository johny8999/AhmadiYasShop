using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasShop.Infrastructure.EfCore.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFileServer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HttpDomain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HttpPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: false),
                    FtpData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFileServer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFileTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Extentions = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTopics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFilePath",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileServerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilePath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFilePath_tblFileServer_FileServerId",
                        column: x => x.FileServerId,
                        principalTable: "tblFileServer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategory_tblTopics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "tblTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FilePathId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    FileTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SizeOnDisk = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFiles_tblFilePath_FilePathId",
                        column: x => x.FilePathId,
                        principalTable: "tblFilePath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFiles_tblFileTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "tblFileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCategoryTranslates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    LangId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ImgId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tblCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tblLanguagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategoryTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategoryTranslates_tblCategory_tblCategoryId",
                        column: x => x.tblCategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblCategoryTranslates_tblLanguages_tblLanguagesId",
                        column: x => x.tblLanguagesId,
                        principalTable: "tblLanguages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblContries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlagImgId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblContries_tblFiles_FlagImgId",
                        column: x => x.FlagImgId,
                        principalTable: "tblFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblProvince",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProvince", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProvince_tblContries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblContries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_tblProvince_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tblProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Plaque = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblAddress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblAddress_tblCities_CityId",
                        column: x => x.CityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_CityId",
                table: "tblAddress",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_UserId",
                table: "tblAddress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_TopicId",
                table: "tblCategory",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoryTranslates_tblCategoryId",
                table: "tblCategoryTranslates",
                column: "tblCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategoryTranslates_tblLanguagesId",
                table: "tblCategoryTranslates",
                column: "tblLanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_ProvinceId",
                table: "tblCities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblContries_FlagImgId",
                table: "tblContries",
                column: "FlagImgId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblFilePath_FileServerId",
                table: "tblFilePath",
                column: "FileServerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiles_FilePathId",
                table: "tblFiles",
                column: "FilePathId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFiles_FileTypeId",
                table: "tblFiles",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProvince_CountryId",
                table: "tblProvince",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAddress");

            migrationBuilder.DropTable(
                name: "tblCategoryTranslates");

            migrationBuilder.DropTable(
                name: "tblCities");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblProvince");

            migrationBuilder.DropTable(
                name: "tblTopics");

            migrationBuilder.DropTable(
                name: "tblContries");

            migrationBuilder.DropTable(
                name: "tblFiles");

            migrationBuilder.DropTable(
                name: "tblFilePath");

            migrationBuilder.DropTable(
                name: "tblFileTypes");

            migrationBuilder.DropTable(
                name: "tblFileServer");
        }
    }
}
