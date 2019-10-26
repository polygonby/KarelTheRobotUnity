using System.Collections;
using System.Collections.Generic;
using KarelTheRobotUnity.Core;
using UnityEngine;

public class Lesson01 : MonoBehaviour
{
    private void Start()
    {
        var robot = FindObjectOfType<Robot>();
        var executor = FindObjectOfType<ExecutionController>();
        
        var move = new AsyncRobotMoveAction(robot);
        var rotateLeft = new AsyncRobotTurnAction(robot, AsyncRobotTurnAction.RotationDirection.Left);
        var rotateRight = new AsyncRobotTurnAction(robot, AsyncRobotTurnAction.RotationDirection.Right);
        
        executor.AddAction(move);
        executor.AddAction(rotateRight);
        executor.AddAction(move);
        executor.AddAction(move);
        executor.AddAction(rotateLeft);
        executor.AddAction(move);
        
        executor.Execute();
    }
}
