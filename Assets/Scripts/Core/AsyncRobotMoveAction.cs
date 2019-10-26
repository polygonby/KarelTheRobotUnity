using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncRobotMoveAction : AsyncRobotAction
    {
        public AsyncRobotMoveAction(Robot robot) : base(robot) { }

        private Vector3 _startingRobotPosition;
        private Vector3 _targetRobotPosition;
        private Vector3 _movingVector;
        
        public override void Run()
        {
            _startingRobotPosition = Robot.transform.position;
            _targetRobotPosition = _startingRobotPosition + Robot.transform.forward * 1.0f;
            _movingVector = (_targetRobotPosition - _startingRobotPosition).normalized;
        }

        public override void Update(float deltaTime)
        {
            var fullMovingVector = _movingVector * Robot.MoveSpeed * deltaTime;
            var vectorToTarget = _targetRobotPosition - Robot.transform.position;

            if (vectorToTarget.sqrMagnitude < fullMovingVector.sqrMagnitude)
            {
                Robot.transform.position = _targetRobotPosition;
                EventActionFinished?.Invoke(this);
            }
            else
            {
                Robot.transform.Translate(fullMovingVector, Space.World);
            }
        }
    }
}
