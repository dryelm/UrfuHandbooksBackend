using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandbooksBackend.Domain.Entities;

public class ColumnInfo
{
	[Key] public long Id { get; set; }

	[ForeignKey(nameof(Handbook))] public long HandbookId { get; set; }

	[Required] public int Index { get; set; }
	[Required] public string Header { get; set; }
	[Required] public ColumnType ColumnType { get; set; }
	[Required] public bool IsRequired { get; set; }
}