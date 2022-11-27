namespace YasShop.Infrastructure.EfCore.Config
{
    public static class ConnectionString
    {
        public static string Get()
        {
            return "Server=.;Database=SinaYasShopDB;Trusted_Connection=True;";
            //return "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";
        }
    }
}
