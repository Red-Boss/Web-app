﻿// <auto-generated />
using System;
using MebelWeb.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebSMDB_true_.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240227155809_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MebelWeb.Models.Budjet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("bonus")
                        .HasColumnType("int");

                    b.Property<decimal>("budjet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("pr_prodaji")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Budjets");
                });

            modelBuilder.Entity("MebelWeb.Models.Doljnost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("name_doljnost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Doljnosts");
                });

            modelBuilder.Entity("MebelWeb.Models.Edinica_izmerenia", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Naimenovanie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Edinica_izmerenias");
                });

            modelBuilder.Entity("MebelWeb.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Oklad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("doljnost")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("doljnost");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MebelWeb.Models.Ingridient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("produkcia")
                        .HasColumnType("int");

                    b.Property<int>("syrio")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("syrio");

                    b.ToTable("Ingridients");
                });

            modelBuilder.Entity("MebelWeb.Models.Prodaja_produkci", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("check")
                        .HasColumnType("bit");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.Property<int>("employee")
                        .HasColumnType("int");

                    b.Property<int>("produkcia")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("employee");

                    b.HasIndex("produkcia");

                    b.ToTable("Prodaja_produkcis");
                });

            modelBuilder.Entity("MebelWeb.Models.Produkcia", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Naimenovanie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("edinica_izmerenia")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("edinica_izmerenia");

                    b.ToTable("Produkcias");
                });

            modelBuilder.Entity("MebelWeb.Models.Proizvodstvo_produkci", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("check")
                        .HasColumnType("bit");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.Property<int>("employee")
                        .HasColumnType("int");

                    b.Property<int>("produkcia")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("employee");

                    b.HasIndex("produkcia");

                    b.ToTable("Proizvodstvo_produkcis");
                });

            modelBuilder.Entity("MebelWeb.Models.Syrio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Naimenovanie_materiala")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("edinica_izmerenia")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("edinica_izmerenia");

                    b.ToTable("Syrios");
                });

            modelBuilder.Entity("MebelWeb.Models.Zakupka_syria", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("check")
                        .HasColumnType("bit");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.Property<int>("employee")
                        .HasColumnType("int");

                    b.Property<int>("syrio")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("employee");

                    b.HasIndex("syrio");

                    b.ToTable("Zakupka_syrias");
                });

            modelBuilder.Entity("MebelWeb.Models.Zarplata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Bonus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Common")
                        .HasColumnType("int");

                    b.Property<int>("ForProduction")
                        .HasColumnType("int");

                    b.Property<int>("ForPurchase")
                        .HasColumnType("int");

                    b.Property<int>("ForSale")
                        .HasColumnType("int");

                    b.Property<decimal>("General")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<int?>("employee")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("given")
                        .HasColumnType("bit");

                    b.Property<decimal>("oklad")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("employee");

                    b.ToTable("Zarplatas");
                });

            modelBuilder.Entity("MebelWeb.Models.Employee", b =>
                {
                    b.HasOne("MebelWeb.Models.Doljnost", "Doljnost")
                        .WithMany("Employee")
                        .HasForeignKey("doljnost")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doljnost");
                });

            modelBuilder.Entity("MebelWeb.Models.Ingridient", b =>
                {
                    b.HasOne("MebelWeb.Models.Syrio", "Syrio")
                        .WithMany("Ingridient")
                        .HasForeignKey("syrio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Syrio");
                });

            modelBuilder.Entity("MebelWeb.Models.Prodaja_produkci", b =>
                {
                    b.HasOne("MebelWeb.Models.Employee", "Employee")
                        .WithMany("Prodaja_produkci")
                        .HasForeignKey("employee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MebelWeb.Models.Produkcia", "Produkcia")
                        .WithMany("Prodaja_produkci")
                        .HasForeignKey("produkcia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Produkcia");
                });

            modelBuilder.Entity("MebelWeb.Models.Produkcia", b =>
                {
                    b.HasOne("MebelWeb.Models.Edinica_izmerenia", "Edinica_izmerenia")
                        .WithMany("Produkcia")
                        .HasForeignKey("edinica_izmerenia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Edinica_izmerenia");
                });

            modelBuilder.Entity("MebelWeb.Models.Proizvodstvo_produkci", b =>
                {
                    b.HasOne("MebelWeb.Models.Employee", "Employee")
                        .WithMany("Proizvodstvo_produkci")
                        .HasForeignKey("employee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MebelWeb.Models.Produkcia", "Produkcia")
                        .WithMany("Proizvodstvo_produkci")
                        .HasForeignKey("produkcia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Produkcia");
                });

            modelBuilder.Entity("MebelWeb.Models.Syrio", b =>
                {
                    b.HasOne("MebelWeb.Models.Edinica_izmerenia", "Edinica_izmerenia")
                        .WithMany("Syrio")
                        .HasForeignKey("edinica_izmerenia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Edinica_izmerenia");
                });

            modelBuilder.Entity("MebelWeb.Models.Zakupka_syria", b =>
                {
                    b.HasOne("MebelWeb.Models.Employee", "Employee")
                        .WithMany("Zakupka_syria")
                        .HasForeignKey("employee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MebelWeb.Models.Syrio", "Syrio")
                        .WithMany("Zakupka_syria")
                        .HasForeignKey("syrio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Syrio");
                });

            modelBuilder.Entity("MebelWeb.Models.Zarplata", b =>
                {
                    b.HasOne("MebelWeb.Models.Employee", "Employee")
                        .WithMany("Zarplata")
                        .HasForeignKey("employee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MebelWeb.Models.Doljnost", b =>
                {
                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MebelWeb.Models.Edinica_izmerenia", b =>
                {
                    b.Navigation("Produkcia");

                    b.Navigation("Syrio");
                });

            modelBuilder.Entity("MebelWeb.Models.Employee", b =>
                {
                    b.Navigation("Prodaja_produkci");

                    b.Navigation("Proizvodstvo_produkci");

                    b.Navigation("Zakupka_syria");

                    b.Navigation("Zarplata");
                });

            modelBuilder.Entity("MebelWeb.Models.Produkcia", b =>
                {
                    b.Navigation("Prodaja_produkci");

                    b.Navigation("Proizvodstvo_produkci");
                });

            modelBuilder.Entity("MebelWeb.Models.Syrio", b =>
                {
                    b.Navigation("Ingridient");

                    b.Navigation("Zakupka_syria");
                });
#pragma warning restore 612, 618
        }
    }
}