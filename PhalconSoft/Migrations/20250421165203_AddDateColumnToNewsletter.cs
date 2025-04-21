using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhalconSoft.Migrations
{
    /// <inheritdoc />
    public partial class AddDateColumnToNewsletter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendEmails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsletterSubscribers",
                table: "NewsletterSubscribers");

            migrationBuilder.RenameTable(
                name: "NewsletterSubscribers",
                newName: "NewsletterSubscriber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsletterSubscriber",
                table: "NewsletterSubscriber",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsletterSubscriber",
                table: "NewsletterSubscriber");

            migrationBuilder.RenameTable(
                name: "NewsletterSubscriber",
                newName: "NewsletterSubscribers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsletterSubscribers",
                table: "NewsletterSubscribers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SendEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmails", x => x.Id);
                });
        }
    }
}
