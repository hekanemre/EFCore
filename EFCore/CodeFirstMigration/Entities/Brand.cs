namespace CodeFirstMigration.Entities;
//[Table("Brands", Schema = "brandsschema")]
public class Brand : BaseEntity
{
    // We are using dataannotations for specifying name,order or datatype for related column
    // Best practice is making them in dbcontext with OnModelCrating method so we are commenting those annotation lines
    // [Column(TypeName = "varchar(150)")] -- [MaxLength(150)]
    [StringLength(150, MinimumLength =1)] //there is nothing like min value in db  
    // string? --> ? make those column nullable
    public string Name { get; set; }
}
