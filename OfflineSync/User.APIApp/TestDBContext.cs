using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace User.APIApp
{
    public class TestDBContext : DbContext
    {
        public TestDBContext() : base("SyncConn")
        {
        }

        public virtual DbSet<tblTestACTS> tblTestACTSs { get; set; }
        public virtual DbSet<tblTestACTSH> tblTestACTSHs { get; set; }
        public virtual DbSet<tblTestASTC> tblTestASTCs { get; set; }
        public virtual DbSet<tblTestATWS> tblTestATWSs { get; set; }
        public virtual DbSet<tblTestCTS> tblTestCTSs { get; set; }
        public virtual DbSet<tblTestCTSH> tblTestCTSHs { get; set; }
        public virtual DbSet<tblTestSTC> tblTestSTCs { get; set; }
        public virtual DbSet<tblTestTWS> tblTestTWSs { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            // Table names doesn't changes
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //dbModelBuilder.Entity<tblTestACTS>()
            //.Property(e => e.SyncCreatedAt)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            //dbModelBuilder.Entity<tblTestACTS>()
            //.Property(e => e.SyncModifiedAt)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}