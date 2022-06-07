using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetTask2Single.Migrations
{
    public partial class img2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Skills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Skills");
        }
    }
}
