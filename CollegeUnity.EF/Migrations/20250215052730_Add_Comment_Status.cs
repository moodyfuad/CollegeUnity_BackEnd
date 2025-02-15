using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeUnity.EF.Migrations
{
    /// <inheritdoc />
    public partial class Add_Comment_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PostComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PostComments");
        }
    }
}
