using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASM_01.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvModel",
                columns: table => new
                {
                    EvModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvModel", x => x.EvModelId);
                });

            migrationBuilder.CreateTable(
                name: "Spec",
                columns: table => new
                {
                    SpecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<int>(type: "int", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spec", x => x.SpecId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EvTrim",
                columns: table => new
                {
                    EvTrimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvModelId = table.Column<int>(type: "int", nullable: false),
                    TrimName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvTrim", x => x.EvTrimId);
                    table.ForeignKey(
                        name: "FK_EvTrim_EvModel_EvModelId",
                        column: x => x.EvModelId,
                        principalTable: "EvModel",
                        principalColumn: "EvModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    DealerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.DealerId);
                    table.ForeignKey(
                        name: "FK_Dealers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrimPrice",
                columns: table => new
                {
                    TrimPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvTrimId = table.Column<int>(type: "int", nullable: false),
                    ListedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrimPrice", x => x.TrimPriceId);
                    table.ForeignKey(
                        name: "FK_TrimPrice_EvTrim_EvTrimId",
                        column: x => x.EvTrimId,
                        principalTable: "EvTrim",
                        principalColumn: "EvTrimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrimSpec",
                columns: table => new
                {
                    EvTrimId = table.Column<int>(type: "int", nullable: false),
                    SpecId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrimSpec", x => new { x.EvTrimId, x.SpecId });
                    table.ForeignKey(
                        name: "FK_TrimSpec_EvTrim_EvTrimId",
                        column: x => x.EvTrimId,
                        principalTable: "EvTrim",
                        principalColumn: "EvTrimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrimSpec_Spec_SpecId",
                        column: x => x.SpecId,
                        principalTable: "Spec",
                        principalColumn: "SpecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributionRequests",
                columns: table => new
                {
                    DistributionRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    EvTrimId = table.Column<int>(type: "int", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    ApprovedQuantity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionRequests", x => x.DistributionRequestId);
                    table.ForeignKey(
                        name: "FK_DistributionRequests_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "DealerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionRequests_EvTrim_EvTrimId",
                        column: x => x.EvTrimId,
                        principalTable: "EvTrim",
                        principalColumn: "EvTrimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleStocks",
                columns: table => new
                {
                    VehicleStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    EvTrimId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleStocks", x => x.VehicleStockId);
                    table.ForeignKey(
                        name: "FK_VehicleStocks_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "DealerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleStocks_EvTrim_EvTrimId",
                        column: x => x.EvTrimId,
                        principalTable: "EvTrim",
                        principalColumn: "EvTrimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EvModel",
                columns: new[] { "EvModelId", "Description", "ModelName", "Status" },
                values: new object[,]
                {
                    { 1, "Mid-size all-electric SUV", "VinFast VF8", 1 },
                    { 2, "Compact SUV with long range", "Tesla Model Y", 1 }
                });

            migrationBuilder.InsertData(
                table: "Spec",
                columns: new[] { "SpecId", "Category", "SpecName", "Unit" },
                values: new object[,]
                {
                    { 1, 0, "Battery Capacity", "kWh" },
                    { 2, 0, "Range", "km" },
                    { 3, 1, "Motor Power", "hp" },
                    { 4, 2, "Charging Time (fast)", "minutes" },
                    { 5, 3, "Seating Capacity", "seats" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "LastLoginAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QmGJk7SKOTvDnRcZHcrFKV28WBJmOIKlHhrf906pTAA=", "DISTRIBUTOR", "distributor" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "51GXL9fbslr7yF7K4MS3GW7sOuQJGwfcJ/+CAwn7m0I=", "DEALER", "dealer" }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "DealerId", "Address", "Name", "UserId" },
                values: new object[] { 1, "New York, NY", "City EV Motors", 1 });

            migrationBuilder.InsertData(
                table: "EvTrim",
                columns: new[] { "EvTrimId", "Description", "EvModelId", "ModelYear", "TrimName" },
                values: new object[,]
                {
                    { 1, "Entry version", 1, 2024, "VF8 Eco" },
                    { 2, "Premium version", 1, 2024, "VF8 Plus" },
                    { 3, "Dual motor", 2, 2024, "Model Y Long Range" },
                    { 4, "High performance", 2, 2024, "Model Y Performance" }
                });

            migrationBuilder.InsertData(
                table: "TrimPrice",
                columns: new[] { "TrimPriceId", "EffectiveDate", "EvTrimId", "ListedPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 46000m },
                    { 2, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 47000m },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 52000m },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 55000m },
                    { 5, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 56000m },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 61000m }
                });

            migrationBuilder.InsertData(
                table: "TrimSpec",
                columns: new[] { "EvTrimId", "SpecId", "Value" },
                values: new object[,]
                {
                    { 1, 1, "82" },
                    { 1, 2, "420" },
                    { 1, 3, "350" },
                    { 1, 4, "35" },
                    { 1, 5, "5" },
                    { 2, 1, "87" },
                    { 2, 2, "470" },
                    { 2, 3, "402" },
                    { 2, 4, "30" },
                    { 2, 5, "5" },
                    { 3, 1, "82" },
                    { 3, 2, "497" },
                    { 3, 3, "384" },
                    { 3, 4, "27" },
                    { 3, 5, "5" },
                    { 4, 1, "82" },
                    { 4, 2, "450" },
                    { 4, 3, "456" },
                    { 4, 4, "25" },
                    { 4, 5, "5" }
                });

            migrationBuilder.InsertData(
                table: "VehicleStocks",
                columns: new[] { "VehicleStockId", "DealerId", "EvTrimId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 1, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionRequests_DealerId",
                table: "DistributionRequests",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionRequests_EvTrimId",
                table: "DistributionRequests",
                column: "EvTrimId");

            migrationBuilder.CreateIndex(
                name: "IX_EvTrim_EvModelId",
                table: "EvTrim",
                column: "EvModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Spec_SpecName",
                table: "Spec",
                column: "SpecName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrimPrice_EvTrimId",
                table: "TrimPrice",
                column: "EvTrimId");

            migrationBuilder.CreateIndex(
                name: "IX_TrimSpec_SpecId",
                table: "TrimSpec",
                column: "SpecId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleStocks_DealerId",
                table: "VehicleStocks",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleStocks_EvTrimId",
                table: "VehicleStocks",
                column: "EvTrimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributionRequests");

            migrationBuilder.DropTable(
                name: "TrimPrice");

            migrationBuilder.DropTable(
                name: "TrimSpec");

            migrationBuilder.DropTable(
                name: "VehicleStocks");

            migrationBuilder.DropTable(
                name: "Spec");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropTable(
                name: "EvTrim");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EvModel");
        }
    }
}
