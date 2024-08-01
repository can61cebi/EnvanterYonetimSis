using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EYS.Plugins.EFCoreSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class tasima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Envanterler",
                columns: table => new
                {
                    EnvanterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnvanterIsim = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envanterler", x => x.EnvanterId);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunIsim = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.UrunId);
                });

            migrationBuilder.CreateTable(
                name: "EnvanterIslemleri",
                columns: table => new
                {
                    EnvanterIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    almaSayisi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UretimNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnvanterId = table.Column<int>(type: "int", nullable: false),
                    OncekiAdet = table.Column<int>(type: "int", nullable: false),
                    AksiyonTipi = table.Column<int>(type: "int", nullable: false),
                    SonrakiAdet = table.Column<int>(type: "int", nullable: false),
                    AdetFiyati = table.Column<double>(type: "float", nullable: false),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlanKisi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvanterIslemleri", x => x.EnvanterIslemId);
                    table.ForeignKey(
                        name: "FK_EnvanterIslemleri_Envanterler_EnvanterId",
                        column: x => x.EnvanterId,
                        principalTable: "Envanterler",
                        principalColumn: "EnvanterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunEnvanterleri",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    EnvanterId = table.Column<int>(type: "int", nullable: false),
                    EnvanterAdeti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunEnvanterleri", x => new { x.UrunId, x.EnvanterId });
                    table.ForeignKey(
                        name: "FK_UrunEnvanterleri_Envanterler_EnvanterId",
                        column: x => x.EnvanterId,
                        principalTable: "Envanterler",
                        principalColumn: "EnvanterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunEnvanterleri_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunIslemleri",
                columns: table => new
                {
                    UrunIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UretimNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    OncekiAdet = table.Column<int>(type: "int", nullable: false),
                    AksiyonTipi = table.Column<int>(type: "int", nullable: false),
                    SonrakiAdet = table.Column<int>(type: "int", nullable: false),
                    AdetFiyati = table.Column<double>(type: "float", nullable: true),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlanKisi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunIslemleri", x => x.UrunIslemId);
                    table.ForeignKey(
                        name: "FK_UrunIslemleri_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Envanterler",
                columns: new[] { "EnvanterId", "Adet", "EnvanterIsim", "Fiyat" },
                values: new object[,]
                {
                    { 1, 17, "Ekran Kartı", 12000.0 },
                    { 2, 16, "İşlemci", 6000.0 },
                    { 3, 16, "Anakart", 4000.0 },
                    { 4, 32, "Rastgele Erişim Bellek", 600.0 },
                    { 5, 16, "Güç Kaynağı", 2000.0 },
                    { 6, 20, "Katı Hal Sürücüsü", 2000.0 },
                    { 7, 28, "Monitör", 13000.0 },
                    { 8, 30, "Klavye", 1100.0 },
                    { 9, 28, "Fare", 750.0 },
                    { 10, 17, "Kulaklık", 1600.0 },
                    { 11, 20, "Masa", 11000.0 },
                    { 12, 20, "Koltuk", 6800.0 },
                    { 13, 20, "Raf", 780.0 },
                    { 14, 50, "Priz", 200.0 },
                    { 15, 60, "Kalem", 50.0 },
                    { 16, 2000, "Kağıt", 10.0 }
                });

            migrationBuilder.InsertData(
                table: "Urunler",
                columns: new[] { "UrunId", "Adet", "Fiyat", "UrunIsim" },
                values: new object[,]
                {
                    { 1, 16, 26600.0, "Masaüstü Bilgisayar" },
                    { 2, 17, 14850.0, "Çevre Bileşenleri" },
                    { 3, 20, 18840.0, "Ofis Mobilyaları" }
                });

            migrationBuilder.InsertData(
                table: "UrunEnvanterleri",
                columns: new[] { "EnvanterId", "UrunId", "EnvanterAdeti" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 1, 1 },
                    { 4, 1, 2 },
                    { 5, 1, 1 },
                    { 6, 1, 1 },
                    { 7, 2, 1 },
                    { 8, 2, 1 },
                    { 9, 2, 1 },
                    { 10, 2, 1 },
                    { 11, 3, 1 },
                    { 12, 3, 1 },
                    { 13, 3, 1 },
                    { 14, 3, 2 },
                    { 15, 3, 2 },
                    { 16, 3, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnvanterIslemleri_EnvanterId",
                table: "EnvanterIslemleri",
                column: "EnvanterId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunEnvanterleri_EnvanterId",
                table: "UrunEnvanterleri",
                column: "EnvanterId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunIslemleri_UrunId",
                table: "UrunIslemleri",
                column: "UrunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvanterIslemleri");

            migrationBuilder.DropTable(
                name: "UrunEnvanterleri");

            migrationBuilder.DropTable(
                name: "UrunIslemleri");

            migrationBuilder.DropTable(
                name: "Envanterler");

            migrationBuilder.DropTable(
                name: "Urunler");
        }
    }
}
