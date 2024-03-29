﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Dominos.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dominos.Persistence.Postgres.Migrations
{
    [DbContext(typeof(VouchersDbContext))]
    [Migration("20230410170224_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dominos.Domain.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price")
                        .HasAnnotation("Relational:JsonPropertyName", "price");

                    b.Property<List<string>>("ProductCodes")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("product_codes")
                        .HasAnnotation("Relational:JsonPropertyName", "product_codes");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_vouchers_name");

                    b.ToTable("vouchers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
