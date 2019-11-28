using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarelTheRobotUnity.Core
{
    public abstract class Lesson06_01_Internal : SyncActionSceneController
    {
        public enum BeeperType
        {
            Undefined = 0,
            Blue = 1,
            Red = 2
        }

        private List<Beeper> _randomizedBeepers;
        private BeeperType _deliveredBeepersType;
        
        public List<Beeper> GetBeepers()
        {
            return _randomizedBeepers;
        }

        public BeeperType GetDeliveredBeepersType()
        {
            return _deliveredBeepersType;
        }

        public void OnDeliverRedBeepersButtonClick()
        {
            _deliveredBeepersType = BeeperType.Red;
            Execute();
            _executionController.Execute();
        }

        public void OnDeliverBlueBeepersButtonClick()
        {
            _deliveredBeepersType = BeeperType.Blue;
            Execute();
            _executionController.Execute();
        }

        protected abstract void Execute();

        protected override void Program()
        {
            RandomizeBeepers();
            RandomizeRobotsRotations();
        }

        private void RandomizeBeepers()
        {
            var field = FindObjectOfType<Field>();
            
            int beepersCount = Random.Range(4, 7);
            var set = new HashSet<Vector2Int>();
            var beepers = new List<Beeper>(beepersCount);

            for (int i = 0; i < beepersCount; ++i)
            {
                bool emptyCellUsed = false;
                while (!emptyCellUsed)
                {
                    var randomizedCoordinates = new Vector2Int(Random.Range(1, field.Size.x - 1), Random.Range(1, field.Size.y - 6));
                    if (set.Add(randomizedCoordinates))
                    {
                        var cell = field.GetCell(randomizedCoordinates);
                        var beeper = Instantiate(MainSettings.Instance.MainBeeperPrefab, cell.transform);
                        cell.IsBeeperPresentedSync = true;
                        beeper.SetColor(Random.Range(0, 2) == 1 ? Color.red : Color.blue);
                        beeper.Position = randomizedCoordinates;
                        beepers.Add(beeper);
                        emptyCellUsed = true;
                    }
                }
            }

            _randomizedBeepers = beepers;
        }

        private void RandomizeRobotsRotations()
        {
            var directions = Enum.GetValues(typeof(Direction))
                .OfType<Direction>()
                .ToList()
                .FindAll(v => v != Direction.Undefined);

            foreach (var robot in Robots)
            {
                robot.SetRotationImmediately(directions[Random.Range(0, directions.Count)]);
            }
        }
    }
}
