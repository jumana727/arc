﻿// <auto-generated />
using System;
using Configuration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Configuration.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Configuration.Domain.Entities.ComponentConfig", b =>
                {
                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("ComponentId", "DeviceId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("Configuration.Domain.Entities.DeviceConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("HttpPort")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Protocol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("RtspPort")
                        .HasColumnType("integer");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Configuration.Domain.Entities.StreamProfileConfig", b =>
                {
                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<int>("No")
                        .HasColumnType("integer");

                    b.Property<int>("AudioCodec")
                        .HasColumnType("integer");

                    b.Property<int>("Bitrate")
                        .HasColumnType("integer");

                    b.Property<byte>("BitrateControl")
                        .HasColumnType("smallint");

                    b.Property<int?>("ComponentConfigComponentId")
                        .HasColumnType("integer");

                    b.Property<int?>("ComponentConfigDeviceId")
                        .HasColumnType("integer");

                    b.Property<bool>("EnableAudio")
                        .HasColumnType("boolean");

                    b.Property<int>("FPS")
                        .HasColumnType("integer");

                    b.Property<int>("GOP")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Quality")
                        .HasColumnType("integer");

                    b.Property<int>("Resolution")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("VideoCodec")
                        .HasColumnType("smallint");

                    b.HasKey("ComponentId", "DeviceId", "No");

                    b.HasIndex("ComponentConfigComponentId", "ComponentConfigDeviceId");

                    b.ToTable("StreamProfiles");
                });

            modelBuilder.Entity("Configuration.Domain.Entities.ComponentConfig", b =>
                {
                    b.HasOne("Configuration.Domain.Entities.DeviceConfig", "Device")
                        .WithMany("Components")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Configuration.Domain.Entities.StreamProfileConfig", b =>
                {
                    b.HasOne("Configuration.Domain.Entities.ComponentConfig", null)
                        .WithMany("StreamProfiles")
                        .HasForeignKey("ComponentConfigComponentId", "ComponentConfigDeviceId");

                    b.HasOne("Configuration.Domain.Entities.ComponentConfig", "Component")
                        .WithMany("Profiles")
                        .HasForeignKey("ComponentId", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("Configuration.Domain.Entities.ComponentConfig", b =>
                {
                    b.Navigation("Profiles");

                    b.Navigation("StreamProfiles");
                });

            modelBuilder.Entity("Configuration.Domain.Entities.DeviceConfig", b =>
                {
                    b.Navigation("Components");
                });
#pragma warning restore 612, 618
        }
    }
}
