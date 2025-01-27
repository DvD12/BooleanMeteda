using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M015_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class StudentEmailNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_student_Email",
                table: "student");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "student",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_student_Email",
                table: "student",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_student_Email",
                table: "student");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_Email",
                table: "student",
                column: "Email",
                unique: true);
        }
    }
}
