﻿// <auto-generated />
using System;
using Hoshmand.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hoshmand.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230730163629_change-rawrepose")]
    partial class changerawrepose
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hoshmand.Core.Entities.CheckCodeRequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageCodeInput")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderRequestId")
                        .HasColumnType("int");

                    b.Property<string>("RawResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResponsDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderRequestId");

                    b.ToTable("CheckCodeRequests");
                });

            modelBuilder.Entity("Hoshmand.Core.Entities.IdCardRequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageId1")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("ImageId2")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("OrderRequestId")
                        .HasColumnType("int");

                    b.Property<string>("RawResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResponsDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderRequestId");

                    b.ToTable("IdCardRequests");
                });

            modelBuilder.Entity("Hoshmand.Core.Entities.NumPhoneRequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResponsDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderRequestId");

                    b.ToTable("NumPhoneRequests");
                });

            modelBuilder.Entity("Hoshmand.Core.Entities.OrderRequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResponsDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("OrderRequests");
                });

            modelBuilder.Entity("Hoshmand.Core.Entities.CheckCodeRequestEntity", b =>
                {
                    b.HasOne("Hoshmand.Core.Entities.OrderRequestEntity", "OrderRequest")
                        .WithMany()
                        .HasForeignKey("OrderRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderRequest");
                });

            modelBuilder.Entity("Hoshmand.Core.Entities.IdCardRequestEntity", b =>
                {
                    b.HasOne("Hoshmand.Core.Entities.OrderRequestEntity", "OrderRequest")
                        .WithMany()
                        .HasForeignKey("OrderRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderRequest");
                });

            modelBuilder.Entity("Hoshmand.Core.Entities.NumPhoneRequestEntity", b =>
                {
                    b.HasOne("Hoshmand.Core.Entities.OrderRequestEntity", "OrderRequest")
                        .WithMany()
                        .HasForeignKey("OrderRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderRequest");
                });
#pragma warning restore 612, 618
        }
    }
}
