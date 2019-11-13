using KarelTheRobotUnity.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace TestNamespace
{
    public class Lesson01 : SyncActionSceneController
    {
        protected override void Program()
        {
            Move();
            Move();
            TakeBeeper();
            Move();
            
            TurnLeft();
            TurnLeft();
            Move();
            Move();
            TurnRight();
            Move();
            PutBeeper();
            Move();
        }
    }
}
