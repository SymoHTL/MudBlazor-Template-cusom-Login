namespace Model.Entities.Roles; 

[Table("ROLES")]
public class Role {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Required]
    [StringLength(16)]
    [Column("NAME", TypeName = "VARCHAR(16)")]
    public string Name { get; set; }
    
    [Required]
    [StringLength(16)]
    [Column("DESCRIPTION", TypeName = "VARCHAR(255)")]
    public string Description { get; set; }


    public Role(string name, string description ) {
        Name = name;
        Description = description;
    }
    public Role(int id, string name, string description ) {
        Id = id;
        Name = name;
        Description = description;
    }
}