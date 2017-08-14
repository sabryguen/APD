using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APD.Client.Win
{
    /// <summary>
    /// Assumption - Project will run on .Net 4+ since I've chosen to use a ConcurrentQueue
    /// </summary>
    public sealed class ActionManager : IActionManager
    {
        // Initialise a concurrent queue to maintain order of execution.
        // not really necessary since these will be dequeued sequentially but it does offer a level 
        // of future proofing.
        private ConcurrentQueue<Task> _queue = new ConcurrentQueue<Task>();
        private ILog _logger;

        /// <summary>
        /// Constructor assumed to be initialised with a collection of actions which are then enqueued
        /// to honour the order of execution.
        /// </summary>
        /// <param name="actions">a list of tasks</param>
        public ActionManager(List<Task> actions, ILog logger)
        {
            _logger = logger;

            if (actions != null && actions.Count > 0)
            {
                foreach(Task act in actions)
                {
                    _queue.Enqueue(act);
                }
            }          
        }

        /// <summary>
        /// Adds a task for execution to the queue
        /// This satisifies the requirement to add further actions to the collection of tasks at a later point in time 
        /// to be executed.
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(Task action)
        {
            if (action != null)
            {
                _queue.Enqueue(action);
            }
        }

        public int GetQueueCount()
        {
            return _queue.Count;
        }

        public Task[] GetTasks()
        {
            return _queue.ToArray();
        }

        public void RunActions()
        {
            Task act;

            while (_queue.Count > 0)
            {
                if(_queue.TryDequeue(out act))
                {
                    try
                    {
                        // Start and execute sequentially.
                        act.Start();
                        act.Wait();
                    }
                    catch (AggregateException ae)
                    {
                        _logger.Error($"Task failed!", ae);
                    }                    
                }
            }
        }
    }
}
