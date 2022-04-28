using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Infrastructure.EfCore.Config
{
   public static class ConnectionString
    {
        public static string Get()
        {
            return "Server=.;Database=YasShopDB;Trusted_Connection=True;";
            //return "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";
        }
    }
}
