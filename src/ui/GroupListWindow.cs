using Dawn;
using Dawn.UI;
using IMDemo.Chat;
using ImGuiNET;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.UI
{
    public class GroupListWindow : ImGuiWindow
    {
        [MenuItem("Group/Group List")]
        public static void ShowGroupList()
        {
            var window = GetWindow<GroupListWindow>();
            window.rect.w = 600;
            window.rect.h = 400;
            window.Show("Group List");
        }

        List<IMGroup> groupList = new List<IMGroup>();
        public override void OnEnable()
        {
            RefreshGroupList();

            ChatMgr.Instance.currentUser.groupListener.Event_OnGroupInfoChange += OnGroupInfoChange;
        }
        public override void OnClose()
        {
            base.OnClose();
            ChatMgr.Instance.currentUser.groupListener.Event_OnGroupInfoChange -= OnGroupInfoChange;
        }
        void RefreshGroupList()
        {
            OpenIMSDK.GetJoinedGroups((list) =>
            {
                if (list != null)
                {
                    groupList.Clear();
                    groupList.AddRange(list);
                }
            });
        }

        void OnGroupInfoChange(IMGroup group)
        {
            RefreshGroupList();
        }

        public override void OnGUI()
        {
            if (groupList == null) return;
            ImGui.BeginChild("TableContainer", new System.Numerics.Vector2(0, rect.h - 50), true, ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.AlwaysVerticalScrollbar);

            if (ImGui.BeginTable("GroupListTable", 6, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollX))
            {
                ImGui.TableSetupColumn("GroupName");
                ImGui.TableSetupColumn("FaceURL");
                ImGui.TableSetupColumn("MemberCount");
                ImGui.TableSetupColumn("Chat");
                ImGui.TableSetupColumn("Delete");
                ImGui.TableSetupColumn("Detail");

                ImGui.TableHeadersRow();

                for (int i = 0; i < groupList.Count; i++)
                {
                    var group = groupList[i];
                    ImGui.TableNextRow();

                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(group.GroupName);

                    ImGui.TableSetColumnIndex(1);
                    ImGui.Text(group.FaceURL);

                    ImGui.TableSetColumnIndex(2);
                    ImGui.Text(group.MemberCount.ToString());

                    ImGui.TableSetColumnIndex(3);
                    ImGui.PushID("Btn_Chat" + i);
                    if (ImGui.Button("Chat"))
                    {
                        OnChatClick(group);
                    }
                    ImGui.PopID();
                    ImGui.TableSetColumnIndex(4);
                    ImGui.PushID("Btn_Delete" + i);
                    if (ImGui.Button("Delete"))
                    {
                        OnDeleteClick(group);
                    }
                    ImGui.PopID();
                    ImGui.TableSetColumnIndex(5);
                    ImGui.PushID("Btn_Detail" + i);
                    if (ImGui.Button("Detail"))
                    {
                        OnDetailClick(group);
                    }
                    ImGui.PopID();
                }

                ImGui.EndTable();
            }

            ImGui.EndChild();
        }

        void OnChatClick(IMGroup group)
        {
            OpenIMSDK.GetOneConversation((converstion) =>
            {
                if (converstion != null)
                {
                    ChatWindow.ShowChatWindow(converstion);
                }
            }, SessionType.ReadGroup, group.GroupID);
        }
        void OnDetailClick(IMGroup group)
        {
            GroupInfoWindow.ShowGroupInfo(group.GroupID);
        }
        void OnDeleteClick(IMGroup group)
        {
            if (group.OwnerUserID == ChatMgr.Instance.currentUser.uid)
            {
                OpenIMSDK.DismissGroup((suc) =>
                {
                    if (suc)
                    {
                        RefreshGroupList();
                    }
                }, group.GroupID);
            }
            else
            {
                OpenIMSDK.QuitGroup((suc) =>
                {
                    if (suc)
                    {
                        RefreshGroupList();
                    }
                }, group.GroupID);
            }
        }
    }
}