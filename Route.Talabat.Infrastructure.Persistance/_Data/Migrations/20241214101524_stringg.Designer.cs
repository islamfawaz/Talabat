using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route.Talabat.Infrastructure.Persistance.Migrations
{
    public partial class stringg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the old column (with a string type)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorites");

            // Add the new column with Identity property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Favorites",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Ensure that any other constraints or indexes are re-applied, e.g., foreign keys, etc.
            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FoodId",
                table: "Favorites",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_FoodItems_FoodId",
                table: "Favorites",
                column: "FoodId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorites");

            // Add the old column back (with a string type)
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: false);

            // Recreate the foreign key relationship and index
            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FoodId",
                table: "Favorites",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_FoodItems_FoodId",
                table: "Favorites",
                column: "FoodId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
