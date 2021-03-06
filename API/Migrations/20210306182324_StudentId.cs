using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class StudentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Replies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_StudentId",
                table: "Replies",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_StudentId",
                table: "Replies",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_StudentId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_StudentId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Replies");
        }
    }
}
