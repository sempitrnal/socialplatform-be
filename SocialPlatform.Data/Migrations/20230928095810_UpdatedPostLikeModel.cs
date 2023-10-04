using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPostLikeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "PostLikes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PostLikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PostLikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "PostLikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
