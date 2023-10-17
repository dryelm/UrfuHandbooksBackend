using System.ComponentModel.DataAnnotations;

namespace HandbooksBackend.Domain.Entities;

public class HandbookRecord
{
    [Key] public long Id { get; set; }
    [Required] public Handbook Handbook { get; set; }
    [Required] public HandbookRecordContent HandbookRecordContent { get; set; }
    public HandbookRecord Parent { get; set; }
}