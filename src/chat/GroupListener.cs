using Dawn;
using OpenIM.IMSDK;
using OpenIM.IMSDK.Listener;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.Chat
{
    public class GroupListener : IGroupListener
    {

        public event Action<IMGroup> Event_OnGroupAdd;
        public event Action<IMGroup> Event_OnGroupDel;
        public event Action<IMGroup> Event_OnGroupInfoChange;
        public GroupListener()
        {
        }
        public void OnGroupInfoChanged(IMGroup groupInfo)
        {
            Event_OnGroupInfoChange?.Invoke(groupInfo);
        }


        public void OnJoinedGroupAdded(IMGroup groupInfo)
        {
            Event_OnGroupAdd?.Invoke(groupInfo);
        }

        public void OnJoinedGroupDeleted(IMGroup groupInfo)
        {
            Event_OnGroupDel?.Invoke(groupInfo);
        }

        public void OnGroupMemberAdded(IMGroupMember groupMemberInfo)
        {
            throw new NotImplementedException();
        }

        public void OnGroupMemberDeleted(IMGroupMember groupMemberInfo)
        {
            throw new NotImplementedException();
        }

        public void OnGroupApplicationAdded(IMGroupApplication groupApplication)
        {
            throw new NotImplementedException();
        }

        public void OnGroupApplicationDeleted(IMGroupApplication groupApplication)
        {
            throw new NotImplementedException();
        }

        public void OnGroupDismissed(IMGroup groupInfo)
        {
            throw new NotImplementedException();
        }

        public void OnGroupMemberInfoChanged(IMGroupMember groupMemberInfo)
        {
            throw new NotImplementedException();
        }

        public void OnGroupApplicationAccepted(IMGroupApplication groupApplication)
        {
            throw new NotImplementedException();
        }

        public void OnGroupApplicationRejected(IMGroupApplication groupApplication)
        {
            throw new NotImplementedException();
        }
    }
}

