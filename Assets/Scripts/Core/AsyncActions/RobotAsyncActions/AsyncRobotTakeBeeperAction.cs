using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncRobotTakeBeeperAction : AsyncRobotAction
    {
        private Beeper _beeper;
        private AnimationClip _takeAnimation;

        public AsyncRobotTakeBeeperAction(Beeper beeper, Robot robot) : base(robot)
        {
            _beeper = beeper;
        }

        public override void Run()
        {
            // TODO: refactor to async tasks to avoid callback hell
            _beeper.StartCoroutine(Tweens.RotateTransformCoroutine(_beeper.transform, Robot.transform.rotation, 90.0f, 
                onCompleted: () =>
                {                    
                    Robot.StartCoroutine(Tweens.MoveTransformCoroutine(
                        Robot.Arms, 
                        Robot.Arms.position - Vector3.up * Robot.ArmsTravellingDistance, 
                        Robot.ArmsMovingSpeed,
                        onCompleted: () =>
                        {
                            _beeper.transform.SetParent(Robot.Arms);
                    
                            Robot.StartCoroutine(Tweens.MoveTransformCoroutine(
                                Robot.Arms, 
                                Robot.Arms.position + Vector3.up * Robot.ArmsTravellingDistance, 
                                Robot.ArmsMovingSpeed,
                                onCompleted: () => { EventActionFinished?.Invoke(this); }));
                        }));
                }));
        }

        public override void Update(float deltaTime) { }
    }
}
