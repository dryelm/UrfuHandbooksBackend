using System.ComponentModel.DataAnnotations;

namespace HandbooksBackend.Domain.Entities;

public class ColumnType
{
    [Key] public long Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public bool IsMultiple { get; set; }

    public string[]? PossibleValues { get; set; }
}