using System;

namespace KarelTheRobotUnity.Core
{
    public abstract class AsyncCellAction : IAsyncAction
    {
        public Action<IAsyncAction> EventActionFinished { get; set; }
        
        public Cell Cell { get; set; }

        public abstract void Run();
        public abstract void Update(float deltaTime);

        public AsyncCellAction(Cell cell)
        {
            Cell = cell;
        }
    }
}