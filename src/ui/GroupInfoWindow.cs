using Dawn;
using Dawn.UI;
using ImGuiNET;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.UI
{
    public class GroupInfoWindow : ImGuiWindow
    {
        public static void ShowGroupInfo(string groupId)
        {
            var window = GetWindow<GroupInfoWindow>();
            window.groupId = groupId;
            window.rect.w = 600;
            window.rect.h = 400;
            window.Show("Group Info");
        }
        public string groupId;
        public IMGroup group;

        string groupName;
        string notification;
        string introduction;
        string ex;
        GroupNeedVerification needVerification;

        public override void OnEnable()
        {
            OpenIMSDK.GetSpecifiedGroupsInfo((groupList) =>
            {
                if (groupList != null)
                {
                    if (groupList.Length == 1)
                    {
                        group = groupList[0];
                        groupName = group.GroupName;
                        notification = group.Notification;
                        introduction = group.Introduction;
                        ex = group.Ex;
                        needVerification = group.NeedVerification;
                    }
                }
            }, [groupId]);
        }

        public override void OnGUI()
        {
            if (group == null) return;
            ImGui.Columns(2, "GroupInfoColumns", false);

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("GroupID");
            ImGui.NextColumn();
            ImGui.Text(group.GroupID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("GroupName");
            ImGui.NextColumn();
            if (ImGui.InputText("##groupName", ref groupName, 100)) { }
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Notification");
            ImGui.NextColumn();
            if (ImGui.InputText("##notification", ref notification, 100)) { }
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Introduction");
            ImGui.NextColumn();
            if (ImGui.InputText("##introduction", ref introduction, 100)) { }
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("FaceURL");
            ImGui.NextColumn();
            ImGui.Text(group.FaceURL);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("CreateTime");
            ImGui.NextColumn();
            ImGui.Text($"{Time.GetTimeStampStr(group.CreateTime / 1000)}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Status");
            ImGui.NextColumn();
            ImGui.Text($"{group.Status}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("CreatorUserID");
            ImGui.NextColumn();
            ImGui.Text(group.CreatorUserID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("OwnerUserID");
            ImGui.NextColumn();
            ImGui.Text(group.OwnerUserID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("MemberCount");
            ImGui.NextColumn();
            ImGui.Text($"{group.MemberCount}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Ex");
            ImGui.NextColumn();
            ImGui.InputText("##Ex", ref ex, 1000);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("AttachedInfo");
            ImGui.NextColumn();
            ImGui.Text(group.AttachedInfo);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("NeedVerification");
            ImGui.NextColumn();
            ImGui.Text(group.NeedVerification.ToString());
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("LookMemberInfo");
            ImGui.NextColumn();
            ImGui.Text($"{group.LookMemberInfo}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("ApplyMemberFriend");
            ImGui.NextColumn();
            ImGui.Text($"{group.ApplyMemberFriend}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("NotificationUpdateTime");
            ImGui.NextColumn();
            ImGui.Text($"{group.NotificationUpdateTime}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("NotificationUserID");
            ImGui.NextColumn();
            ImGui.Text(group.NotificationUserID);
            ImGui.NextColumn();

            ImGui.Columns(1);

            if (ImGui.Button("Modify"))
            {
                OpenIMSDK.SetGroupInfo((suc) =>
                {
                    if (suc)
                    {
                        Close();
                    }
                }, new SetGroupInfoReq()
                {
                    GroupID = group.GroupID,
                    GroupName = group.GroupName,
                    Notification = group.Notification,
                    Introduction = group.Introduction,
                    FaceURL = group.FaceURL,
                    Ex = group.Ex,
                });
            }
        }
    }
}