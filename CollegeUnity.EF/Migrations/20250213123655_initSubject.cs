using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeUnity.EF.Migrations
{
    /// <inheritdoc />
    public partial class initSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubjectId",
                table: "Posts",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Subjects_SubjectId",
                table: "Posts",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Subjects_SubjectId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubjectId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Posts");
        }
    }
}
