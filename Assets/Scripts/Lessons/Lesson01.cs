using KarelTheRobotUnity.Core;

public class Lesson01 : SyncActionSceneController
{
    protected override void Program()
    {
        Move();
        TurnLeft();
        Move();
        Move();
        TurnRight();
        Move();
    }
}
