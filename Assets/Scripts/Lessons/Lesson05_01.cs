using System.Collections.Generic;
using KarelTheRobotUnity.Core;
using UnityEngine;

namespace TestNamespace
{
    public class Lesson05_01 : Lesson05_01_Internal
    {
        protected override void Execute()
        {
            // В данном задании необходимо развезти биперы определенных цветов по своим углам и сложить их рядом друг с
            // другом (по любому принципу, по оси X, по оси Y, просто рядом - не важно). Везти бипер определенного цвета
            // необходимо с помощью робота того же цвета (переменные даны в начале)
            // 
            // Общий подход к решению задачи - создать класс-обертку для робота, в котором вы реализуете
            // метод DeliverBeeperToTargetLocation(Vector2Int beeperCoordinates, Vector2Int targetCoordinates).
            // В реализацию этого метода следует декомпозировать до дерева простых и понятных шагов, чтобы код
            // оставался читаемым и у вас была возможность быстро вносить изменения.
            //
            // Начальный поворот роботов и положения биперов рандомные
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
            // 

            List<Beeper> beepers = GetBeepers();
            var blueRobot = Robots[0];
            var redRobot = Robots[1];

            foreach (var beeper in beepers)
            {
                if (beeper.GetColor() == Color.red)
                {
                    // Код красного робота
                }
                else if (beeper.GetColor() == Color.blue)
                {
                    // Код синего робота
                }
                else
                {
                    throw new System.Exception($"Unknown beeper color = {beeper.GetColor()}");
                }
            }
        }
    }
}
