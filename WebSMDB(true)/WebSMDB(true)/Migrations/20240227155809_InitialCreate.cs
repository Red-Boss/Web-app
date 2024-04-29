using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSMDB_true_.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budjets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    budjet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bonus = table.Column<int>(type: "int", nullable: false),
                    pr_prodaji = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budjets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Doljnosts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_doljnost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doljnosts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Edinica_izmerenias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naimenovanie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edinica_izmerenias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doljnost = table.Column<int>(type: "int", nullable: false),
                    Oklad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Doljnosts_doljnost",
                        column: x => x.doljnost,
                        principalTable: "Doljnosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produkcias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naimenovanie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    edinica_izmerenia = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkcias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produkcias_Edinica_izmerenias_edinica_izmerenia",
                        column: x => x.edinica_izmerenia,
                        principalTable: "Edinica_izmerenias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Syrios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naimenovanie_materiala = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    edinica_izmerenia = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syrios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Syrios_Edinica_izmerenias_edinica_izmerenia",
                        column: x => x.edinica_izmerenia,
                        principalTable: "Edinica_izmerenias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zarplatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ForPurchase = table.Column<int>(type: "int", nullable: false),
                    ForProduction = table.Column<int>(type: "int", nullable: false),
                    ForSale = table.Column<int>(type: "int", nullable: false),
                    Common = table.Column<int>(type: "int", nullable: false),
                    oklad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    General = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    given = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zarplatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zarplatas_Employees_employee",
                        column: x => x.employee,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prodaja_produkcis",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    produkcia = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employee = table.Column<int>(type: "int", nullable: false),
                    check = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodaja_produkcis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prodaja_produkcis_Employees_employee",
                        column: x => x.employee,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prodaja_produkcis_Produkcias_produkcia",
                        column: x => x.produkcia,
                        principalTable: "Produkcias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodstvo_produkcis",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    produkcia = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employee = table.Column<int>(type: "int", nullable: false),
                    check = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodstvo_produkcis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Proizvodstvo_produkcis_Employees_employee",
                        column: x => x.employee,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvodstvo_produkcis_Produkcias_produkcia",
                        column: x => x.produkcia,
                        principalTable: "Produkcias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingridients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    produkcia = table.Column<int>(type: "int", nullable: false),
                    syrio = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingridients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ingridients_Syrios_syrio",
                        column: x => x.syrio,
                        principalTable: "Syrios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zakupka_syrias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    syrio = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employee = table.Column<int>(type: "int", nullable: false),
                    check = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zakupka_syrias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zakupka_syrias_Employees_employee",
                        column: x => x.employee,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zakupka_syrias_Syrios_syrio",
                        column: x => x.syrio,
                        principalTable: "Syrios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_doljnost",
                table: "Employees",
                column: "doljnost");

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_syrio",
                table: "Ingridients",
                column: "syrio");

            migrationBuilder.CreateIndex(
                name: "IX_Prodaja_produkcis_employee",
                table: "Prodaja_produkcis",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_Prodaja_produkcis_produkcia",
                table: "Prodaja_produkcis",
                column: "produkcia");

            migrationBuilder.CreateIndex(
                name: "IX_Produkcias_edinica_izmerenia",
                table: "Produkcias",
                column: "edinica_izmerenia");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodstvo_produkcis_employee",
                table: "Proizvodstvo_produkcis",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodstvo_produkcis_produkcia",
                table: "Proizvodstvo_produkcis",
                column: "produkcia");

            migrationBuilder.CreateIndex(
                name: "IX_Syrios_edinica_izmerenia",
                table: "Syrios",
                column: "edinica_izmerenia");

            migrationBuilder.CreateIndex(
                name: "IX_Zakupka_syrias_employee",
                table: "Zakupka_syrias",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_Zakupka_syrias_syrio",
                table: "Zakupka_syrias",
                column: "syrio");

            migrationBuilder.CreateIndex(
                name: "IX_Zarplatas_employee",
                table: "Zarplatas",
                column: "employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budjets");

            migrationBuilder.DropTable(
                name: "Ingridients");

            migrationBuilder.DropTable(
                name: "Prodaja_produkcis");

            migrationBuilder.DropTable(
                name: "Proizvodstvo_produkcis");

            migrationBuilder.DropTable(
                name: "Zakupka_syrias");

            migrationBuilder.DropTable(
                name: "Zarplatas");

            migrationBuilder.DropTable(
                name: "Produkcias");

            migrationBuilder.DropTable(
                name: "Syrios");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Edinica_izmerenias");

            migrationBuilder.DropTable(
                name: "Doljnosts");
        }
    }
}
