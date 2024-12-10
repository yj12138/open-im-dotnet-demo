using Dawn;
using Dawn.UI;
using IMDemo.Chat;
using ImGuiNET;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.UI
{
    public class UserInfoWindow : ImGuiWindow
    {
        [MenuItem("User/Self UserInfo")]
        public static void ShowUserInfo()
        {
            GetWindow<UserInfoWindow>().Show("UserInfo");
        }
        [MenuItem("User/LogOut")]
        public static void LogOut()
        {
            if (ChatMgr.Instance.currentUser != null)
            {
                ChatMgr.Instance.currentUser.Logout();
            }
        }

        IMUser selfUserInfo = null;

        string nickName = "";
        string faceUrl = "";
        string ex = "";

        public override void OnEnable()
        {

            if (ChatMgr.Instance.currentUser != null)
            {
                OpenIMSDK.GetSelfUserInfo((userInfo) =>
                {
                    if (userInfo != null)
                    {
                        selfUserInfo = userInfo;
                        nickName = selfUserInfo.Nickname;
                        faceUrl = selfUserInfo.FaceURL;
                        ex = selfUserInfo.Ex;
                    }
                });
            }
        }

        public override void OnGUI()
        {
            if (selfUserInfo != null)
            {
                ImGui.Columns(2, "UserInfoColumns", false);
                ImGui.SetColumnWidth(0, 150);

                ImGui.Text("UserId");
                ImGui.NextColumn();
                ImGui.Text($"{selfUserInfo.UserID}");
                ImGui.NextColumn();

                ImGui.Text("NickName");
                ImGui.NextColumn();
                if (ImGui.InputText("##nickname", ref nickName, 100))
                {

                }
                ImGui.NextColumn();

                ImGui.Text("FaceUrl");
                ImGui.NextColumn();
                if (ImGui.InputText("##FaceURL", ref faceUrl, 100))
                {

                }
                ImGui.NextColumn();

                ImGui.Text("CreateTime");
                ImGui.NextColumn();
                ImGui.Text($"{Time.GetTimeStampStr(selfUserInfo.CreateTime / 1000)}");
                ImGui.NextColumn();

                ImGui.Text("Ex");
                ImGui.NextColumn();
                if (ImGui.InputText("##Ex", ref ex, 500))
                {

                }
                ImGui.NextColumn();

                ImGui.Text("GlobalRecvMsgOpt");
                ImGui.NextColumn();
                ImGui.Text($"{selfUserInfo.GlobalRecvMsgOpt}");

                ImGui.NextColumn();

                ImGui.Columns(1);

                if (ImGui.Button("Modify"))
                {
                    OpenIMSDK.SetSelfInfo((suc) =>
                    {
                        if (suc)
                        {
                            Close();
                        }
                    }, new SetSelfInfoReq
                    {
                        Nickname = nickName,
                        FaceURL = faceUrl,
                        Ex = ex
                    });
                }
            }
        }
    }
}