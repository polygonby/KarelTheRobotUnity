using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class AsyncExecutionController : MonoBehaviour
    {
        private readonly Queue<IAsyncAction> _actions = new Queue<IAsyncAction>();
        private IAsyncAction _currentAction;

        private void Update()
        {
            _currentAction?.Update(Time.deltaTime);
        }

        public void AddAction(IAsyncAction action)
        {
            _actions.Enqueue(action);
        }

        public void Execute()
        {
            TryExecuteNextAction();
        }

        private void TryExecuteNextAction()
        {
            if (_actions.Count > 0)
            {
                ExecuteNextAction(_actions.Dequeue());
            }
        }
        
        private void ExecuteNextAction(IAsyncAction action)
        {
            _currentAction = action;
            
            if (!(_currentAction is IAsyncErrorAction))
            {
                _currentAction.EventActionFinished += OnActionFinished;
            }
            
            _currentAction.Run();
        }

        private void OnActionFinished(IAsyncAction action)
        {
            // ReSharper disable once DelegateSubtraction
            action.EventActionFinished -= OnActionFinished;
            TryExecuteNextAction();
        }
    }
}
