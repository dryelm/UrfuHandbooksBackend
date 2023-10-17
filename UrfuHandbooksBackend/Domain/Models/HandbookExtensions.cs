using HandbooksBackend.Domain.Entities;

public static class HandbookExtensions
{
    public static HandbookDto ToDto(this Handbook handbook)
    {
        return new HandbookDto
        {
            Id = handbook.Id,
            Header = handbook.Header,
            Description = handbook.Description,
            IsInherited = handbook.IsInherited
        };
    }
}