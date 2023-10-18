using HandbooksBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HandbooksBackend.Domain;

public class Context : DbContext
{
	public Context()
	{
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(
				@"Server=localhost;Port=5432;Database=URFUHandbooks;Username=postgres;Password=postgres")
			;
	}

	public DbSet<Handbook> Handbooks { get; set; }
	public DbSet<HandbookRecord> HandbookRecords { get; set; }
	public DbSet<HandbookRecordContent> HandbookRecordContents { get; set; }
	public DbSet<ColumnInfo> ColumnInfos { get; set; }
	public DbSet<ColumnType> ColumnTypes { get; set; }
}