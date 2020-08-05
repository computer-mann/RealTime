using System.Threading.Tasks;

namespace realtime.Interfaces
{
    public interface IGameClient
    {
         Task GetCoordinates(int x,int y);
         Task WelcomeAlert(string message);
    }
}