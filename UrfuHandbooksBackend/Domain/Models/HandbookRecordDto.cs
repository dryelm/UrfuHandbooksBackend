using HandbooksBackend.Domain.Models;

public class HandbookRecordDto
{
	public long Id { get; set; }
	public long HandbookId { get; set; }
	public IEnumerable<HandbookRecordContentDto> Values { get; set; }
	public long? ParentId { get; set; }
}