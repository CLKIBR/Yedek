using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordColumnsToEmailAuthenticator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("f0a2a6cc-3c24-4dc8-81a6-19ad393e2c6b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98c6c661-3131-4997-9d97-c8b15ba27005"));

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Schools",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Schools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Schools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Schools",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResetPasswordToken",
                table: "EmailAuthenticators",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiry",
                table: "EmailAuthenticators",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("35d13bbd-3098-485c-a272-bbe0416e0c18"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "clkibrahim@outlook.com", new byte[] { 238, 159, 139, 204, 20, 134, 210, 103, 123, 77, 81, 237, 232, 164, 106, 248, 88, 123, 168, 85, 233, 17, 233, 95, 241, 10, 237, 5, 236, 208, 84, 90, 130, 208, 27, 21, 67, 248, 11, 88, 146, 124, 219, 160, 151, 234, 216, 128, 223, 135, 243, 41, 109, 105, 1, 193, 124, 139, 197, 7, 167, 255, 77, 134 }, new byte[] { 202, 122, 220, 228, 221, 221, 207, 5, 121, 114, 28, 30, 75, 179, 43, 247, 144, 210, 187, 118, 93, 146, 16, 249, 27, 24, 63, 203, 67, 100, 199, 40, 46, 6, 103, 166, 9, 143, 114, 46, 96, 116, 4, 107, 179, 82, 90, 96, 90, 186, 116, 84, 112, 231, 130, 91, 113, 216, 178, 238, 240, 207, 144, 65, 174, 214, 96, 136, 138, 94, 21, 116, 94, 246, 112, 91, 144, 206, 0, 226, 5, 238, 210, 45, 155, 80, 204, 130, 31, 215, 78, 79, 34, 113, 251, 191, 148, 94, 247, 103, 8, 61, 37, 173, 204, 11, 45, 245, 227, 110, 61, 74, 185, 8, 195, 188, 180, 218, 134, 132, 77, 0, 14, 214, 157, 41, 201, 108 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("5909e261-635f-4730-9e81-9c145314b70e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("35d13bbd-3098-485c-a272-bbe0416e0c18") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("5909e261-635f-4730-9e81-9c145314b70e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("35d13bbd-3098-485c-a272-bbe0416e0c18"));

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "EmailAuthenticators");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiry",
                table: "EmailAuthenticators");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("98c6c661-3131-4997-9d97-c8b15ba27005"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "clkibrahim@outlook.com", new byte[] { 88, 205, 2, 106, 123, 207, 197, 157, 194, 248, 203, 32, 106, 171, 93, 25, 121, 86, 34, 78, 180, 163, 158, 42, 102, 99, 159, 161, 161, 79, 7, 184, 156, 85, 229, 15, 231, 69, 216, 239, 252, 226, 160, 163, 140, 204, 147, 97, 198, 31, 137, 109, 147, 137, 182, 36, 59, 12, 38, 103, 50, 76, 50, 12 }, new byte[] { 191, 93, 21, 10, 122, 62, 170, 236, 58, 215, 13, 230, 216, 189, 159, 231, 52, 253, 36, 57, 18, 167, 220, 12, 112, 22, 249, 227, 199, 154, 42, 150, 116, 152, 196, 217, 167, 116, 3, 41, 78, 12, 162, 104, 214, 231, 178, 34, 144, 225, 163, 1, 130, 82, 191, 72, 149, 10, 212, 84, 53, 98, 144, 107, 197, 54, 37, 31, 181, 94, 162, 254, 98, 29, 58, 93, 233, 88, 149, 33, 235, 193, 208, 46, 240, 52, 81, 191, 22, 146, 232, 109, 35, 180, 188, 128, 36, 16, 25, 207, 224, 194, 14, 116, 80, 178, 166, 226, 253, 72, 114, 106, 238, 77, 226, 193, 119, 200, 45, 178, 44, 200, 78, 86, 86, 63, 45, 183 }, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("f0a2a6cc-3c24-4dc8-81a6-19ad393e2c6b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("98c6c661-3131-4997-9d97-c8b15ba27005") });
        }
    }
}
