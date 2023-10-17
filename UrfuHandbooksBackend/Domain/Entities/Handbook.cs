using System.ComponentModel.DataAnnotations;

namespace HandbooksBackend.Domain.Entities;

public class Handbook
{
	[Key] public long Id { get; set; }
	[Required] public string Header { get; set; }
	[Required] public string Description { get; set; }
	[Required] public bool IsInherited { get; set; }
	[Required] public ICollection<ColumnInfo> ColumnInfos { get; set; }
}