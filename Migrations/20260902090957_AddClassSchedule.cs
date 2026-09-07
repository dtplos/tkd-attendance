using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TkdAttendance.Migrations
{
    /// <inheritdoc />
    public partial class AddClassSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassDefinitionId",
                table: "Students",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassDefinitionId",
                table: "ClassSessions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassDefinitions_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassDefinitionId",
                table: "Students",
                column: "ClassDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSessions_ClassDefinitionId",
                table: "ClassSessions",
                column: "ClassDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDefinitions_BranchId",
                table: "ClassDefinitions",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSessions_ClassDefinitions_ClassDefinitionId",
                table: "ClassSessions",
                column: "ClassDefinitionId",
                principalTable: "ClassDefinitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassDefinitions_ClassDefinitionId",
                table: "Students",
                column: "ClassDefinitionId",
                principalTable: "ClassDefinitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSessions_ClassDefinitions_ClassDefinitionId",
                table: "ClassSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassDefinitions_ClassDefinitionId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "ClassDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassDefinitionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_ClassSessions_ClassDefinitionId",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "ClassDefinitionId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassDefinitionId",
                table: "ClassSessions");
        }
    }
}
