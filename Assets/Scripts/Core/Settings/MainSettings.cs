using UnityEngine;

namespace KarelTheRobotUnity.Core
{
    [CreateAssetMenu(menuName = "Karel the Robot/MainSettings")]
    public class MainSettings : SingletonScriptableObject<MainSettings>
    {
        [Header("General settings")] 
        [SerializeField]
        public Vector2 MinMaxTimeScale = new Vector2(1.0f, 5.0f);
        [SerializeField]
        public float StartTimeScale = 1.0f;
        [SerializeField]
        public float GridCellSize = 1.0f;
        
        [Header("Robot settings")]
        [SerializeField, Tooltip("In units per second")]
        public float RobotMoveSpeed = 1.0f;
        [SerializeField, Tooltip("In degrees per second")]
        public float RobotRotationSpeed = 90.0f;
        
        [Header("Content settings")]
        [SerializeField]
        public Cell MainCellPrefab;
        [SerializeField]
        public Wall MainWallPrefab;
        [SerializeField]
        public Beeper MainBeeperPrefab;
        [SerializeField]
        public Material TargetCellMaterial;
    }
}
