using HandbooksBackend.Domain;
using HandbooksBackend.Domain.Entities;
using HandbooksBackend.Domain.Models;

public static class HandbookExtensions
{
	public static HandbookDto ToDto(this Handbook handbook)
	{
		return new HandbookDto
		{
			Id = handbook.Id,
			Header = handbook.Header,
			Description = handbook.Description,
			IsInherited = handbook.IsInherited,
			ColumnInfos = handbook.ColumnInfos.Select(x => new ColumnDto()
			{
				ColumnTypeId = x.ColumnType.Id,
				Header = x.Header,
				Index = x.Index,
				IsRequired = x.IsRequired
			}).ToArray()
		};
	}
}