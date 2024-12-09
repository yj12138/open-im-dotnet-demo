using Dawn;
using OpenIM.IMSDK;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;
using OpenIM.Proto;

namespace IMDemo.Chat
{
    public class User
    {
        public string uid;
        public string token;
        public LoginStatus loginStatus = LoginStatus.Default;
        public ConversationListener conversationListener;
        public FriendShipListener friendShipListener;
        public GroupListener groupListener;
        public MessageListener messageListener;
        public int totalUnreadCount;

        public User(string uid, string token)
        {
            this.uid = uid;
            this.token = token;
            conversationListener = new ConversationListener();
            friendShipListener = new FriendShipListener();
            groupListener = new GroupListener();
            messageListener = new MessageListener();

            OpenIMSDK.SetConversationListener(conversationListener);
            OpenIMSDK.SetFriendShipListener(friendShipListener);
            OpenIMSDK.SetGroupListener(groupListener);
            OpenIMSDK.SetMessageListener(messageListener);
        }

        public void Login()
        {
            OpenIMSDK.Login((bool suc) =>
            {
                if (suc)
                {
                    OnLoginSuc();
                }
            }, uid, token);
        }
        public void Logout()
        {
            OpenIMSDK.Logout((bool suc) =>
            {
                if (suc)
                {
                    Debug.Log($"{uid} Logout suc");
                    ChatMgr.Instance.currentUser = null;
                    ChatMgr.Application.Title = "IMDemo";
                }
            });
        }
        void OnLoginSuc()
        {
            ChatMgr.Application.Title = "IMDemo-" + uid;
            OpenIMSDK.GetLoginStatus((status) =>
            {
                loginStatus = status;
            });
            OpenIMSDK.GetTotalUnreadMsgCount((count) =>
            {
                totalUnreadCount = count;
            });
        }

        public static void TryLogin(string uid, string token)
        {
            if (ChatMgr.Instance.currentUser == null)
            {
                var user = new User(uid, token);
                user.Login();
                ChatMgr.Instance.currentUser = user;
            }
            else
            {
                if (ChatMgr.Instance.currentUser.uid != uid)
                {
                    Application.CreateNewInstance(uid, token);
                }
                else
                {
                    Debug.Log("Already Login");
                }
            }
        }
    }
}

