using Dawn;
using Dawn.UI;
using IMDemo.Chat;
using ImGuiNET;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.UI
{
    public class ConversationListWindow : ImGuiWindow
    {
        [MenuItem("Conversation/Conversation List")]
        public static void ShowConversationList()
        {
            var window = GetWindow<ConversationListWindow>();
            window.rect.w = 600;
            window.rect.h = 400;
            window.Show("Conversation List");
        }

        List<IMConversation> conversationList = new List<IMConversation>();
        public override void OnEnable()
        {
            ChatMgr.Instance.currentUser.conversationListener.Event_OnConversationsInfoChange += OnConversationsInfoChange;

            RefreshConversationList();
        }
        public override void OnClose()
        {
            base.OnClose();

            ChatMgr.Instance.currentUser.conversationListener.Event_OnConversationsInfoChange -= OnConversationsInfoChange;
        }
        void OnConversationsInfoChange(List<IMConversation> conversations)
        {
            RefreshConversationList();
        }
        public void RefreshConversationList()
        {
            OpenIMSDK.GetAllConversationList((list) =>
            {
                if (list != null)
                {
                    conversationList.Clear();
                    conversationList.AddRange(list);
                }
            });
        }
        public override void OnGUI()
        {
            if (conversationList == null) return;
            ImGui.BeginChild("TableContainer", new System.Numerics.Vector2(0, rect.h - 50), true, ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.AlwaysVerticalScrollbar);

            if (ImGui.BeginTable("GroupInfoTable", 6, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollX))
            {
                ImGui.TableSetupColumn("ShowName");
                ImGui.TableSetupColumn("FaceURL");
                ImGui.TableSetupColumn("UnreadCount");
                ImGui.TableSetupColumn("Chat");
                ImGui.TableSetupColumn("Delete");
                ImGui.TableSetupColumn("Detail");

                ImGui.TableHeadersRow();

                for (int i = 0; i < conversationList.Count; i++)
                {
                    var conversation = conversationList[i];
                    ImGui.TableNextRow();

                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(conversation.ShowName);

                    ImGui.TableSetColumnIndex(1);
                    ImGui.Text(conversation.FaceURL);

                    ImGui.TableSetColumnIndex(2);
                    ImGui.Text(conversation.UnreadCount.ToString());

                    ImGui.TableSetColumnIndex(3);

                    ImGui.PushID("Btn_Chat" + i);
                    if (ImGui.Button("Chat"))
                    {
                        OnChatClick(conversation);
                    }
                    ImGui.PopID();

                    ImGui.TableSetColumnIndex(4);
                    ImGui.PushID("Btn_Delete" + i);
                    if (ImGui.Button("Delete"))
                    {
                        OnDeleteClick(conversation);
                    }
                    ImGui.PopID();

                    ImGui.TableSetColumnIndex(5);

                    ImGui.PushID("Btn_Detail" + i);
                    if (ImGui.Button("Detail"))
                    {
                        OnDetailClick(conversation);
                    }
                    ImGui.PopID();
                }

                ImGui.EndTable();
            }

            ImGui.EndChild();
        }

        void OnChatClick(IMConversation conversation)
        {
            ChatWindow.ShowChatWindow(conversation);
        }
        void OnDeleteClick(IMConversation conversation)
        {
            OpenIMSDK.DeleteConversationAndDeleteAllMsg((suc) =>
            {
                if (suc)
                {
                    RefreshConversationList();
                }
            }, conversation.ConversationID);
        }
        void OnDetailClick(IMConversation conversation)
        {
            ConversationInfoWindow.ShowConversationInfo(conversation.ConversationID);
        }
    }
}