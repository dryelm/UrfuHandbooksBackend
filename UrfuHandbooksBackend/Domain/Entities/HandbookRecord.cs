using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandbooksBackend.Domain.Entities;

public class HandbookRecord
{
	[Key] public long Id { get; set; }

	[ForeignKey(nameof(Entities.Handbook))]
	[Required]
	public long HandbookId { get; set; }

	public Handbook Handbook { get; set; }
	public ICollection<HandbookRecordContent> HandbookRecordContents { get; set; }
	public HandbookRecord? Parent { get; set; }
}