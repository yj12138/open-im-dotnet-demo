using Dawn;
using Dawn.UI;
using ImGuiNET;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;
using OpenIM.Proto;

namespace IMDemo.UI
{
    public class ConversationInfoWindow : ImGuiWindow
    {
        public static void ShowConversationInfo(string conversationId)
        {
            var window = GetWindow<ConversationInfoWindow>();
            window.conversationId = conversationId;
            window.rect.w = 600;
            window.rect.h = 400;
            window.Show("Conversation");
        }
        public string conversationId;
        public IMConversation conversation;

        string draftText;
        bool isPinned;
        bool isPrivateChat;
        int burnDuration;
        string ex;

        public override void OnEnable()
        {
            conversation = null;
            OpenIMSDK.GetMultipleConversation((list) =>
            {
                if (list != null)
                {
                    if (list.Length == 1)
                    {
                        conversation = list[0];
                        draftText = conversation.DraftText;
                        isPinned = conversation.IsPinned;
                        isPrivateChat = conversation.IsPrivateChat;
                        burnDuration = conversation.BurnDuration;
                        ex = conversation.Ex;
                    }
                }
            }, [conversationId]);
        }


        public override void OnClose()
        {
            base.OnClose();
            conversation = null;

        }

        public override void OnGUI()
        {
            if (conversation == null) return;
            ImGui.Columns(2, "ConversationInfoColumns", false);

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("ConversationID");
            ImGui.NextColumn();
            ImGui.Text(conversation.ConversationID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("ConversationType");
            ImGui.NextColumn();
            ImGui.Text($"{conversation.ConversationType}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("UserID");
            ImGui.NextColumn();
            ImGui.Text(conversation.UserID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("GroupID");
            ImGui.NextColumn();
            ImGui.Text(conversation.GroupID);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("ShowName");
            ImGui.NextColumn();
            ImGui.Text(conversation.ShowName);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("FaceURL");
            ImGui.NextColumn();
            ImGui.Text(conversation.FaceURL);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("RecvMsgOpt");
            ImGui.NextColumn();
            ImGui.Text($"{conversation.RecvMsgOpt}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("UnreadCount");
            ImGui.NextColumn();
            ImGui.Text($"{conversation.UnreadCount}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("GroupAtType");
            ImGui.NextColumn();
            ImGui.Text($"{conversation.GroupAtType}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("LatestMsg");
            ImGui.NextColumn();
            if (conversation.LatestMsg != null)
            {
                ImGui.Text(conversation.LatestMsg.ClientMsgID);
            }
            else
            {
                ImGui.Text("");
            }
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("LatestMsgSendTime");
            ImGui.NextColumn();
            ImGui.Text($"{conversation.LatestMsgSendTime}");
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("DraftText");
            ImGui.NextColumn();
            if (ImGui.InputText("##draftText", ref draftText, 500)) { }
            ImGui.SameLine();
            if (ImGui.Button("SetDraftText"))
            {
                OpenIMSDK.SetConversationDraft((suc) =>
                {
                    if (suc)
                    {
                        OpenIMSDK.GetOneConversation((newConversation) =>
                       {
                           if (newConversation != null)
                           {
                               conversation = newConversation;
                           }
                       }, conversation.ConversationType, conversation.ConversationType == SessionType.Single ? conversation.UserID : conversation.GroupID);
                    }
                }, conversationId, conversation.DraftText);
            }
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("DraftTextTime");
            ImGui.NextColumn();
            ImGui.Text($"{Time.GetTimeStampStr(conversation.DraftTextTime / 1000)}");

            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("IsPinned");
            ImGui.NextColumn();
            ImGui.Checkbox("##IsPinned", ref isPinned);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("IsPrivateChat");
            ImGui.NextColumn();
            ImGui.Checkbox("##IsPrivateChat", ref isPrivateChat);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("BurnDuration");
            ImGui.NextColumn();
            ImGui.InputInt("##BurnDuration", ref burnDuration);
            ImGui.NextColumn();

            ImGui.SetColumnWidth(0, 150);
            ImGui.Text("Ex");
            ImGui.NextColumn();
            ImGui.InputText("##Ex", ref ex, 500);
            ImGui.NextColumn();

            ImGui.Columns(1);

            if (ImGui.Button("Modify"))
            {
                OpenIMSDK.SetConversation((suc) =>
                {
                    if (suc)
                    {
                        Close();
                    }
                }, new SetConversationReq()
                {
                    ConversationID = conversation.ConversationID,
                    IsPinned = isPinned,
                    IsPrivateChat = isPrivateChat,
                    Ex = ex,
                    BurnDuration = burnDuration,
                });
            }
        }
    }
}