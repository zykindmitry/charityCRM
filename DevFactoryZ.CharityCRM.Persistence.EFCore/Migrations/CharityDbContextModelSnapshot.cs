﻿// <auto-generated />
using System;
using DevFactoryZ.CharityCRM.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Migrations
{
    [DbContext(typeof(CharityDbContext))]
    partial class CharityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Commodity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CommodityDonationId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommodityDonationId");

                    b.ToTable("Commodities");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Donation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Donations");

                    b.HasDiscriminator<string>("DonationType").HasValue("Donation");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.FundRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("MaxLifeTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("SucceededAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FundRegistration");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Role+RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.CashDonation", b =>
                {
                    b.HasBaseType("DevFactoryZ.CharityCRM.Donation");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("CashDonation");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.CommodityDonation", b =>
                {
                    b.HasBaseType("DevFactoryZ.CharityCRM.Donation");

                    b.HasDiscriminator().HasValue("CommodityDonation");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Commodity", b =>
                {
                    b.HasOne("DevFactoryZ.CharityCRM.CommodityDonation", null)
                        .WithMany("Commodities")
                        .HasForeignKey("CommodityDonationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Role+RolePermission", b =>
                {
                    b.HasOne("DevFactoryZ.CharityCRM.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevFactoryZ.CharityCRM.Role", null)
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
