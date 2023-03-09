using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlayerMigration.Migrations
{
    public partial class _20230308 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnhanceStoneInfos");

            migrationBuilder.DropIndex(
                name: "IX_WeaponInfos_PlayerId",
                table: "WeaponInfos");

            migrationBuilder.DropIndex(
                name: "IX_AttributeInfos_PlayerId",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "ElementAttack",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementDefence",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementEnhanceStone",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementType",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "PhysicDefence",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "PhysicEnhanceStone",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "UnbreakRecoverTimes",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "UnbreakValue",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "WeaponIndex",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "WeaponValue",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Coin",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "EP",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "EPFull",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "Resin",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "ResinFull",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "AttributeInfos");

            migrationBuilder.DropColumn(
                name: "StaminaFull",
                table: "AttributeInfos");

            migrationBuilder.CreateTable(
                name: "ArtifactInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArtifactName = table.Column<int>(nullable: false),
                    Attack = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtifactInfos_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeaponInfos_PlayerId",
                table: "WeaponInfos",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeInfos_PlayerId",
                table: "AttributeInfos",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactInfos_PlayerId",
                table: "ArtifactInfos",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtifactInfos");

            migrationBuilder.DropIndex(
                name: "IX_WeaponInfos_PlayerId",
                table: "WeaponInfos");

            migrationBuilder.DropIndex(
                name: "IX_AttributeInfos_PlayerId",
                table: "AttributeInfos");

            migrationBuilder.AddColumn<int>(
                name: "ElementAttack",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementDefence",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementEnhanceStone",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementType",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicDefence",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicEnhanceStone",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnbreakRecoverTimes",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "UnbreakValue",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "WeaponIndex",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeaponValue",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Coin",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EP",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EPFull",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Resin",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResinFull",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StaminaFull",
                table: "AttributeInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnhanceStoneInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElementAttackEnhance = table.Column<int>(nullable: false),
                    ElementDefenceEnhance = table.Column<int>(nullable: false),
                    ElementEnhanceStoneName = table.Column<int>(nullable: false),
                    ElementStoneRarity = table.Column<int>(nullable: false),
                    ElementType = table.Column<int>(nullable: false),
                    PhysicAttackEnhance = table.Column<int>(nullable: false),
                    PhysicDefenceEnhance = table.Column<int>(nullable: false),
                    PhysicEnhanceStoneName = table.Column<int>(nullable: false),
                    PhysicStoneRarity = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnhanceStoneInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnhanceStoneInfos_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeaponInfos_PlayerId",
                table: "WeaponInfos",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeInfos_PlayerId",
                table: "AttributeInfos",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnhanceStoneInfos_PlayerId",
                table: "EnhanceStoneInfos",
                column: "PlayerId",
                unique: true);
        }
    }
}
