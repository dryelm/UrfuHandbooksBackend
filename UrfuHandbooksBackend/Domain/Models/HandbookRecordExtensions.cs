using HandbooksBackend.Domain.Entities;
using HandbooksBackend.Domain.Models;
using NuGet.Protocol;

public static class HandbookRecordExtensions
{
	public static HandbookRecordDto ToDto(this HandbookRecord handbookRecord)
	{
		return new HandbookRecordDto
		{
			Id = handbookRecord.Id,
			HandbookId = handbookRecord.Handbook.Id,
			Values = handbookRecord.HandbookRecordContents
				.Select(x => new HandbookRecordContentDto
				{
					Id = x.Id, Value = x.Value
				}),
			ParentId = handbookRecord.Parent?.Id
		};
	}
}