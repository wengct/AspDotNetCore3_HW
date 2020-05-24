using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HW01.Migrations
{
    public partial class isDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Person",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Person",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Department",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Department",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Course",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Course",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Course");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Person",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Department",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Course",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
