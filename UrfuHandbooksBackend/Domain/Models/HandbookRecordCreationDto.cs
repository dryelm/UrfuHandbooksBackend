namespace HandbooksBackend.Domain.Models;

public class HandbookRecordCreationDto
{
	public long Id { get; set; }
	public long HandbookId { get; set; }
	public IEnumerable<HandbookRecordContentCreationDto> Values { get; set; }
	public long? ParentId { get; set; }
}