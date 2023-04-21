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
            string ConnectStr = SetCoonectStr(ConfigureModeCode.Offline);
            optionsBuilder.UseSqlServer(connectionString: ConnectStr);
        }

        private string SetCoonectStr(ConfigureModeCode mode)
        {
            string connectStr = null;
            if (mode == ConfigureModeCode.Online)
            {
                connectStr = "Data Source = IZE4NVUT7CZL1RZ; Initial Catalog = FantasyOfSango_Player;" + "User ID = ROOT; Password = root;";
            }
            else if (mode == ConfigureModeCode.Offline)
            {
                connectStr = "Data Source = SANGONOMIYAPC; Initial Catalog = FantasyOfSango_Player;" + "User ID = ROOT; Password = root;";
            }
            return connectStr;
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

        private enum ConfigureModeCode
        {
            Offline,
            Online
        }
    }
}
