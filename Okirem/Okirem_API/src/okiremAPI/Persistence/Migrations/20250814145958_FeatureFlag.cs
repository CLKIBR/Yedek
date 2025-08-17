using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FeatureFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("5909e261-635f-4730-9e81-9c145314b70e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35d13bbd-3098-485c-a272-bbe0416e0c18"));

            migrationBuilder.CreateTable(
                name: "FeatureFlags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Environments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowedClaims = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFlags", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 78, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FeatureFlags.Admin", null },
                    { 79, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FeatureFlags.Read", null },
                    { 80, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FeatureFlags.Write", null },
                    { 81, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FeatureFlags.Create", null },
                    { 82, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FeatureFlags.Update", null },
                    { 83, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FeatureFlags.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("466344bb-df21-40b6-9277-c8ecaa319e2b"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "clkibrahim@outlook.com", new byte[] { 4, 174, 194, 102, 167, 172, 25, 92, 20, 222, 82, 181, 1, 161, 32, 5, 62, 21, 196, 241, 111, 241, 153, 165, 106, 183, 96, 96, 92, 90, 167, 158, 214, 109, 167, 158, 240, 42, 110, 89, 194, 25, 120, 215, 202, 254, 189, 195, 174, 156, 218, 65, 18, 79, 131, 218, 2, 253, 229, 20, 8, 137, 71, 173 }, new byte[] { 100, 115, 104, 225, 239, 17, 192, 129, 241, 109, 29, 201, 206, 124, 171, 5, 206, 215, 29, 156, 24, 51, 254, 169, 79, 213, 44, 205, 250, 24, 40, 228, 8, 151, 8, 47, 167, 75, 19, 100, 230, 167, 174, 107, 253, 10, 214, 255, 238, 5, 96, 209, 48, 142, 53, 184, 98, 135, 10, 230, 250, 99, 209, 170, 47, 130, 93, 216, 68, 222, 138, 146, 143, 4, 11, 154, 181, 137, 71, 225, 6, 216, 173, 204, 165, 0, 22, 115, 127, 249, 91, 71, 211, 98, 148, 2, 222, 122, 127, 40, 219, 138, 28, 206, 152, 237, 89, 210, 16, 134, 51, 90, 122, 10, 219, 130, 215, 174, 100, 168, 226, 143, 86, 9, 112, 188, 120, 69 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("373b892d-8ce9-4226-8234-82947eb47e22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("466344bb-df21-40b6-9277-c8ecaa319e2b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureFlags");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("373b892d-8ce9-4226-8234-82947eb47e22"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("466344bb-df21-40b6-9277-c8ecaa319e2b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("35d13bbd-3098-485c-a272-bbe0416e0c18"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "clkibrahim@outlook.com", new byte[] { 238, 159, 139, 204, 20, 134, 210, 103, 123, 77, 81, 237, 232, 164, 106, 248, 88, 123, 168, 85, 233, 17, 233, 95, 241, 10, 237, 5, 236, 208, 84, 90, 130, 208, 27, 21, 67, 248, 11, 88, 146, 124, 219, 160, 151, 234, 216, 128, 223, 135, 243, 41, 109, 105, 1, 193, 124, 139, 197, 7, 167, 255, 77, 134 }, new byte[] { 202, 122, 220, 228, 221, 221, 207, 5, 121, 114, 28, 30, 75, 179, 43, 247, 144, 210, 187, 118, 93, 146, 16, 249, 27, 24, 63, 203, 67, 100, 199, 40, 46, 6, 103, 166, 9, 143, 114, 46, 96, 116, 4, 107, 179, 82, 90, 96, 90, 186, 116, 84, 112, 231, 130, 91, 113, 216, 178, 238, 240, 207, 144, 65, 174, 214, 96, 136, 138, 94, 21, 116, 94, 246, 112, 91, 144, 206, 0, 226, 5, 238, 210, 45, 155, 80, 204, 130, 31, 215, 78, 79, 34, 113, 251, 191, 148, 94, 247, 103, 8, 61, 37, 173, 204, 11, 45, 245, 227, 110, 61, 74, 185, 8, 195, 188, 180, 218, 134, 132, 77, 0, 14, 214, 157, 41, 201, 108 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("5909e261-635f-4730-9e81-9c145314b70e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("35d13bbd-3098-485c-a272-bbe0416e0c18") });
        }
    }
}
