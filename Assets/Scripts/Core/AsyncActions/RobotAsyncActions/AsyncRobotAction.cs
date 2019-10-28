using System;

namespace KarelTheRobotUnity.Core
{
    public abstract class AsyncRobotAction : IAsyncAction
    {
        public Action<IAsyncAction> EventActionFinished { get; set; }

        public Robot Robot { get; private set; }

        public abstract void Run();
        public abstract void Update(float deltaTime);

        public AsyncRobotAction(Robot robot)
        {
            Robot = robot;
        }
    }
}
