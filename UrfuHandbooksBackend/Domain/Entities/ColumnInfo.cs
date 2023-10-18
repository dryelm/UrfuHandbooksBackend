using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HandbooksBackend.Domain.Models;

namespace HandbooksBackend.Domain.Entities;

public class ColumnInfo
{
	[Key] public long Id { get; set; }

	[ForeignKey(nameof(Handbook))] public long HandbookId { get; set; }

	[Required] public Handbook Handbook { get; set; }

	[Required] public int Index { get; set; }
	[Required] public string Header { get; set; }

	[ForeignKey(nameof(ColumnType))] public long ColumnTypeId { get; set; }
	public ColumnType ColumnType { get; set; }
	[Required] public bool IsRequired { get; set; }

	public ColumnDto ToDto()
	{
		return new ColumnDto()
		{
			ColumnType = ColumnType,
			Header = Header,
			IsRequired = IsRequired,
			Index = Index
		};
	}
}