using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhalconSoft.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "newsletter_subscriber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "blob", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "portfolio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Category = table.Column<int>(type: "int(11)", nullable: false),
                    SeoUrl = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    ProjectUrl = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    PortfolioTitle = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    PortfolioDetail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "portfolio_ibfk_1",
                        column: x => x.Category,
                        principalTable: "category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "portfolio_photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverPhoto = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    ThumbnailPhoto = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Portfolio = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "portfolio_photo_ibfk_1",
                        column: x => x.Portfolio,
                        principalTable: "portfolio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "Name",
                table: "category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Email",
                table: "newsletter_subscriber",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Category",
                table: "portfolio",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "Portfolio",
                table: "portfolio_photo",
                column: "Portfolio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "newsletter_subscriber");

            migrationBuilder.DropTable(
                name: "portfolio_photo");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "portfolio");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
