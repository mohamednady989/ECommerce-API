using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProductRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_productBrands_ProductBrandsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductBrandsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductBrandsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypesId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_productBrands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "productBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_TypeId",
                table: "Products",
                column: "TypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_productBrands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_TypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TypeId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductBrandsId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypesId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandsId",
                table: "Products",
                column: "ProductBrandsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypesId",
                table: "Products",
                column: "ProductTypesId");
        }
    }
}
