using OpenIM.IMSDK;
using OpenIM.IMSDK.Listener;
public class Conversation : IConversationListener
{
    User user;
    List<OpenIM.IMSDK.Conversation> conversationList;
    public List<OpenIM.IMSDK.Conversation> ConversationList
    {
        get
        {
            return conversationList;
        }
    }

    public Conversation(User user)
    {
        this.user = user;
        conversationList = new List<OpenIM.IMSDK.Conversation>();
    }

    public void OnConversationChanged(List<Conversation> conversationList)
    {
    }

    public void OnNewConversation(List<Conversation> conversationList)
    {
    }

    public void OnSyncServerFailed()
    {
    }

    public void OnSyncServerFinish()
    {
        IMSDK.GetAllConversationList((list, errCode, errMsg) =>
        {
            if (list != null)
            {
                conversationList.AddRange(list);
            }
            else
            {
                Debug.Log(errCode, errMsg);
            }
        });
    }

    public void OnSyncServerStart()
    {
    }

    public void OnTotalUnreadMessageCountChanged(int totalUnreadCount)
    {
    }

    public void OnConversationUserInputStatusChanged(InputStatesChangedData data)
    {
    }

    public void OnSyncServerProgress(int progress)
    {
    }

    public void OnNewConversation(List<OpenIM.IMSDK.Conversation> conversationList)
    {
    }

    public void OnConversationChanged(List<OpenIM.IMSDK.Conversation> conversationList)
    {
    }
}