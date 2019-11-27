using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarelTheRobotUnity.Core
{
    public abstract class Lesson04_01_Internal : SyncActionSceneController
    {
        private List<Vector2Int> _randomizedBeeperCoordinates;
        
        public List<Vector2Int> GetBeepersCoordinates()
        {
            return _randomizedBeeperCoordinates;
        }
        
        protected abstract void Execute();

        protected override void Program()
        {
            RandomizeBeepers();
            RandomizeRobotRotation();
            Execute();
        }

        private void RandomizeBeepers()
        {
            var field = FindObjectOfType<Field>();
            
            var set = new HashSet<Vector2Int>();

            for (int i = 0; i < 3; ++i)
            {
                bool emptyCellUsed = false;
                while (!emptyCellUsed)
                {
                    var randomizedCoordinates = new Vector2Int(Random.Range(1, field.Size.x - 1), Random.Range(4, field.Size.y));
                    if (set.Add(randomizedCoordinates))
                    {
                        var cell = field.GetCell(randomizedCoordinates);
                        var beeper = Instantiate(MainSettings.Instance.MainBeeperPrefab, cell.transform);
                        cell.IsBeeperPresentedSync = true;
                        emptyCellUsed = true;
                    }
                }
            }

            _randomizedBeeperCoordinates = set.ToList();
        }

        private void RandomizeRobotRotation()
        {
            var directions = Enum.GetValues(typeof(Direction))
                .OfType<Direction>()
                .ToList()
                .FindAll(v => v != Direction.Undefined);
            var rotation = directions[Random.Range(0, directions.Count)];
            MainRobot.SetRotationImmediately(rotation);
        }
    }
}
