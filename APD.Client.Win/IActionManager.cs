using System.Threading.Tasks;

namespace APD.Client.Win
{
    public interface IActionManager
    {
        void AddAction(Task action);

        void RunActions();

        int GetQueueCount();

        Task[] GetTasks();
    }
}
