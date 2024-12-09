using Dawn;
using OpenIM.IMSDK;
using OpenIM.IMSDK.Listener;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.Chat
{
    public class FriendShipListener : IFriendShipListener
    {

        public event Action<IMFriend> Event_OnFriendInfoChange;
        public FriendShipListener()
        {
        }


        public void OnBlackAdded(IMBlack blackInfo)
        {
        }

        public void OnBlackDeleted(IMBlack blackInfo)
        {
        }

        public void OnFriendAdded(IMBlack friendInfo)
        {
        }

        public void OnFriendDeleted(IMFriend friendInfo)
        {

        }

        public void OnFriendInfoChanged(IMFriend friendInfo)
        {
            Event_OnFriendInfoChange?.Invoke(friendInfo);
        }

        public void OnFriendApplicationAdded(IMFriendApplication friendApplication)
        {
            throw new NotImplementedException();
        }

        public void OnFriendApplicationDeleted(IMFriendApplication friendApplication)
        {
            throw new NotImplementedException();
        }

        public void OnFriendApplicationAccepted(IMFriendApplication friendApplication)
        {
            throw new NotImplementedException();
        }

        public void OnFriendApplicationRejected(IMFriendApplication friendApplication)
        {
            throw new NotImplementedException();
        }

        public void OnFriendAdded(IMFriend friend)
        {
            throw new NotImplementedException();
        }
    }
}
