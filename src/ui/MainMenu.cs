using Dawn;
using IMDemo.Chat;

namespace IMDemo.UI
{
    public static class MainMenu
    {
        [MenuItem("Start/Login/test1")]
        public static void LoginUser1()
        {
            var uid = "test1";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJ0ZXN0MSIsIlBsYXRmb3JtSUQiOjMsImV4cCI6MTc0MTU3Mzg4MCwibmJmIjoxNzMzNzk3NTgwLCJpYXQiOjE3MzM3OTc4ODB9.bEUQjXRJnb2zTAUZcjTIw4iBA9kX9TNnl5ZUQRyKIEY";
            User.TryLogin(uid, token);
        }

        [MenuItem("Start/Login/test2")]
        public static void LoginUser2()
        {
            var uid = "test2";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJ0ZXN0MiIsIlBsYXRmb3JtSUQiOjMsImV4cCI6MTc0MTU3Mzg5NCwibmJmIjoxNzMzNzk3NTk0LCJpYXQiOjE3MzM3OTc4OTR9.3LFoOqwV9Hd2yq5nXFWpADZqnS886SsvdIXdBNSzuAk";
            User.TryLogin(uid, token);
        }

        [MenuItem("Help/Official WebSite")]
        public static void OfficialWebSite()
        {
            Application.OpenUrl(Config.OfficialWebSite);
        }

        [MenuItem("Help/Doc WebSite")]
        public static void DocWebSite()
        {
            Application.OpenUrl(Config.DocWebSite);
        }
    }
}