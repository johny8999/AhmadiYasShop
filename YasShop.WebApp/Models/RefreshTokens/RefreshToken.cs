using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YasShop.WebApp.Models.RefreshTokens
{
    public static class RefreshToken
    {
        private static List<string> LstIds { get; set; }

        public static void AddToList(string userId)
        {
            if (!IsInList(userId))
                LstIds.Add(userId);

        }

        public static bool IsInList(string userId)
        {
            return LstIds.Any(a => a.Equals(userId));
        }

        public static void RemoveUserId(string userId)
        {
            if (IsInList(userId))
                LstIds.Remove(userId);
        }
    }
}
