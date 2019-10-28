using System;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public enum Direction
    {
        Undefined = 0,
        North = 1,
        East = 2,
        South = 3,
        West = 4
    }

    public static class DirectionExtensions
    {
        public static Quaternion ToRotationWorldSpace(this Direction _this)
        {
            switch (_this)
            {
                case Direction.North:
                    return Quaternion.identity;
                case Direction.East:
                    return Quaternion.Euler(0.0f, 90.0f, 0.0f);
                case Direction.South:
                    return Quaternion.Euler(0.0f, 180.0f, 0.0f);
                case Direction.West:
                    return Quaternion.Euler(0.0f, 270.0f, 0.0f);
                default:
                    throw new ArgumentException("Cannot get rotation of Direction = " + _this);
            }
        }

        public static Direction GetRight(this Direction _this)
        {
            if (_this == Direction.Undefined)
                throw new ArgumentException($"Cannot get right from {_this} direction");

            int newDirection = (int)_this + 1;
            
            if (newDirection >= Enum.GetValues(typeof(Direction)).Length)
                newDirection = 1;
            
            return (Direction)newDirection;
        }
        
        public static Direction GetLeft(this Direction _this)
        {
            if (_this == Direction.Undefined)
                throw new ArgumentException($"Cannot get right from {_this} direction");

            int newDirection = (int)_this - 1;
            
            if (newDirection <= 0)
                newDirection = Enum.GetValues(typeof(Direction)).Length - 1;
            
            return (Direction)newDirection;
        }
        
        public static Direction GetBack(this Direction _this)
        {
            if (_this == Direction.Undefined)
                throw new ArgumentException($"Cannot get right from {_this} direction");

            int newDirection = (int)_this + 2;
            
            if (newDirection == Enum.GetValues(typeof(Direction)).Length)
                newDirection = 1;
            else if (newDirection > Enum.GetValues(typeof(Direction)).Length)
                newDirection = 2;
            
            return (Direction)newDirection;
        }
    }
}