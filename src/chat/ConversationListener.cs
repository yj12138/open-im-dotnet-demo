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
            throw new NotImplementedException();
        }

        public void OnSyncServerFinish(bool reinstalled)
        {
            throw new NotImplementedException();
        }

        public void OnSyncServerProgress(int progress)
        {
            throw new NotImplementedException();
        }

        public void OnSyncServerFailed(bool reinstalled)
        {
            throw new NotImplementedException();
        }

        public void OnNewConversation(IMConversation[] conversations)
        {
            throw new NotImplementedException();
        }

        public void OnConversationChanged(IMConversation[] conversations)
        {
            throw new NotImplementedException();
        }

        public void OnTotalUnreadMessageCountChanged(int totalUnreadCount)
        {
            throw new NotImplementedException();
        }

        public void OnConversationUserInputStatusChanged(string conversationId, string userId, Platform[] platforms)
        {
            throw new NotImplementedException();
        }
    }
}
