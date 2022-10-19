namespace Model.Configuration;

public class ModelDbContext : DbContext {
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
}