using Dawn;
using Dawn.UI;
using IMDemo.Chat;
using ImGuiNET;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.UI
{
    public class FriendInfoWindow : ImGuiWindow
    {
        public static void ShowFriendInfo(string friendUserId)
        {
            var window = GetWindow<FriendInfoWindow>();
            window.friendUserId = friendUserId;
            window.rect.w = 600;
            window.rect.h = 400;
            window.Show("Friend");
        }
        public string friendUserId;
        public IMFriend friend;
        string remark;
        bool isPinned;
        string ex;
        public override void OnEnable()
        {
            OpenIMSDK.GetSpecifiedFriends((list) =>
            {
                if (list != null && list.Length == 1)
                {
                    friend = list[0];
                    remark = friend.Remark;
                    isPinned = friend.IsPinned;
                    ex = friend.Ex;
                }
            }, [friendUserId], true);
        }
        public override void OnClose()
        {
            base.OnClose();
            friend = null;
        }

        public override void OnGUI()
        {
            if (friend == null) return;
            ImGui.Columns(2, "FriendInfoColumns", false);

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("OwnerUserID");
            ImGui.NextColumn();
            ImGui.Text(friend.OwnerUserID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("FriendUserID");
            ImGui.NextColumn();
            ImGui.Text(friend.FriendUserID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Remark");
            ImGui.NextColumn();
            if (ImGui.InputText("##remark", ref remark, 100)) { }
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("CreateTime");
            ImGui.NextColumn();
            ImGui.Text($"{Time.GetTimeStampStr(friend.CreateTime / 1000)}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("AddSource");
            ImGui.NextColumn();
            ImGui.Text($"{friend.AddSource}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("OperatorUserID");
            ImGui.NextColumn();
            ImGui.Text(friend.OperatorUserID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Nickname");
            ImGui.NextColumn();
            ImGui.Text(friend.Nickname);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("FaceURL");
            ImGui.NextColumn();
            ImGui.Text(friend.FaceURL);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("AttachedInfo");
            ImGui.NextColumn();
            ImGui.Text(friend.AttachedInfo);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("IsPinned");
            ImGui.NextColumn();
            ImGui.Checkbox("##isPinned", ref isPinned);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Ex");
            ImGui.NextColumn();
            if (ImGui.InputText("##ex", ref ex, 500)) { }
            ImGui.NextColumn();

            ImGui.Columns(1);

            if (ImGui.Button("Modify"))
            {
                OpenIMSDK.UpdateFriend((suc) =>
                {
                    if (suc)
                    {
                        Close();
                    }
                }, friend.FriendUserID, isPinned, remark, ex);
            }
        }
    }
}