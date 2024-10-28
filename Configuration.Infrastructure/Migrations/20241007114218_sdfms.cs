using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sdfms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreamProfiles_Components_ComponentConfigComponentId_Compon~",
                table: "StreamProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StreamProfiles_ComponentConfigComponentId_ComponentConfigDe~",
                table: "StreamProfiles");

            migrationBuilder.DropColumn(
                name: "ComponentConfigComponentId",
                table: "StreamProfiles");

            migrationBuilder.DropColumn(
                name: "ComponentConfigDeviceId",
                table: "StreamProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComponentConfigComponentId",
                table: "StreamProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentConfigDeviceId",
                table: "StreamProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StreamProfiles_ComponentConfigComponentId_ComponentConfigDe~",
                table: "StreamProfiles",
                columns: new[] { "ComponentConfigComponentId", "ComponentConfigDeviceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StreamProfiles_Components_ComponentConfigComponentId_Compon~",
                table: "StreamProfiles",
                columns: new[] { "ComponentConfigComponentId", "ComponentConfigDeviceId" },
                principalTable: "Components",
                principalColumns: new[] { "ComponentId", "DeviceId" });
        }
    }
}
