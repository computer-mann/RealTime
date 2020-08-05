using System.Threading.Tasks;

namespace realtime.Interfaces
{
    public interface ISingleChatClient
    {
         Task WelcomeAlert(string s);
         Task ReceiveMessage(string message);
    }
}