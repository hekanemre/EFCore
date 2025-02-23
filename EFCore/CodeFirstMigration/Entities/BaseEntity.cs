﻿namespace CodeFirstMigration.Entities;

public class BaseEntity : CoreEntity
{
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
}
