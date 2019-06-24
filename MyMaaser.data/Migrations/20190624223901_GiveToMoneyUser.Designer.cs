﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyMaaser.data;

namespace MyMaaser.data.Migrations
{
    [DbContext(typeof(MaaserContext))]
    [Migration("20190624223901_GiveToMoneyUser")]
    partial class GiveToMoneyUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyMaaser.data.GiveToMoney", b =>
                {
                    b.Property<int>("MoneyId");

                    b.Property<int>("MaaserGivenId");

                    b.Property<int>("UserId");

                    b.HasKey("MoneyId", "MaaserGivenId");

                    b.HasIndex("MaaserGivenId");

                    b.ToTable("GiveToMoney");
                });

            modelBuilder.Entity("MyMaaser.data.MaaserGiven", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("ToWhere");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MaaserGiven");
                });

            modelBuilder.Entity("MyMaaser.data.MoneyEarned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<decimal>("AmountLeft");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("PaidUp");

                    b.Property<string>("RecievedFrom");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MoneyEarned");
                });

            modelBuilder.Entity("MyMaaser.data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HashedPassword");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyMaaser.data.GiveToMoney", b =>
                {
                    b.HasOne("MyMaaser.data.MaaserGiven", "MaaserGiven")
                        .WithMany("GiveToMoney")
                        .HasForeignKey("MaaserGivenId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyMaaser.data.MoneyEarned", "Money")
                        .WithMany("GiveToMoney")
                        .HasForeignKey("MoneyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyMaaser.data.MaaserGiven", b =>
                {
                    b.HasOne("MyMaaser.data.User", "User")
                        .WithMany("MaaserGiven")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyMaaser.data.MoneyEarned", b =>
                {
                    b.HasOne("MyMaaser.data.User", "User")
                        .WithMany("MoneyEarned")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}