using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Peyment.Persistence.Database.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Makaya");

            migrationBuilder.CreateTable(
                name: "Pruducts",
                schema: "Makaya",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    urlimg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pruducts", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Subscribes",
                schema: "Makaya",
                columns: table => new
                {
                    SubscribeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanPriceSID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribeStripeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idCardStripe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    countscreen = table.Column<int>(type: "int", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => x.SubscribeId);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "Makaya",
                columns: table => new
                {
                    PlanId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    idplanstripe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypePlan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PlanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    PruductProductId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_Plans_Pruducts_PruductProductId",
                        column: x => x.PruductProductId,
                        principalSchema: "Makaya",
                        principalTable: "Pruducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlanId",
                schema: "Makaya",
                table: "Plans",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PruductProductId",
                schema: "Makaya",
                table: "Plans",
                column: "PruductProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Pruducts_ProductId",
                schema: "Makaya",
                table: "Pruducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_SubscribeId",
                schema: "Makaya",
                table: "Subscribes",
                column: "SubscribeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans",
                schema: "Makaya");

            migrationBuilder.DropTable(
                name: "Subscribes",
                schema: "Makaya");

            migrationBuilder.DropTable(
                name: "Pruducts",
                schema: "Makaya");
        }
    }
}
