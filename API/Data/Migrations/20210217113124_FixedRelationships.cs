using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class FixedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Courses_CourseId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseCourse",
                columns: table => new
                {
                    PreRequisiteToCourseId = table.Column<string>(type: "TEXT", nullable: false),
                    PreRequisitesCourseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourse", x => new { x.PreRequisiteToCourseId, x.PreRequisitesCourseId });
                    table.ForeignKey(
                        name: "FK_CourseCourse_Courses_PreRequisitesCourseId",
                        column: x => x.PreRequisitesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourse_Courses_PreRequisiteToCourseId",
                        column: x => x.PreRequisiteToCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourse_PreRequisitesCourseId",
                table: "CourseCourse",
                column: "PreRequisitesCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourse");

            migrationBuilder.AddColumn<string>(
                name: "CourseId1",
                table: "Courses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseId1",
                table: "Courses",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Courses_CourseId1",
                table: "Courses",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
