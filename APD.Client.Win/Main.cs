using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APD.Client.Win
{
    public partial class Main : Form
    {
        private IActionManager _actionManager;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public Main()
        {
            InitializeComponent();

            // Give the main thread a name just for clarity. we'll use the thread Ids on other threads to simply show that 
            // some of the tasks are running on dfferent threads.
            Thread.CurrentThread.Name = "Main";

            log.Info($"Hello from Form running on Thread:{Thread.CurrentThread.Name} - id: {Thread.CurrentThread.ManagedThreadId}");

            Task taskA = new Task(() => log.Info($"Hello from task A. on Thread:{Thread.CurrentThread.ManagedThreadId}"));
            Task taskB = new Task(() => log.Info($"Hello from task B. on Thread:{Thread.CurrentThread.ManagedThreadId}"));

            Task longRunning = new Task(() =>
            {
                log.Info($"Hello from Long Running. on Thread:{Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 1000000000; i++)
                { }
                log.Info($"Long Running - Done. on Thread:{Thread.CurrentThread.ManagedThreadId}");
            });

            // Simulate a task breaking to show that the RunActions method has handling for errors.
            Task badTask = new Task(() =>
            {
                log.Info($"Hello from Bad Task on Thread:{Thread.CurrentThread.ManagedThreadId}");
                throw new ApplicationException("This task is faulty");
            });

            List<Task> actions = new List<Task>
            {
                taskA,
                taskB,
                longRunning,
                badTask
            };

            _actionManager = new ActionManager(actions, log);
        }

        private void btnRunActions_Click(object sender, EventArgs e)
        {
            _actionManager.RunActions();
        }

        private void btnAddAction_Click(object sender, EventArgs e)
        {
            Task taskC = new Task(() => Console.WriteLine($"Hello from task C. on Thread:{Thread.CurrentThread.ManagedThreadId}"));
            _actionManager.AddAction(taskC);
        }
    }
}
