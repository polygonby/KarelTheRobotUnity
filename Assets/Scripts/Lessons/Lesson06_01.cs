using System;
using System.Collections.Generic;
using KarelTheRobotUnity.Core;
using UnityEngine;
using BeeperType = KarelTheRobotUnity.Core.Lesson06_01_Internal.BeeperType;

namespace TestNamespace
{
    public class Lesson06_01 : Lesson06_01_Internal
    {
        protected override void Execute()
        {
            // В данном задании необходимо в зависимости от нажатой кнопки развезти либо красные биперы, либо синие.
            // Синие биперы необходимо складывать по оси Y друг за другом начиная с верхнего левого угла, красные - из правого верхнего угла по диагонали.
            // 
            // Алгоритмы развоза должен быть реализованы в двух разных классах, BlueBeepersDeliveryStrategy и RedBeepersDeliveryStrategy, которые должны передаваться в вызывающий их
            // класс RobotStrategyExecutor, наследник SmartRobot, написанного на прошлом занятии.
            // Эти классы должны наследоваться от абстрактного класса RobotDeliveryStrategy и переопределять виртуальный метод Execute(RobotStrategyExecutor robot).
            //
            // Начальный поворот робота и положения биперов рандомные
            //
            // Необходимый код:
            // проверка бипера: if (beeper.GetColor() == Color.red) или if (beeper.GetColor() == Color.blue)
            // 

            List<Beeper> beepers = GetBeepers();
            BeeperType beepersTypeToDeliver = GetDeliveredBeepersType();
            var mainRobot = MainRobot;

            switch (beepersTypeToDeliver)
            {
                case BeeperType.Red:
                    Debug.Log("Delivering red beepers");
                    break;
                case BeeperType.Blue:
                    Debug.Log("Delivering blue beepers");
                    break;
                default:
                    throw new System.Exception($"Unknown beeper type = {beepersTypeToDeliver}");
            }
        }
    }

    public class SmartRobot
    {
        private RobotSyncActionWrapper _robot;

        public SmartRobot(RobotSyncActionWrapper robot)
        {
            _robot = robot;
        }

        public void DeliverBeeperToTargetLocation(Beeper beeper, Vector2Int targetLocation)
        {
            // 1) Доехать до бипера
            TravelToLocation(beeper.Position);

            // 2) Поднять бипер
            _robot.TakeBeeper();

            // 3) Доехать до цели
            TravelToLocation(targetLocation);

            // 4) Положить бипер
            _robot.PutBeeper();
        }

        public void TravelToLocation(Vector2Int targetLocation)
        {
            var difference = targetLocation - _robot.Position;

            var xDirection = difference.x > 0 ? Direction.East : Direction.West;
            TurnToDirection(xDirection);
            MoveSteps(Math.Abs(difference.x));

            var yDirection = difference.y > 0 ? Direction.North : Direction.South;
            TurnToDirection(yDirection);
            MoveSteps(Math.Abs(difference.y));
        }

        public void TurnToDirection(Direction direction)
        {
            while (_robot.Rotation != direction)
            {
                _robot.TurnRight();
            }
        }

        private void MoveSteps(int steps)
        {
            for (int i = 0; i < steps; ++i)
            {
                _robot.Move();
            }
        }
    }

    
}
