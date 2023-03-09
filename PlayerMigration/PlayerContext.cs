using Microsoft.EntityFrameworkCore;
using PlayerData;

//Developer : SangonomiyaSakunovi
//Discription : The CoreScript of EntityFramework, all DbSet and Relations should define here

namespace PlayerMigration
{
    public class PlayerContext : DbContext
    {
        //SQL configure
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectStr = "Data Source = SANGONOMIYAPC; Initial Catalog = FantasyOfSango_Player;" +
                "User ID = ROOT; Password = root;";
            optionsBuilder.UseSqlServer(connectionString: ConnectStr);
        }
        //The RefenrenceKey structure should define here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
                .HasOne(x => x.Player)
                .WithOne(y => y.UserInfo)
                .HasForeignKey<UserInfo>(z => z.PlayerId);
        }
        //The DbSet should define here
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<AttributeInfo> AttributeInfos { get; set; }
        public DbSet<WeaponInfo> WeaponInfos { get; set; }
        public DbSet<ArtifactInfo> ArtifactInfos { get; set; }
    }
}
