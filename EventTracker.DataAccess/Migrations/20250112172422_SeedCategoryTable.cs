using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoryTable : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "Categories",
				columns: new[] { "Id", "Title" },
				values: new object[,]
				{
					{ 1, "Sport" },
					{ 2, "Music" },
					{ 3, "Art" },
					{ 4, "Technology" },
					{ 5, "Education" },
					{ 6, "Health" },
					{ 7, "Food" },
					{ 8, "Travel" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
		}

	}
}
