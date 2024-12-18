using Microsoft.EntityFrameworkCore.Migrations;

public partial class stringg : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Drop the old column
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

        // Add any foreign key constraints or other necessary actions here
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Drop the new column
        migrationBuilder.DropColumn(
            name: "Id",
            table: "Favorites");

        // Recreate the old column with string type
        migrationBuilder.AddColumn<string>(
            name: "Id",
            table: "Favorites",
            type: "nvarchar(450)",
            nullable: false);
    }
}
