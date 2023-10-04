using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageNameColumnForUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Users",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Users",
                newName: "ImageUrl");
        }
    }
}
