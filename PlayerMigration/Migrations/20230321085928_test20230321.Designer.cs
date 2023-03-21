﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayerMigration;
using SangoCommon.ArtifactCode;
using SangoCommon.GameObjectCode;
using SangoCommon.ElementCode;
using SangoCommon.StateCode;
using SangoCommon.ComBatCode;
using SangoCommon.WeaponCode;

namespace PlayerMigration.Migrations
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20230321085928_test20230321")]
    partial class test20230321
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlayerData.ArtifactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtifactName");

                    b.Property<int>("Attack");

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("ArtifactInfos");
                });

            modelBuilder.Entity("PlayerData.AttributeInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Attack");

                    b.Property<int>("Avater")
                        .HasMaxLength(64);

                    b.Property<int>("Defence");

                    b.Property<int>("ElementGauge");

                    b.Property<int>("ElementType");

                    b.Property<int>("HP");

                    b.Property<int>("HPFull");

                    b.Property<int>("MP");

                    b.Property<int>("MPFull");

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("AttributeInfos");
                });

            modelBuilder.Entity("PlayerData.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PlayerData.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("PlayerData.WeaponInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Experience");

                    b.Property<int>("Level");

                    b.Property<int>("PhysicAttack");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Rarity");

                    b.Property<int>("Skill");

                    b.Property<int>("WeaponName");

                    b.Property<int>("WeaponType");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("WeaponInfos");
                });

            modelBuilder.Entity("PlayerData.ArtifactInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithMany("ArtifactInfoList")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayerData.AttributeInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithMany("AttributeInfoList")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayerData.UserInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithOne("UserInfo")
                        .HasForeignKey("PlayerData.UserInfo", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayerData.WeaponInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithMany("WeaponInfoList")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
