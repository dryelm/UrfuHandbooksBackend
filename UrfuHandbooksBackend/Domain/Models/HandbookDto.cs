﻿using HandbooksBackend.Domain.Entities;
using HandbooksBackend.Domain.Models;

public class HandbookDto
{
	public long Id { get; set; }
	public string Header { get; set; }
	public string Description { get; set; }
	public bool IsInherited { get; set; }
	public ColumnDto[] ColumnInfos { get; set; }
}