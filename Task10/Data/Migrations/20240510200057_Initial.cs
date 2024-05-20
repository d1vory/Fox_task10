using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task10.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Learn the basics of Python programming language, including variables, data types, control structures, functions, and basic algorithms.", "Python Programming Fundamentals" },
                    { 2, "Discover effective digital marketing strategies, including search engine optimization (SEO), social media marketing, email marketing, and content marketing.", "Digital Marketing Strategies" },
                    { 3, "Get started with web development by learning HTML, CSS, and JavaScript fundamentals.", "Web Development Basics" },
                    { 4, "Learn the fundamentals of graphic design, including typography, color theory, layout design, and image manipulation techniques.", "Graphic Design Essentials" },
                    { 5, "Get started with mobile app development by learning about mobile platforms, user interface design, and mobile app development tools.", "Mobile App Development Basics" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CourseId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "PPF-01" },
                    { 2, 1, "PPF-02" },
                    { 3, 1, "PPF-03" },
                    { 4, 2, "DMC-01" },
                    { 5, 2, "DMC-02" },
                    { 6, 3, "WDB-01" },
                    { 7, 3, "WDB-02" },
                    { 8, 4, "GDE-01" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "GroupId", "LastName" },
                values: new object[,]
                {
                    { 1, "John", 1, "Doe" },
                    { 2, "Alice", 1, "Smith" },
                    { 3, "Michael", 1, "Johnson" },
                    { 4, "Emily", 2, "Brown" },
                    { 5, "Daniel", 3, "Wilson" },
                    { 6, "Jessica", 3, "Martinez" },
                    { 7, "Matthew", 4, "Taylor" },
                    { 8, "Sophia", 5, "Anderson" },
                    { 9, "William", 6, "Thomas" },
                    { 10, "Olivia", 7, "Hernandez" },
                    { 11, "Ethan", 8, "Moore" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
