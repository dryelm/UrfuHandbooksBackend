using System.ComponentModel.DataAnnotations;

namespace HandbooksBackend.Domain.Entities;

public class HandbookRecordContent
{
    public long Id { get; set; }
    [Required] public ColumnInfo ColumnInfo { get; set; }
    [Required] public string Value { get; set; }
}