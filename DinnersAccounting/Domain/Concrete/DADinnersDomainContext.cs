using System.Data.Entity;

namespace DA.Dinners.Domain.Concrete
{
    public class DADinnersDomainContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        //
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<DA.Dinners.Domain.Models.DADinnersDomainContext>());

        public DbSet<DA.Dinners.Domain.CreditOperation> CreditOperations { get; set; }

        public DbSet<DA.Dinners.Model.Product> Products { get; set; }

        public DbSet<DA.Dinners.Domain.DebitOperation> DebitOperations { get; set; }

        public DbSet<DA.Dinners.Model.Proposition> Propositions { get; set; }

        public DbSet<DA.Dinners.Model.DayProposition> DayPropositions { get; set; }

        public DbSet<DA.Dinners.Model.ContinuousProposition> ContinuousPropositions { get; set; }

        public DbSet<DA.Dinners.Domain.Order> Orders { get; set; }

        public DbSet<DA.Dinners.Domain.OrderDetail> OrderDetails { get; set; }

        public DbSet<DA.Dinners.Domain.OrderStatus> OrderStatus { get; set; }

        public DbSet<DA.Dinners.Domain.Person> People { get; set; }

        //public DbSet<DA.Dinners.Domain.PersonAccount> PersonAccounts { get; set; }

        public DbSet<DA.Dinners.Domain.Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}