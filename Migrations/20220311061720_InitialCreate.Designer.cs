// <auto-generated />
using System;
using CNaturalApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CNaturalApi.Migrations
{
    [DbContext(typeof(CNaturalContext))]
    [Migration("20220311061720_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("CNaturalApi.Models.Accountancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("EarnedMoney")
                        .HasColumnType("REAL");

                    b.Property<double>("InvestedMoney")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Accountancies");
                });

            modelBuilder.Entity("CNaturalApi.Models.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("CNaturalApi.Models.Investment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountancyId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TaskDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountancyId");

                    b.HasIndex("ProductId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("CNaturalApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Design")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CNaturalApi.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountancyId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountancyId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("CNaturalApi.Models.Investment", b =>
                {
                    b.HasOne("CNaturalApi.Models.Accountancy", "Accountancy")
                        .WithMany("Investments")
                        .HasForeignKey("AccountancyId");

                    b.HasOne("CNaturalApi.Models.Product", "Product")
                        .WithMany("Investments")
                        .HasForeignKey("ProductId");

                    b.Navigation("Accountancy");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CNaturalApi.Models.Sale", b =>
                {
                    b.HasOne("CNaturalApi.Models.Accountancy", "Accountancy")
                        .WithMany("Sales")
                        .HasForeignKey("AccountancyId");

                    b.HasOne("CNaturalApi.Models.Buyer", "Buyer")
                        .WithMany("Sales")
                        .HasForeignKey("BuyerId");

                    b.HasOne("CNaturalApi.Models.Product", "Product")
                        .WithMany("Sales")
                        .HasForeignKey("ProductId");

                    b.Navigation("Accountancy");

                    b.Navigation("Buyer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CNaturalApi.Models.Accountancy", b =>
                {
                    b.Navigation("Investments");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("CNaturalApi.Models.Buyer", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("CNaturalApi.Models.Product", b =>
                {
                    b.Navigation("Investments");

                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
