﻿namespace HandbooksBackend.Domain.Models;

public class ColumnDto
{
	public int Index { get; set; }
	public string Header { get; set; }
	public long ColumnTypeId { get; set; }
	public bool IsRequired { get; set; }
}