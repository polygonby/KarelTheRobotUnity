using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    public class Robot : MonoBehaviour
    {
        public Transform Arms { get { return _arms; } }
        public float ArmsTravellingDistance { get { return _armsTravellingDistance; } }
        public float ArmsMovingSpeed { get { return _armsMovingSpeed; } }
        
        [SerializeField]
        private Transform _arms = null;
        [SerializeField]
        private float _armsTravellingDistance = 0.2f;
        [SerializeField]
        private float _armsMovingSpeed = 2.0f;
        
        public float MoveSpeed { get; set; } = 1.0f; // In units per second
        public float RotationSpeed { get; set; } = 90.0f; // In degrees per second
    }
}