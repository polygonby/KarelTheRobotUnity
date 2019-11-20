using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public abstract class Lesson03_02_Internal : SyncActionSceneController
    {
        private Vector2Int _randomizedCellCoordinates;

        public Vector2Int GetTargetCellCoordinates()
        {
            return _randomizedCellCoordinates;
        }

        protected abstract void Execute();

        protected override void Program()
        {
            RandomizeTargetCell();
            Execute();
        }

        private void RandomizeTargetCell()
        {
            var field = FindObjectOfType<Field>();
            _randomizedCellCoordinates = new Vector2Int(Random.Range(1, field.Size.x - 1), Random.Range(4, field.Size.y));
            field.GetCell(_randomizedCellCoordinates).GetComponentInChildren<MeshRenderer>().material = MainSettings.Instance.TargetCellMaterial;
        }
    }
}
