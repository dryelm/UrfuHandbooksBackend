using HandbooksBackend.Domain;
using HandbooksBackend.Domain.Entities;
using HandbooksBackend.Domain.Models;

public static class HandbookExtensions
{
	public static HandbookDto ToDto(this Handbook handbook, ColumnInfo[] columnInfos)
	{
		return new HandbookDto
		{
			Id = handbook.Id,
			Header = handbook.Header,
			Description = handbook.Description,
			IsInherited = handbook.IsInherited,
			ColumnInfos = columnInfos.Select(c => c.ToDto()).ToArray()
		};
	}
}