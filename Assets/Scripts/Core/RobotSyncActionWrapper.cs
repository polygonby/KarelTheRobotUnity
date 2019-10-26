namespace KarelTheRobotUnity.Core
{
    public class RobotSyncActionWrapper
    {
        public readonly Robot Robot;
        
        private readonly AsyncExecutionController _executionController;
        
        public RobotSyncActionWrapper(Robot robot, AsyncExecutionController executionController)
        {
            Robot = robot;
            _executionController = executionController;
        }

        public void Move()
        {
            _executionController.AddAction(new AsyncRobotMoveAction(Robot));
        }

        public void TurnLeft()
        {
            _executionController.AddAction(new AsyncRobotTurnAction(Robot, AsyncRobotTurnAction.RotationDirection.Left));
        }
        
        public void TurnRight()
        {
            _executionController.AddAction(new AsyncRobotTurnAction(Robot, AsyncRobotTurnAction.RotationDirection.Right));
        }
    }
}
