﻿// <auto-generated />
using MemorySignal.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataMigrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231008090614_InitSqlite")]
    partial class InitSqlite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("CardCardCollection", b =>
                {
                    b.Property<int>("CardCollectionsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CardCollectionsId", "CardsId");

                    b.HasIndex("CardsId");

                    b.ToTable("CardCardCollection");
                });

            modelBuilder.Entity("MemorySignal.Core.Data.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ApiId");

                    b.ToTable("Cards", (string)null);
                });

            modelBuilder.Entity("MemorySignal.Core.Data.Models.CardCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("CardCollections");
                });

            modelBuilder.Entity("CardCardCollection", b =>
                {
                    b.HasOne("MemorySignal.Core.Data.Models.CardCollection", null)
                        .WithMany()
                        .HasForeignKey("CardCollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemorySignal.Core.Data.Models.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
