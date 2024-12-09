using Dawn;
using OpenIM.IMSDK;
using OpenIM.IMSDK.Listener;
using OpenIM.Proto;
using OpenIMSDK = OpenIM.IMSDK.IMSDK;

namespace IMDemo.Chat
{
    public class MessageListener : IMessageListener
    {
        public event Action<List<IMMessage>> Event_OnRecvNewMessages;
        public MessageListener()
        {
        }

        public void OnRecvNewMessages(List<IMMessage> messageList)
        {
            Event_OnRecvNewMessages?.Invoke(messageList);
        }

        public void OnRecvOfflineNewMessages(List<IMMessage> messageList)
        {

        }

        public void OnRecvNewMessage(IMMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnRecvC2CReadReceipt(MessageReceipt[] msgReceiptList)
        {
            throw new NotImplementedException();
        }

        public void OnNewRecvMessageRevoked(RevokedTips revokedTips)
        {
            throw new NotImplementedException();
        }

        public void OnRecvOfflineNewMessage(IMMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnMessageDeleted(IMMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnRecvOnlineOnlyMessage(IMMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnMessageEdited(IMMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
