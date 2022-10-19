namespace Model.Entities.Users; 

[Table("USER")]
public class User {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [Column("FIRST_NAME", TypeName = "VARCHAR(50)")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    [Column("LAST_NAME", TypeName = "VARCHAR(50)")]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(100)]
    [EmailAddress]
    [Column("EMAIL", TypeName = "VARCHAR(100)")]
    public string Email { get; set; } // email ist unique, DBContext muss entsprechend konfiguriert werden
    
    [Column("PASSWORD", TypeName = "TEXT(65000)")]
    public string Password { get; set; }

    public List<string> Roles => RoleClaims.Select(r => r.Role.Name).ToList();
    public List<RoleClaim> RoleClaims { get; set; } = new();
}