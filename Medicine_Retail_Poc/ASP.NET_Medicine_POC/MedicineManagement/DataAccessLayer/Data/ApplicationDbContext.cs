using DataAccessLayer.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    
    /// <summary>
    /// ApplicationDbContext class is a subclass of DbContext, responsible for interacting with the database.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        

        /// <summary>
        /// Constructor for ApplicationDbContext that takes DbContextOptions as a parameter.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }

        // DbSet<Login> represents a collection of Login entities in the database.
        // This maps to the "Logins" table in the database.

        public DbSet<Login> Logins { get; set; }

        // DbSet<Registration> represents a collection of Registration entities in the database.
        // This maps to the "Registrations" table in the database.
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Payments> Payments { get; set; }

        public DbSet<AddToCart> AddToCart { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Apply the RegistrationConfiguration to the Registration entity
        //    modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
        //}
    }
}
