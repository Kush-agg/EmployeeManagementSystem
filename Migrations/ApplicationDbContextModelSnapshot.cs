﻿// <auto-generated />
using System;
using EMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.14");

            modelBuilder.Entity("EMS.Models.Employee", b =>
                {
                    b.Property<int>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("dateOfJoining")
                        .HasColumnType("TEXT");

                    b.Property<string>("designation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("employeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EMS.Models.EmployeeSkill", b =>
                {
                    b.Property<int>("employeeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("skillId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("employeeSkillId")
                        .HasColumnType("TEXT");

                    b.Property<int>("experience")
                        .HasColumnType("INTEGER");

                    b.Property<string>("level")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("employeeId", "skillId");

                    b.HasIndex("skillId");

                    b.ToTable("EmployeeSkills");
                });

            modelBuilder.Entity("EMS.Models.Login", b =>
                {
                    b.Property<int>("loginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("loginId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("EMS.Models.Skill", b =>
                {
                    b.Property<int>("skillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("skillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("EMS.Models.EmployeeSkill", b =>
                {
                    b.HasOne("EMS.Models.Employee", "employee")
                        .WithMany("employeeSkills")
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMS.Models.Skill", "skill")
                        .WithMany("employeeSkills")
                        .HasForeignKey("skillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");

                    b.Navigation("skill");
                });

            modelBuilder.Entity("EMS.Models.Employee", b =>
                {
                    b.Navigation("employeeSkills");
                });

            modelBuilder.Entity("EMS.Models.Skill", b =>
                {
                    b.Navigation("employeeSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
