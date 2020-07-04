﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGCC_API.Repository;

namespace SGCC_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SGCC_API.Model.Empresa", b =>
                {
                    b.Property<int>("IdEmpresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AgenciaBancaria")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Cnpj")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ContaBancaria")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NomeReal")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("IdEmpresa");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("SGCC_API.Model.Local", b =>
                {
                    b.Property<int>("IdLocal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Andar")
                        .HasColumnType("int");

                    b.Property<int?>("LocadorIdEmpresa")
                        .HasColumnType("int");

                    b.Property<int?>("LocatarioIdEmpresa")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<float>("TamanhoM2")
                        .HasColumnType("float");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("IdLocal");

                    b.HasIndex("LocadorIdEmpresa");

                    b.HasIndex("LocatarioIdEmpresa");

                    b.ToTable("Locais");
                });

            modelBuilder.Entity("SGCC_API.Model.Visitante", b =>
                {
                    b.Property<int>("IdVisitante")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataNasc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TipoPessoa")
                        .HasColumnType("int");

                    b.HasKey("IdVisitante");

                    b.ToTable("Visitantes");
                });

            modelBuilder.Entity("SGCC_API.Model.Local", b =>
                {
                    b.HasOne("SGCC_API.Model.Empresa", "Locador")
                        .WithMany()
                        .HasForeignKey("LocadorIdEmpresa");

                    b.HasOne("SGCC_API.Model.Empresa", "Locatario")
                        .WithMany()
                        .HasForeignKey("LocatarioIdEmpresa");
                });
#pragma warning restore 612, 618
        }
    }
}