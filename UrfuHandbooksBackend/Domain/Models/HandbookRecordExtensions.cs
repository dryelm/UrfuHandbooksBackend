using HandbooksBackend.Domain.Entities;

public static class HandbookRecordExtensions
{
    public static HandbookRecordDto ToDto(this HandbookRecord handbookRecord)
    {
        return new HandbookRecordDto
        {
            Id = handbookRecord.Id,
            HandbookId = handbookRecord.Handbook.Id,
            HandbookRecordContentId = handbookRecord.HandbookRecordContent.Id,
            ParentId = handbookRecord.Parent?.Id
        };
    }
}