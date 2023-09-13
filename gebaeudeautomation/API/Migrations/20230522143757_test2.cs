using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SensorData",
                newName: "SensorsData");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "SensorsData",
                newName: "Time");

            migrationBuilder.AddColumn<string>(
                name: "SensorId",
                table: "SensorsData",
                type: "text",
                nullable: true);

            migrationBuilder.Sql(
"SELECT create_hypertable( '\"SensorsData\"', 'Time');\n" +
"CREATE INDEX ix_sensorid_time ON \"SensorsData\" (\"SensorId\", \"Time\" DESC)"
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "SensorsData");

            migrationBuilder.RenameTable(
                name: "SensorsData",
                newName: "SensorData");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "SensorData",
                newName: "Timestamp");
        }
    }
}
