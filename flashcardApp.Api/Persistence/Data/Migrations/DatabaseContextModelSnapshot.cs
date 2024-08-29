﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using flashcardApp.Persistence;

#nullable disable

namespace flashcardApp.Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("flashcardApp.Models.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("NumberOfCards")
                        .HasColumnType("int");

                    b.Property<decimal?>("Stars")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Topic")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("flashcardApp.Models.Flashcard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Backside")
                        .HasColumnType("longtext");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<string>("Frontside")
                        .HasColumnType("longtext");

                    b.Property<string>("SubTopic")
                        .HasColumnType("longtext");

                    b.Property<int>("flashcardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.HasIndex("flashcardId");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("flashcardApp.Models.Flashcard", b =>
                {
                    b.HasOne("flashcardApp.Models.Deck", null)
                        .WithMany("Flashcards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("flashcardApp.Models.Flashcard", "flashcard")
                        .WithMany()
                        .HasForeignKey("flashcardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("flashcard");
                });

            modelBuilder.Entity("flashcardApp.Models.Deck", b =>
                {
                    b.Navigation("Flashcards");
                });
#pragma warning restore 612, 618
        }
    }
}