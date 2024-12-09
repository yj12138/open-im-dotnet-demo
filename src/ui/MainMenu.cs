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
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJ0ZXN0MSIsIlBsYXRmb3JtSUQiOjMsImV4cCI6MTc0MTUyOTcxNiwibmJmIjoxNzMzNzUzNDE2LCJpYXQiOjE3MzM3NTM3MTZ9.Z3Fdx4bO-ymhtq4XSQh8rsUTtothCJhAfu8ry8VaL7Q";
            User.TryLogin(uid, token);
        }

        [MenuItem("Start/Login/test2")]
        public static void LoginUser2()
        {
            var uid = "test2";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJ0ZXN0MiIsIlBsYXRmb3JtSUQiOjMsImV4cCI6MTc0MTUyOTczMiwibmJmIjoxNzMzNzUzNDMyLCJpYXQiOjE3MzM3NTM3MzJ9.uoas8c1ooaE-oavAlehLTrwunPyEOyWbuUO3dUtKB-c";
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