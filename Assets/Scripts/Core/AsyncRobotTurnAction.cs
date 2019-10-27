using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncRobotTurnAction : AsyncRobotAction
    {
        public enum RotationDirection
        {
            Left = 1,
            Right = 2
        }

        public AsyncRobotTurnAction(Robot robot, RotationDirection direction) : base(robot)
        {
            _direction = direction;
        }

        private readonly RotationDirection _direction;
        private Quaternion _startRobotRotation;
        private Quaternion _targetRobotRotation;
        
        public override void Run()
        {
            _startRobotRotation = Robot.transform.rotation;
            _targetRobotRotation = _startRobotRotation *
                Quaternion.Euler(0.0f, _direction == RotationDirection.Left ? -90.0f : 90.0f, 0.0f);
        }

        public override void Update(float deltaTime)
        {
            var fullRotationAngle =
                (_direction == RotationDirection.Left ? -1.0f : 1.0f) * Robot.RotationSpeed * deltaTime;
            var remainingRotationAngle = Quaternion.Angle(Robot.transform.rotation, _targetRobotRotation);

            if (Mathf.Abs(remainingRotationAngle) < Mathf.Abs(fullRotationAngle))
            {
                Robot.transform.rotation = _targetRobotRotation;
                EventActionFinished?.Invoke(this);
            }
            else
            {
                Robot.transform.Rotate(0.0f, fullRotationAngle, 0.0f, Space.World);
            }
        }
    }
}