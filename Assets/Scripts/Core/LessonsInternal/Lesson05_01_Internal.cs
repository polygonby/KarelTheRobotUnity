using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarelTheRobotUnity.Core
{
    public abstract class Lesson05_01_Internal : SyncActionSceneController
    {
        private List<Beeper> _randomizedBeepers;
        
        public List<Beeper> GetBeepers()
        {
            return _randomizedBeepers;
        }
        
        protected abstract void Execute();

        protected override void Program()
        {
            RandomizeBeepers();
            RandomizeRobotsRotations();
            Execute();
        }

        private void RandomizeBeepers()
        {
            var field = FindObjectOfType<Field>();
            
            int beepersCount = Random.Range(2, 7);
            var set = new HashSet<Vector2Int>();
            var beepers = new List<Beeper>(beepersCount);

            for (int i = 0; i < beepersCount; ++i)
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
