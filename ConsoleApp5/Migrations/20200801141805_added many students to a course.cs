using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp5.Migrations
{
    public partial class addedmanystudentstoacourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentPocoId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentPocoId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentPocoId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "Students_Courses",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students_Courses", x => new { x.CourseId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_Students_Courses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "MumboJumbo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Courses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Courses_StudentId",
                table: "Students_Courses",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students_Courses");

            migrationBuilder.AddColumn<int>(
                name: "StudentPocoId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentPocoId",
                table: "Courses",
                column: "StudentPocoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentPocoId",
                table: "Courses",
                column: "StudentPocoId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
