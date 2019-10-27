using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class Robot : MonoBehaviour
    {
        public float MoveSpeed { get; set; } = 1.0f; // In units per second
        public float RotationSpeed { get; set; } = 90.0f; // In degrees per second
    }
}