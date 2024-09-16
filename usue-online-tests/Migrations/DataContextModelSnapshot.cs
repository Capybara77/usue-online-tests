﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using usue_online_tests.Data;

#nullable disable

namespace usue_online_tests.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("usue_online_tests.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Group")
                        .HasColumnType("text");

                    b.Property<bool>("IsEnd")
                        .HasColumnType("boolean");

                    b.Property<int>("PresetId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PresetId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("usue_online_tests.Models.ExamTestAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CorrectAnswers")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TestId")
                        .HasColumnType("integer");

                    b.Property<int>("TotalAnswers")
                        .HasColumnType("integer");

                    b.Property<int>("UserExamResultId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserExamResultId");

                    b.ToTable("ExamTestAnswers");
                });

            modelBuilder.Entity("usue_online_tests.Models.PredictionCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<Guid>("PredictionResultId")
                        .HasColumnType("uuid");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PredictionResultId");

                    b.ToTable("PredictionCategories");
                });

            modelBuilder.Entity("usue_online_tests.Models.PredictionResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HeadIndex")
                        .HasColumnType("integer");

                    b.Property<string>("HeadName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PredictionResults");
                });

            modelBuilder.Entity("usue_online_tests.Models.TestPreset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsHomework")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int[]>("Tests")
                        .HasColumnType("integer[]");

                    b.Property<bool>("TimeLimited")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Presets");
                });

            modelBuilder.Entity("usue_online_tests.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Group")
                        .HasColumnType("text");

                    b.Property<bool>("IsDark")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("usue_online_tests.Models.UserExamResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExamId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExamResults");
                });

            modelBuilder.Entity("usue_online_tests.Models.Exam", b =>
                {
                    b.HasOne("usue_online_tests.Models.TestPreset", "Preset")
                        .WithMany()
                        .HasForeignKey("PresetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Preset");
                });

            modelBuilder.Entity("usue_online_tests.Models.ExamTestAnswer", b =>
                {
                    b.HasOne("usue_online_tests.Models.UserExamResult", "UserExamResult")
                        .WithMany("ExamTestAnswers")
                        .HasForeignKey("UserExamResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserExamResult");
                });

            modelBuilder.Entity("usue_online_tests.Models.PredictionCategory", b =>
                {
                    b.HasOne("usue_online_tests.Models.PredictionResult", "PredictionResult")
                        .WithMany("Categories")
                        .HasForeignKey("PredictionResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PredictionResult");
                });

            modelBuilder.Entity("usue_online_tests.Models.TestPreset", b =>
                {
                    b.HasOne("usue_online_tests.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("usue_online_tests.Models.UserExamResult", b =>
                {
                    b.HasOne("usue_online_tests.Models.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("usue_online_tests.Models.User", "User")
                        .WithMany("UserExamResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("usue_online_tests.Models.PredictionResult", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("usue_online_tests.Models.User", b =>
                {
                    b.Navigation("UserExamResults");
                });

            modelBuilder.Entity("usue_online_tests.Models.UserExamResult", b =>
                {
                    b.Navigation("ExamTestAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}