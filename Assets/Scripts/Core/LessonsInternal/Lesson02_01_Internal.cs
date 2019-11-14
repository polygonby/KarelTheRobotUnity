namespace KarelTheRobotUnity.Core
{
    public abstract class Lesson02_01_Internal : SyncActionSceneController
    {
        private bool _isBeeperDelivery = false;

        public void OnDeliverBeeperButtonClick()
        {
            ExecuteInternal(true);
        }

        public void OnTravelWithoutBeeperButtonClick()
        {
            ExecuteInternal(false);
        }

        protected bool IsBeeperDelivery()
        {
            return _isBeeperDelivery;
        }

        private void ExecuteInternal(bool isBeeperDelivery)
        {
            _isBeeperDelivery = isBeeperDelivery;
            Execute();
            _executionController.Execute();
        }

        protected abstract void Execute();

        protected override void Program() { }
    }
}
