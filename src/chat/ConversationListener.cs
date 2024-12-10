using Dawn;
using OpenIM.IMSDK;
using OpenIM.IMSDK.Listener;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;
using OpenIM.Proto;

namespace IMDemo.Chat
{
    public class ConversationListener : IConversationListener
    {

        public event Action<List<IMConversation>> Event_OnConversationsInfoChange;

        public ConversationListener()
        {
        }

        public void OnConversationChanged(List<IMConversation> conversationList)
        {
            Event_OnConversationsInfoChange?.Invoke(conversationList);
        }

        public void OnSyncServerStart(bool reinstalled)
        {
        }

        public void OnSyncServerFinish(bool reinstalled)
        {
        }

        public void OnSyncServerProgress(int progress)
        {
        }

        public void OnSyncServerFailed(bool reinstalled)
        {
        }

        public void OnNewConversation(IMConversation[] conversations)
        {
        }

        public void OnConversationChanged(IMConversation[] conversations)
        {
        }

        public void OnTotalUnreadMessageCountChanged(int totalUnreadCount)
        {
        }

        public void OnConversationUserInputStatusChanged(string conversationId, string userId, Platform[] platforms)
        {
        }
    }
}
