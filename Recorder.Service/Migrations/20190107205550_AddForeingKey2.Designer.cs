﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recorder.Service;

namespace Recorder.Service.Migrations
{
    [DbContext(typeof(AppDatabaseContext))]
    [Migration("20190107205550_AddForeingKey2")]
    partial class AddForeingKey2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Recorder.Service.Entities.Camera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("MacAddress")
                        .HasMaxLength(17);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Cameras");
                });

            modelBuilder.Entity("Recorder.Service.Entities.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CameraId");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("CameraId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("Recorder.Service.Entities.Record", b =>
                {
                    b.HasOne("Recorder.Service.Entities.Camera", "Camera")
                        .WithMany("Records")
                        .HasForeignKey("CameraId");
                });
#pragma warning restore 612, 618
        }
    }
}