﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Peyment.Persistence.Database;

namespace Peyment.Persistence.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Makaya")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payments.Domain.Plan", b =>
                {
                    b.Property<long>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("PlanDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<long?>("PruductProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TypePlan")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("idProduct")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idplanstripe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlanId");

                    b.HasIndex("PlanId");

                    b.HasIndex("PruductProductId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Payments.Domain.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("urlimg")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Pruducts");
                });

            modelBuilder.Entity("Payments.Domain.Subscribe", b =>
                {
                    b.Property<string>("SubscribeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanPriceSID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubscribeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscribeStripeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("countscreen")
                        .HasColumnType("int");

                    b.Property<string>("idCardStripe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.HasKey("SubscribeId");

                    b.HasIndex("SubscribeId");

                    b.ToTable("Subscribes");
                });

            modelBuilder.Entity("Payments.Domain.Plan", b =>
                {
                    b.HasOne("Payments.Domain.Product", "Pruduct")
                        .WithMany()
                        .HasForeignKey("PruductProductId");

                    b.Navigation("Pruduct");
                });
#pragma warning restore 612, 618
        }
    }
}