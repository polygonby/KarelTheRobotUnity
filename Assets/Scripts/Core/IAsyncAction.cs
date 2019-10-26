using System;

namespace KarelTheRobotUnity.Core
{
    public interface IAsyncAction
    {
        Action<IAsyncAction> EventActionFinished { get; set; }

        void Run();
        void Update(float deltaTime);
    }    
}
