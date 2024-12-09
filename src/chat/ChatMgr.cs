using OpenIMSDK = OpenIM.IMSDK.IMSDK;

using OpenIM.Proto;
using Dawn;
using IMDemo.Data;
using Newtonsoft.Json;
using System.Text;

namespace IMDemo.Chat
{
    public class ChatMgr
    {
        public static DemoApplication Application;
        private static ChatMgr _instance;
        public static ChatMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChatMgr();
                }
                return _instance;
            }
        }
        public User currentUser;
        ConnListener connListener;

        private ChatMgr()
        {
            connListener = new ConnListener();
            OpenIMSDK.SetConnListener(connListener);
            OpenIMSDK.SetErrorHandler(ErrorHandler);
        }
        public static Platform Platform
        {
            get
            {
#if WINDOWS
                return Platform.Windows;
#elif LINUX
                return Platform.Linux;
#elif MAC
                return Platform.MacOs;
#endif
            }
        }
        public void ErrorHandler(int errCode, string errMsg)
        {
            Debug.Error(errCode, errMsg);
        }
        public void InitSDK()
        {
            var config = new IMConfig()
            {
                Platform = Platform,
                ApiAddr = Config.APIAddr,
                WsAddr = Config.WsAddr,
                DataDir = Path.Combine(AppContext.BaseDirectory, Config.DataDir),
                LogLevel = LogLevel.LevelDebug,
                IsLogStandardOutput = Config.IsLogStandardOutput,
                LogFilePath = Path.Combine(AppContext.BaseDirectory, Config.LogFilePath),
                DbPath = Config.DBPath
            };
            OpenIMSDK.InitSDK((suc) =>
            {

            }, config);
        }
        public void UnInitSDK()
        {
        }
        public void Update()
        {
            OpenIMSDK.Polling();
        }

        public string GetConnStatus()
        {
            return connListener.connectStatus.ToString();
        }

        async void RefreshToken(string userId)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var url = string.Format("{0}{1}", Config.APIAddr, "/auth/get_user_token");
                    var userTokenReq = new UserTokenReq()
                    {
                        secret = "openIM123",
                        platformID = (int)Platform,
                        userID = userId,
                    };
                    var postData = JsonConvert.SerializeObject(userTokenReq);
                    httpClient.DefaultRequestHeaders.Add("operationID", "111111");
                    httpClient.DefaultRequestHeaders.Add("token", Config.AdminToken);
                    HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(postData, Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<UserTokenRes>(jsonResponse);
                    if (res.errCode > 0)
                    {
                        Debug.Log($"Http Request Error Code :{res.errCode + ":" + res.errMsg}");
                    }
                    else
                    {
                        var token = res.data.token;
                        // TODO
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Http Request Error:{e.Message}");
                }
            }
        }
    }
}