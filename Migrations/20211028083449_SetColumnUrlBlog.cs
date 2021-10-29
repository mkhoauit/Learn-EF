using Microsoft.EntityFrameworkCore.Migrations;

namespace EFGetStarted.Migrations
{
    public partial class SetColumnUrlBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blogs",
                type: "TEXT",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Blogs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "Index_Url",
                table: "Blogs",
                column: "Url");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Url",
                table: "Blogs",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Url",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_Url",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blogs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 25);
        }
    }
}
