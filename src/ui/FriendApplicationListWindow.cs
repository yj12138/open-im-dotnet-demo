using Dawn;
using Dawn.UI;
using ImGuiNET;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.UI
{
    public class FriendApplicationListWindow : ImGuiWindow
    {
        [MenuItem("Friend/Friend Application List")]
        public static void Show()
        {
            var window = GetWindow<FriendApplicationListWindow>();
            window.rect.w = 600;
            window.rect.h = 400;
            window.Show("Friend Application List");
        }
        List<IMFriendApplication> applicantList = new List<IMFriendApplication>();
        List<IMFriendApplication> recipientList = new List<IMFriendApplication>();
        public override void OnEnable()
        {
            RefreshApplicantList();
            RefreshRecipientList();
        }
        void RefreshApplicantList()
        {
            OpenIMSDK.GetFriendApplication((list) =>
            {
                if (list != null)
                {
                    applicantList.Clear();
                    applicantList.AddRange(list);
                }
            }, true);
        }
        void RefreshRecipientList()
        {
            OpenIMSDK.GetFriendApplication((list) =>
            {
                if (list != null)
                {
                    recipientList.Clear();
                    recipientList.AddRange(list);
                }
            }, false);
        }

        public override void OnGUI()
        {
            if (applicantList != null)
            {
                ImGui.Text("ApplicantList:");
                ImGui.BeginChild("ApplicantList", new System.Numerics.Vector2(0, rect.h / 2), true, ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.AlwaysVerticalScrollbar);

                if (ImGui.BeginTable("ApplicantListTable", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollX))
                {
                    ImGui.TableSetupColumn("FromNickName");
                    ImGui.TableSetupColumn("ToNickName");
                    ImGui.TableSetupColumn("ReqMsg");
                    ImGui.TableSetupColumn("Result");

                    ImGui.TableHeadersRow();

                    foreach (var application in applicantList)
                    {
                        ImGui.TableNextRow();

                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text(application.FromNickname);

                        ImGui.TableSetColumnIndex(1);
                        ImGui.Text(application.ToNickname);

                        ImGui.TableSetColumnIndex(2);
                        ImGui.Text(application.ReqMsg);

                        ImGui.TableSetColumnIndex(3);
                        HandleResult result = (HandleResult)application.HandleResult;
                        ImGui.Text(result.ToString());

                    }

                    ImGui.EndTable();
                }

                ImGui.EndChild();
            }

            ImGui.Text("RecipientList:");
            if (recipientList != null)
            {
                ImGui.BeginChild("RecipientList", new System.Numerics.Vector2(0, rect.h / 2), true, ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.AlwaysVerticalScrollbar);

                if (ImGui.BeginTable("RecipientListTable", 6, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollX))
                {
                    ImGui.TableSetupColumn("FromNickName");
                    ImGui.TableSetupColumn("ToNickName");
                    ImGui.TableSetupColumn("ReqMsg");
                    ImGui.TableSetupColumn("Result");
                    ImGui.TableSetupColumn("Agree");
                    ImGui.TableSetupColumn("Refuse");


                    ImGui.TableHeadersRow();

                    foreach (var application in recipientList)
                    {
                        ImGui.TableNextRow();

                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text(application.FromNickname);

                        ImGui.TableSetColumnIndex(1);
                        ImGui.Text(application.ToNickname);

                        ImGui.TableSetColumnIndex(2);
                        ImGui.Text(application.ReqMsg);

                        ImGui.TableSetColumnIndex(3);
                        HandleResult result = application.HandleResult;
                        ImGui.Text(result.ToString());

                        ImGui.TableSetColumnIndex(4);
                        if (result == HandleResult.ResultDefault)
                        {
                            if (ImGui.Button("Agree"))
                            {
                                OnAgreeClick(application);
                            }
                        }

                        ImGui.TableSetColumnIndex(5);
                        if (result == HandleResult.ResultDefault)
                        {
                            if (ImGui.Button("Refuse"))
                            {
                                OnRefuseClick(application);
                            }
                        }
                    }

                    ImGui.EndTable();
                }

                ImGui.EndChild();
            }
        }

        void OnAgreeClick(IMFriendApplication applicationInfo)
        {
            OpenIMSDK.HandleFriendApplication((suc) =>
            {
                if (suc)
                {
                    RefreshRecipientList();
                }
            }, applicationInfo.FromUserID, "i agree", ApprovalStatus.Approved);
        }
        void OnRefuseClick(IMFriendApplication applicationInfo)
        {
            OpenIMSDK.HandleFriendApplication((suc) =>
            {
                if (suc)
                {
                    RefreshRecipientList();
                }
            }, applicationInfo.FromUserID, "i agree", ApprovalStatus.Rejected);
        }
    }
}