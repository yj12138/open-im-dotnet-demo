using System.Numerics;
using Dawn;
using Dawn.UI;
using IMDemo.Chat;
using ImGuiNET;
using OpenIM.IMSDK;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;
using OpenIM.Proto;

namespace IMDemo.UI
{
    public class ChatWindow : ImGuiWindow
    {
        class SendMsgCallBack : ISendMsg
        {
            public Action<int, string> OnErrorCB;
            public Action<long> OnProgressCB;
            public Action<IMMessage> OnSuccessCB;

            public void OnError(int code, string errMsg)
            {
                if (OnErrorCB != null)
                {
                    OnErrorCB(code, errMsg);
                }
            }

            public void OnProgress(long progress)
            {
                if (OnProgressCB != null)
                {
                    OnProgressCB(progress);
                }
            }
            public void OnSuccess(IMMessage msg)
            {
                throw new NotImplementedException();
            }
        }

        public static void ShowChatWindow(IMConversation conversation)
        {
            var window = GetWindow<ChatWindow>();
            window.conversation = conversation;
            window.rect.w = 600;
            window.rect.h = 500;
            window.Show(conversation.ShowName);
        }

        public IMConversation conversation;
        public List<IMMessage> historyMessage;
        public string inputTextMessage;
        public string inputFilePath;
        public string inputSoundPath;
        public override void OnEnable()
        {
            inputTextMessage = "";
            inputFilePath = "";
            inputSoundPath = "";
            OpenIMSDK.GetHistoryMessageList((resp) =>
            {
                historyMessage = [.. resp.MessageList];
            }, conversation.ConversationID, "", 100, false);
            var user = ChatMgr.Instance.currentUser;
            if (user != null)
            {
                user.messageListener.Event_OnRecvNewMessages += OnRecvMessage;
            }
        }

        public override void OnGUI()
        {
            if (conversation == null) return;

            DrawMessageList();

            DrawTextInput();
            DrawFileInput();
            DrawSoundInput();
        }

        public override void OnClose()
        {
            base.OnClose();
            var user = ChatMgr.Instance.currentUser;
            if (user != null)
            {
                user.messageListener.Event_OnRecvNewMessages -= OnRecvMessage;
            }
        }
        bool scrollToBottom = true;
        void DrawMessageList()
        {
            ImGui.BeginChild("ChatMessages", new Vector2(ImGui.GetWindowWidth() - 50, ImGui.GetWindowHeight() - 200), true, ImGuiWindowFlags.AlwaysVerticalScrollbar);

            if (historyMessage != null)
            {
                var senderNickName = "";
                foreach (var message in historyMessage)
                {
                    if (senderNickName != message.SenderNickname)
                    {
                        ImGui.Text(message.SenderNickname + ":");
                    }
                    ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 50);
                    if (message.TextElem != null)
                    {
                        ImGui.Text(message.TextElem.Content);
                    }
                    if (message.FileElem != null)
                    {
                        ImGui.Text("File -> " + message.FileElem.FileName + "=" + message.FileElem.SourceURL);
                        if (message.SendID != ChatMgr.Instance.currentUser.uid)
                        {
                            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 50);
                            if (ImGui.Button("Receive"))
                            {

                            }
                        }
                    }
                    // if (message.NotificationElem != null)
                    // {
                    //     var notification = Notification.Parse(message.ContentType, message.NotificationElem.Detail);
                    //     if (notification is FriendApplicationApprovedTips)
                    //     {
                    //         var tip = notification as FriendApplicationApprovedTips;
                    //         ImGui.Text("FriendApplicationApprovedTips:" + tip.FromToUserID.FromUserID + tip.FromToUserID.ToUserID);
                    //     }
                    // }
                    senderNickName = message.SenderNickname;
                }
                if (scrollToBottom)
                {
                    ImGui.SetScrollHereY(1.0f);
                    scrollToBottom = false;
                }
            }

            ImGui.EndChild();
        }

        void DrawTextInput()
        {
            ImGui.BeginGroup();
            {
                ImGui.Text("TextInput:");
                ImGui.InputText("##InputText", ref inputTextMessage, 1000);
                ImGui.SameLine();
                ImGui.PushID("Btn_SendText");
                if (ImGui.Button("Send"))
                {
                    Debug.Log("Send Text");
                    if (!string.IsNullOrEmpty(inputTextMessage))
                    {
                        OpenIMSDK.CreateTextMessage((msg) =>
                        {
                            SendMessage(msg);
                            inputTextMessage = "";
                        }, inputTextMessage);
                    }
                }
                ImGui.PopID();
            }
            ImGui.EndGroup();
        }

        void DrawFileInput()
        {
            ImGui.BeginGroup();
            {
                ImGui.Text("File Path:");
                ImGui.InputText("##InputFilePath:", ref inputFilePath, 1000);
                ImGui.SameLine();
                ImGui.PushID("Btn_SendFile");
                if (ImGui.Button("Send"))
                {
                    if (!string.IsNullOrEmpty(inputFilePath) && File.Exists(inputFilePath))
                    {
                        var fileName = Path.GetFileNameWithoutExtension(inputFilePath);
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            OpenIMSDK.CreateFileMessage((msg) =>
                            {
                                SendMessage(msg);
                                inputFilePath = "";
                            }, inputFilePath, fileName);
                        }
                    }
                }
                ImGui.PopID();
            }
            ImGui.EndGroup();
        }
        void DrawSoundInput()
        {
            ImGui.BeginGroup();
            {
                ImGui.Text("Sound File Path:");
                ImGui.InputText("##Sound Path:", ref inputSoundPath, 1000);
                ImGui.SameLine();
                ImGui.PushID("Btn_SendSound");
                if (ImGui.Button("Send"))
                {
                    Debug.Log("Send Sound");
                    if (!string.IsNullOrEmpty(inputSoundPath))
                    {
                        OpenIMSDK.CreateSoundMessage((msg) =>
                        {
                            SendMessage(msg);
                            inputSoundPath = "";
                        }, inputSoundPath, 1);
                    }
                }
                ImGui.PopID();
            }
            ImGui.EndGroup();

        }

        public void SendMessage(IMMessage msg)
        {
            var msgCallBack = new SendMsgCallBack();
            msgCallBack.OnSuccessCB = (_msg) =>
            {
                historyMessage.Add(_msg);
                scrollToBottom = true;
                Debug.Log("Send Suc");
            };
            msgCallBack.OnProgressCB = (progress) =>
            {
                Debug.Log("Send Progress:", progress);
            };
            msgCallBack.OnErrorCB = (errCode, errMsg) =>
            {
                Debug.Error(errMsg);
            };
            OpenIMSDK.SendMessage(msgCallBack, msg, conversation.UserID, conversation.GroupID, false);
        }

        public void OnRecvMessage(List<IMMessage> msgLists)
        {
            foreach (var msg in msgLists)
            {
                if (msg.SessionType == SessionType.Single)
                {
                    if (msg.SendID == conversation.UserID)
                    {
                        historyMessage.Add(msg);
                    }
                }
                else if (msg.SessionType == SessionType.ReadGroup)
                {
                    if (msg.RecvID == conversation.GroupID)
                    {
                        historyMessage.Add(msg);
                    }
                }
                scrollToBottom = true;
            }
        }
    }
}