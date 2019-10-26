using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class ExecutionController : MonoBehaviour
    {
        public Field Field { get; private set; }
        public List<Robot> Robots { get; private set; }

        private Queue<IAsyncAction> _actions = new Queue<IAsyncAction>();
        private IAsyncAction _currentAction;

        private void Awake()
        {
            Field = FindObjectOfType<Field>();
            Robots = FindObjectsOfType<Robot>().ToList();
        }

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
            _currentAction.EventActionFinished += OnActionFinished;
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
