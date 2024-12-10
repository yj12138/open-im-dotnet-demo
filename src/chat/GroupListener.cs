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
        }

        public void OnGroupMemberDeleted(IMGroupMember groupMemberInfo)
        {
        }

        public void OnGroupApplicationAdded(IMGroupApplication groupApplication)
        {
        }

        public void OnGroupApplicationDeleted(IMGroupApplication groupApplication)
        {
        }

        public void OnGroupDismissed(IMGroup groupInfo)
        {
        }

        public void OnGroupMemberInfoChanged(IMGroupMember groupMemberInfo)
        {
        }

        public void OnGroupApplicationAccepted(IMGroupApplication groupApplication)
        {
        }

        public void OnGroupApplicationRejected(IMGroupApplication groupApplication)
        {
        }
    }
}

