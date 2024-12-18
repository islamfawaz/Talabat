using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route.Talabat.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class @string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            // Step 2: Add a new column with the desired type
            migrationBuilder.AddColumn<string>(
                name: "NewId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // Step 3: Copy data from the old column to the new column
            migrationBuilder.Sql("UPDATE Favorites SET NewId = CAST(Id AS NVARCHAR(450))");

            // Step 4: Drop the old column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorites");

            // Step 5: Rename the new column to match the old column name
            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "Favorites",
                newName: "Id");

            // Step 6: Add the primary key constraint back on the new column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            // Step 2: Add the old column back with its original type
            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Step 3: Copy data from the new column back to the old column
            migrationBuilder.Sql("UPDATE Favorites SET OldId = CAST(Id AS INT)");

            // Step 4: Drop the new column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorites");

            // Step 5: Rename the old column to match the original column name
            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "Favorites",
                newName: "Id");

            // Step 6: Add the primary key constraint back on the old column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");
        }

    }
}
