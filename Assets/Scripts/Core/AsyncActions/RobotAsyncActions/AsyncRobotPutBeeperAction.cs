using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncRobotPutBeeperAction : AsyncRobotAction
    {
        private Cell _cell;

        public AsyncRobotPutBeeperAction(Cell cell, Robot robot) : base(robot)
        {
            _cell = cell;
        }

        public override void Run()
        {
            var beeper = Robot.GetComponentInChildren<Beeper>();
            
            // TODO: refactor to async tasks to avoid callback hell
            Robot.StartCoroutine(Tweens.MoveTransformCoroutine(
                Robot.Arms, 
                Robot.Arms.position - Vector3.up * Robot.ArmsTravellingDistance, 
                Robot.ArmsMovingSpeed,
                onCompleted: () =>
                {
                    beeper.transform.SetParent(_cell.transform);
                    
                    Robot.StartCoroutine(Tweens.MoveTransformCoroutine(
                        Robot.Arms, 
                        Robot.Arms.position + Vector3.up * Robot.ArmsTravellingDistance, 
                        Robot.ArmsMovingSpeed,
                        onCompleted: () => { EventActionFinished?.Invoke(this); }));
                }));
        }

        public override void Update(float deltaTime) { }
    }
}
