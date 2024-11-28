using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palazon.Teste.Docker.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModifiedByToTaskHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_TasksId",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_TasksId",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "TaskHistories",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TaskHistories",
                newName: "PropertyName");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "TaskHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "TaskHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_ModifiedById",
                table: "TaskHistories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_TaskId",
                table: "TaskHistories",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Users_ModifiedById",
                table: "TaskHistories",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Users_ModifiedById",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_ModifiedById",
                table: "TaskHistories");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistories_TaskId",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "TaskHistories");

            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "TaskHistories");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "TaskHistories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "TaskHistories",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "TaskHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_TasksId",
                table: "TaskHistories",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_TasksId",
                table: "TaskHistories",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
