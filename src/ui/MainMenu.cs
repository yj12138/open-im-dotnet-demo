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
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJ0ZXN0MSIsIlBsYXRmb3JtSUQiOjMsImV4cCI6MTc0MjIyMjQ5NywibmJmIjoxNzM0NDQ2MTk3LCJpYXQiOjE3MzQ0NDY0OTd9.Rs5hEzaZ-Fe1DQFI2Ptph7-8VA0SxszK7WrkaBHPWTA";
            User.TryLogin(uid, token);
        }

        [MenuItem("Start/Login/test2")]
        public static void LoginUser2()
        {
            var uid = "test2";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJ0ZXN0MiIsIlBsYXRmb3JtSUQiOjMsImV4cCI6MTc0MjIyMjUxMiwibmJmIjoxNzM0NDQ2MjEyLCJpYXQiOjE3MzQ0NDY1MTJ9.VX6-MqRyj67g4JysROfG0ctP99uqrrY0hUQvZpF77aw";
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