using System.Linq;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncRobotNoBeeperErrorAction : AsyncCellBlockedErrorAction
    {
        private Robot _robot = null;
        
        public AsyncRobotNoBeeperErrorAction(Robot robot, Cell cell) : base(cell)
        {
            _robot = robot;
        }

        public override void Run()
        {
            base.Run();
            
            _robot.StartCoroutine(
                Tweens.AnimateMaterialsColorCoroutine(
                    _robot.GetComponentsInChildren<Renderer>().Select(r => r.material).ToList(), 
                    Color.red, 
                    3));
        }

        public override void Update(float deltaTime) { }
    }
}