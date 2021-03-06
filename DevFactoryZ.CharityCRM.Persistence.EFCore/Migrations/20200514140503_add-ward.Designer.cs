﻿// <auto-generated />
using System;
using DevFactoryZ.CharityCRM.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Migrations
{
    [DbContext(typeof(CharityDbContext))]
    [Migration("20200514140503_add-ward")]
    partial class addward
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.AccountSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AccountId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountSession");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Commodity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<long?>("DonationId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonationId");

                    b.ToTable("Commodity");
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

                    b.ToTable("Donation");

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

            modelBuilder.Entity("DevFactoryZ.CharityCRM.PasswordConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxLifeTime")
                        .HasColumnType("int");

                    b.Property<int>("MinLength")
                        .HasColumnType("int");

                    b.Property<int>("SaltLength")
                        .HasColumnType("int");

                    b.Property<string>("SpecialSymbols")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UseDigits")
                        .HasColumnType("bit");

                    b.Property<bool>("UseSpecialSymbols")
                        .HasColumnType("bit");

                    b.Property<bool>("UseUpperCase")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("PasswordConfig");
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

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Ward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("Ward");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Ward+WardCategoryCollectionElement", b =>
                {
                    b.Property<int>("WardId")
                        .HasColumnType("int");

                    b.Property<int>("WardCategoryId")
                        .HasColumnType("int");

                    b.HasKey("WardId", "WardCategoryId");

                    b.HasIndex("WardCategoryId");

                    b.ToTable("WardCategories");
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.WardCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("WardCategory");
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

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Account", b =>
                {
                    b.OwnsOne("DevFactoryZ.CharityCRM.Password", "Password", b1 =>
                        {
                            b1.Property<int>("AccountId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime?>("ChangedAt")
                                .HasColumnType("datetime2");

                            b1.Property<int>("PasswordConfigId")
                                .HasColumnType("int");

                            b1.Property<byte[]>("RawHash")
                                .IsRequired()
                                .HasColumnType("varbinary(max)");

                            b1.Property<byte[]>("RawSalt")
                                .IsRequired()
                                .HasColumnType("varbinary(max)");

                            b1.HasKey("AccountId");

                            b1.HasIndex("PasswordConfigId");

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");

                            b1.HasOne("DevFactoryZ.CharityCRM.PasswordConfig", "PasswordConfig")
                                .WithMany()
                                .HasForeignKey("PasswordConfigId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();
                        });
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.AccountSession", b =>
                {
                    b.HasOne("DevFactoryZ.CharityCRM.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Commodity", b =>
                {
                    b.HasOne("DevFactoryZ.CharityCRM.CommodityDonation", null)
                        .WithMany("Commodities")
                        .HasForeignKey("DonationId")
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

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Ward", b =>
                {
                    b.OwnsOne("DevFactoryZ.CharityCRM.Address", "Address", b1 =>
                        {
                            b1.Property<int>("WardId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Area")
                                .HasColumnName("Area")
                                .HasColumnType("nvarchar(50)")
                                .HasMaxLength(50);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnName("City")
                                .HasColumnType("nvarchar(50)")
                                .HasMaxLength(50);

                            b1.Property<string>("Country")
                                .HasColumnName("Country")
                                .HasColumnType("nvarchar(50)")
                                .HasMaxLength(50);

                            b1.Property<string>("Flat")
                                .HasColumnName("Flat")
                                .HasColumnType("nvarchar(10)")
                                .HasMaxLength(10);

                            b1.Property<string>("House")
                                .HasColumnName("House")
                                .HasColumnType("nvarchar(30)")
                                .HasMaxLength(30);

                            b1.Property<string>("PostCode")
                                .HasColumnName("PostCode")
                                .HasColumnType("nvarchar(6)")
                                .HasMaxLength(6);

                            b1.Property<string>("Region")
                                .HasColumnName("Region")
                                .HasColumnType("nvarchar(50)")
                                .HasMaxLength(50);

                            b1.Property<string>("Street")
                                .HasColumnName("Street")
                                .HasColumnType("nvarchar(50)")
                                .HasMaxLength(50);

                            b1.HasKey("WardId");

                            b1.ToTable("Ward");

                            b1.WithOwner()
                                .HasForeignKey("WardId");
                        });

                    b.OwnsOne("DevFactoryZ.CharityCRM.FullName", "FullName", b1 =>
                        {
                            b1.Property<int>("WardId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnName("FirstName")
                                .HasColumnType("nvarchar(30)")
                                .HasMaxLength(30);

                            b1.Property<string>("MiddleName")
                                .HasColumnName("MiddleName")
                                .HasColumnType("nvarchar(30)")
                                .HasMaxLength(30);

                            b1.Property<string>("SurName")
                                .IsRequired()
                                .HasColumnName("SurName")
                                .HasColumnType("nvarchar(30)")
                                .HasMaxLength(30);

                            b1.HasKey("WardId");

                            b1.ToTable("Ward");

                            b1.WithOwner()
                                .HasForeignKey("WardId");
                        });
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.Ward+WardCategoryCollectionElement", b =>
                {
                    b.HasOne("DevFactoryZ.CharityCRM.WardCategory", "WardCategory")
                        .WithMany()
                        .HasForeignKey("WardCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DevFactoryZ.CharityCRM.Ward", null)
                        .WithMany("WardCategories")
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DevFactoryZ.CharityCRM.WardCategory", b =>
                {
                    b.HasOne("DevFactoryZ.CharityCRM.WardCategory", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
