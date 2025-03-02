namespace CodeFirstMigration.Entities;

public class CoreEntity
{
    //If we want to make Id column primary key we can use Key data annotation
    //[Key]
    public int Id { get; set; }
    public bool IsActive { get; set; }
}
