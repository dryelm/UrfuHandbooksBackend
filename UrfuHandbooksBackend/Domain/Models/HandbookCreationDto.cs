namespace HandbooksBackend.Domain.Models;

public class HandbookCreationDto
{
	public string Header { get; set; }
	public string Description { get; set; }
	public bool IsInherited { get; set; }
	public ColumnDto[] ColumnInfos { get; set; }
}