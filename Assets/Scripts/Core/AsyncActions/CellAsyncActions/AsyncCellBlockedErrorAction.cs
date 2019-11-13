using System.Linq;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncCellBlockedErrorAction : AsyncCellAction, IAsyncErrorAction
    {
        public AsyncCellBlockedErrorAction(Cell cell) : base(cell) { }

        public override void Run()
        {
            Cell.StartCoroutine(
                Tweens.AnimateMaterialsColorCoroutine(
                    Cell.GetComponentsInChildren<Renderer>().Select(r => r.material).ToList(), 
                    Color.red, 
                    3,
                    onCompleted: () => { EventActionFinished?.Invoke(this); }));
        }
        
        public override void Update(float deltaTime) { }
    }
}
