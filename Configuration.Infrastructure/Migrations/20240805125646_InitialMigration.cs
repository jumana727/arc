using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    HttpPort = table.Column<int>(type: "integer", nullable: false),
                    RtspPort = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Protocol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => new { x.ComponentId, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_Components_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamProfiles",
                columns: table => new
                {
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VideoCodec = table.Column<byte>(type: "smallint", nullable: false),
                    Resolution = table.Column<int>(type: "integer", nullable: false),
                    AudioCodec = table.Column<int>(type: "integer", nullable: false),
                    BitrateControl = table.Column<byte>(type: "smallint", nullable: false),
                    Bitrate = table.Column<int>(type: "integer", nullable: false),
                    FPS = table.Column<int>(type: "integer", nullable: false),
                    Quality = table.Column<int>(type: "integer", nullable: false),
                    GOP = table.Column<int>(type: "integer", nullable: false),
                    EnableAudio = table.Column<bool>(type: "boolean", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ComponentConfigComponentId = table.Column<int>(type: "integer", nullable: true),
                    ComponentConfigDeviceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamProfiles", x => new { x.ComponentId, x.DeviceId, x.No });
                    table.ForeignKey(
                        name: "FK_StreamProfiles_Components_ComponentConfigComponentId_Compon~",
                        columns: x => new { x.ComponentConfigComponentId, x.ComponentConfigDeviceId },
                        principalTable: "Components",
                        principalColumns: new[] { "ComponentId", "DeviceId" });
                    table.ForeignKey(
                        name: "FK_StreamProfiles_Components_ComponentId_DeviceId",
                        columns: x => new { x.ComponentId, x.DeviceId },
                        principalTable: "Components",
                        principalColumns: new[] { "ComponentId", "DeviceId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_DeviceId",
                table: "Components",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamProfiles_ComponentConfigComponentId_ComponentConfigDe~",
                table: "StreamProfiles",
                columns: new[] { "ComponentConfigComponentId", "ComponentConfigDeviceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreamProfiles");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
