namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string claimDatabseType = "databasetype";
        public EDatabaseType DatabaseType;
        public EDatabaseType GetDefault => EDatabaseType.SqlServer;
    }
}
