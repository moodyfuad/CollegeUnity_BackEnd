using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeUnity.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeedbackEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedBackStatus",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedBackStatus",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "Feedbacks");
        }
    }
}
