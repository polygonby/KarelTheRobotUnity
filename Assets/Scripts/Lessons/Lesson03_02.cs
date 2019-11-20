using KarelTheRobotUnity.Core;
using UnityEngine;

namespace TestNamespace
{
    public class Lesson03_02 : Lesson03_02_Internal
    {
        protected override void Execute()
        {
            // В данном задании необходимо написать алгоритм, который доведет робота до целевой точки
            // Следует использовать функции и циклы
            //
            // Начните с декомпозиции задачи. Пример:
            // 1) Чтобы доехать до цели, нужно уравнять координаты по x, потом по y (или наоборот)
            // 2) Чтобы уравнять координаты по какой-то оси, нужно повернуться в нужном направлении (например East если разница в координатах положительная) и проехать нужное количество клеток
            // и т.п.
            //
            // Необходимые команды:
            //
            // Move();
            // TurnRight();
            // TurnLeft();
            // GetCurrentRotation(); который возвращает enum Direction
            // 

            Vector2Int targetCellCoordinates = GetTargetCellCoordinates();
            Vector2Int robotCoordinates = MainRobot.Position;


        }
    }
}
