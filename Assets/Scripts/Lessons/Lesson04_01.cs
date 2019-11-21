using System.Collections.Generic;
using KarelTheRobotUnity.Core;
using UnityEngine;

namespace TestNamespace
{
    public class Lesson04_01 : Lesson04_01_Internal
    {
        protected override void Execute()
        {
            // В данном задании необходимо написать алгоритм, который возьмет ближайший бипер
            // и доставит его до целевой точки
            //
            // Начальный поворот робота и положения биперов рандомные
            //
            // Необходимые команды:
            //
            // Move();
            // TurnRight();
            // TurnLeft();
            // TakeBeeper();
            // PutBeeper();
            // GetCurrentRotation(); возвращает enum Direction
            // 
            // Чтобы вывести в консоль расстояние до бипера: Debug.Log($"Distance to beeper: {distance}");
            // 
            
            List<Vector2Int> beepersCoordinates = GetBeepersCoordinates();
            Vector2Int robotCoordinates = MainRobot.Position;

            
        }

        float GetDistanceToTarget(Vector2Int target, Vector2Int robot)
        {
            return (target - robot).magnitude;
        }
    }
}
