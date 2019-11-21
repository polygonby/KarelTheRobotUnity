using System;
using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class RobotSyncActionWrapper
    {
        public Vector2Int Position { get; private set; }
        public Direction Rotation { get; private set; }
        public bool IsBeeperOnBoard { get; private set; }
        
        public readonly Robot AsyncRobot;
        
        public bool IsErrorOccured { get; private set; }
        
        private readonly AsyncExecutionController _executionController;
        private readonly Field _field;
        
        public RobotSyncActionWrapper(Robot asyncRobot, AsyncExecutionController executionController, Field field,
            Vector2Int startPosition, Direction startRotation)
        {
            AsyncRobot = asyncRobot;
            _executionController = executionController;
            _field = field;
            Position = startPosition;
            Rotation = startRotation;
        }

        public void Move()
        {
            if (IsErrorOccured) return;
            
            var frontCell = GetFrontCell();
            
            if (frontCell != null && frontCell.IsClear())
            {
                Position = _field.GetCellCoordinates(frontCell);
                _executionController.AddAction(new AsyncRobotMoveAction(AsyncRobot));
            }
            else if (frontCell != null)
            {
                IsErrorOccured = true;
                _executionController.AddAction(new AsyncCellBlockedErrorAction(frontCell));
            }
        }

        public void TurnLeft()
        {
            if (IsErrorOccured) return;
            
            Rotation = Rotation.GetLeft();
            _executionController.AddAction(new AsyncRobotTurnAction(AsyncRobot, AsyncRobotTurnAction.RotationDirection.Left));
        }
        
        public void TurnRight()
        {
            if (IsErrorOccured) return;
            
            Rotation = Rotation.GetRight();
            _executionController.AddAction(new AsyncRobotTurnAction(AsyncRobot, AsyncRobotTurnAction.RotationDirection.Right));
        }

        public void TakeBeeper()
        {
            if (IsErrorOccured) return;

            var currentCell = GetCurrentCell();
            var beeper = currentCell.GetBeeper();
            if (beeper != null)
            {
                IsBeeperOnBoard = true;
                _executionController.AddAction(new AsyncRobotTakeBeeperAction(beeper, AsyncRobot));
            }
            else
            {
                IsErrorOccured = true;
                _executionController.AddAction(new AsyncRobotNoBeeperErrorAction(AsyncRobot, currentCell));
            }
        }

        public void PutBeeper()
        {
            if (IsErrorOccured) return;

            var currentCell = GetCurrentCell();
            
            if (IsBeeperOnBoard)
            {
                _executionController.AddAction(new AsyncRobotPutBeeperAction(currentCell, AsyncRobot));
            }
            else
            {
                Debug.Log("NO");
                IsErrorOccured = true;
                _executionController.AddAction(new AsyncRobotNoBeeperErrorAction(AsyncRobot, currentCell));
            }
        }

        public Cell GetCurrentCell()
        {
            return _field.GetCell(Position);
        }

        public Cell GetFrontCell()
        {
            return GetNearCell(Rotation);
        }

        public Cell GetRightCell()
        {
            return GetNearCell(Rotation.GetRight());
        }

        public Cell GetLeftCell()
        {
            return GetNearCell(Rotation.GetLeft());
        }
        
        public Cell GetBackCell()
        {
            return GetNearCell(Rotation.GetBack());
        }
        
        public Cell GetNearCell(Direction direction)
        {
            Vector2Int nearCellCoordinates;
            
            switch (direction)
            {
                case Direction.North:
                    nearCellCoordinates = Position + Vector2Int.up;
                    break;
                case Direction.East:
                    nearCellCoordinates = Position + Vector2Int.right;
                    break;
                case Direction.South:
                    nearCellCoordinates = Position + Vector2Int.down;
                    break;
                case Direction.West:
                    nearCellCoordinates = Position + Vector2Int.left;
                    break;
                default:
                    throw new ArgumentException("Cannot get near cell for direction = " + direction);
            }

            if (nearCellCoordinates.x < 0 || nearCellCoordinates.x >= _field.Size.x ||
                nearCellCoordinates.y < 0 || nearCellCoordinates.y >= _field.Size.y)
            {
                return null;
            }
            
            return _field.GetCell(nearCellCoordinates);
        }

        internal void SetRotationImmediately(Direction direction)
        {
            Rotation = direction;
            AsyncRobot.transform.rotation = Rotation.ToRotationWorldSpace();
        }
    }
}
