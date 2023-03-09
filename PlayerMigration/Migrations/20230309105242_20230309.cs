using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerMigration.Migrations
{
    public partial class _20230309 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defence",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementGauge",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementType",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attack",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "Defence",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "ElementGauge",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "ElementType",
                table: "AttributeInfos");
        }
    }
}
