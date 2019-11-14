namespace KarelTheRobotUnity.Core
{
    public abstract class Lesson02_02_Internal : SyncActionSceneController
    {
        private int _target = -1;

        public void OnDeliverBeeperTo1ButtonClick()
        {
            ExecuteInternal(0);
        }

        public void OnDeliverBeeperTo2ButtonClick()
        {
            ExecuteInternal(1);
        }

        public void OnDeliverBeeperTo3ButtonClick()
        {
            ExecuteInternal(2);
        }

        protected int GetTargetIndex()
        {
            return _target;
        }

        private void ExecuteInternal(int target)
        {
            _target = target;
            Execute();
            _executionController.Execute();
        }

        protected abstract void Execute();

        protected override void Program() { }
    }
}
