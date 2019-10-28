using System.Linq;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class CellBlockedAsyncErrorAction : AsyncCellAction, IAsyncErrorAction
    {
        private Coroutine _colorAnimationCoroutine = null;
        
        public CellBlockedAsyncErrorAction(Cell cell) : base(cell) { }

        public override void Run()
        {
            Debug.Log("Run");
            _colorAnimationCoroutine = Cell.StartCoroutine(Tweens.AnimateMaterialColorCoroutine(
                Cell.GetComponentsInChildren<Renderer>().Select(r => r.material).ToList(), Color.red, 3));
        }

        public override void Update(float deltaTime)
        {
            if (_colorAnimationCoroutine == null)
            {
                Debug.Log("Finished");
                EventActionFinished?.Invoke(this);
            }
        }
    }
}