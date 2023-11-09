namespace BlazorFullStackCrud.Server.Data
{
 // WaterTracker.Data.DbContext
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }


    // Declares a DbSet for User entities. 
    public DbSet<User> Users { get; set; }

    // Declares a DbSet for WaterIntake entities
    public DbSet<WaterIntake> WaterIntakes { get; set; }


    // Overrides the OnModelCreating method to configure the entity relationships and database schema
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            // Configures the relationship between WaterIntake and User entities
            modelBuilder.Entity<WaterIntake>()
            .HasOne(wi => wi.User) // Specifies that each WaterIntake has one User
            .WithMany(u => u.WaterIntakes) // Specifies that a User can have many WaterIntakes
            .HasForeignKey(wi => wi.UserID); // Specifies the foreign key property in the WaterIntake entity that points to the related User entity
    }
}

}
