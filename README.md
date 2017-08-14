# APD
Test for APD contract

The solution consists of two projects - the first is a very simple Windows forms project with two buttons
one button to execute the actions and the other to add further tasks to a queue.

The second project is a unit test project which exercises some of the features of the ActionManager class.

The Tasks in the win forms project are as follows:

Two simple ones that log different thigs including the managed thread ID to demonstrate that the tasks do run on different threads.
a further task that demonstrates a longer running task
and a final one that intentially throws and exception to show that this is handled and caught as an AggregateException in the ActionManager class
