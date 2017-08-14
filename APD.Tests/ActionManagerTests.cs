using APD.Client.Win;
using log4net;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APD.Tests
{
    [TestFixture]
    public class ActionManagerTests
    {

        // *The actions must be executed sequentially – one at a time 
        // *The actions must be executed in the order that they were added to the class 
        // *The actions are not necessarily added all at the same time
        // * The actions are not necessarily all executed on the same thread 
        // - Demonstrated in the console and log4Net logs so no tests for this requirement

        [Test]
        public void EnsureNothingEnqueuedInternally()
        {
            // Arrange
            Mock<ILog> mockLogger = new Mock<ILog>();

            // Act
            IActionManager manager = new ActionManager(new List<Task>(), mockLogger.Object);
            manager.RunActions();

            // Assert
            Assert.AreEqual(0, manager.GetQueueCount());
        }

        [Test]
        public void EnsureQueueLoaded()
        {
            // Arrange
            Mock<ILog> mockLogger = new Mock<ILog>();

            List<Task> tasks = new List<Task>();

            Task taskA = new Task(() => Console.WriteLine("A"));
            Task taskB = new Task(() => Console.WriteLine("B"));
            tasks.Add(taskA);
            tasks.Add(taskB);

            // Act
            IActionManager manager = new ActionManager(tasks, mockLogger.Object);

            // Assert
            Assert.AreEqual(2, manager.GetQueueCount());
        }

        [Test]
        public void EnsureDequeue()
        {
            // Arrange
            Mock<ILog> mockLogger = new Mock<ILog>();

            List<Task> tasks = new List<Task>();

            Task taskA = new Task(() => Console.WriteLine("A"));
            Task taskB = new Task(() => Console.WriteLine("B"));
            tasks.Add(taskA);
            tasks.Add(taskB);

            // Act
            IActionManager manager = new ActionManager(tasks, mockLogger.Object);
            manager.RunActions();

            // Assert
            Assert.AreEqual(0, manager.GetQueueCount());
        }


        [Test]
        public void EnsureActionsExecutedInOrderAddedToClassSequentially()
        {
            // Arrange
            Mock<ILog> mockLogger = new Mock<ILog>();

            List<Task> tasks = new List<Task>();
            List<string> results = new List<string>();

            Task taskA = new Task(() => results.Add("A"));
            Task taskB = new Task(() => results.Add("B"));
            tasks.Add(taskA);
            tasks.Add(taskB);

            // Act
            IActionManager manager = new ActionManager(tasks, mockLogger.Object);

            Task[] tasksFromQueue = manager.GetTasks();
            Assert.AreEqual(2, tasksFromQueue.Length);
            
            manager.RunActions();

            Assert.AreEqual("A", results[0]);
            Assert.AreEqual("B", results[1]);
            
            // Assert
            Assert.AreEqual(0, manager.GetQueueCount());
        }

        [Test]
        public void EnsureActionsCanBeQueuedAfterInitialLoad()
        {
            // Arrange
            Mock<ILog> mockLogger = new Mock<ILog>();

            List<Task> tasks = new List<Task>();
            List<string> results = new List<string>();

            Task taskA = new Task(() => results.Add("A"));
            Task taskB = new Task(() => results.Add("B"));
            tasks.Add(taskA);
            tasks.Add(taskB);

            // Act
            IActionManager manager = new ActionManager(tasks, mockLogger.Object);

            Task[] tasksFromQueue = manager.GetTasks();
            Assert.AreEqual(2, tasksFromQueue.Length);

            Task taskC = new Task(() => results.Add("C"));

            manager.AddAction(taskC);

            tasksFromQueue = manager.GetTasks();
            Assert.AreEqual(3, tasksFromQueue.Length);

            manager.RunActions();
            
            Assert.AreEqual("A", results[0]);
            Assert.AreEqual("B", results[1]);
            Assert.AreEqual("C", results[2]);
            
            // Assert
            Assert.AreEqual(0, manager.GetQueueCount());

            // empty at this point so show that we can queue again
            manager.AddAction(taskC);

            tasksFromQueue = manager.GetTasks();
            Assert.AreEqual(1, tasksFromQueue.Length);
        }
    }
}
