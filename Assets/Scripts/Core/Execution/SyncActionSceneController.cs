using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KarelTheRobotUnity.Core
{
    public abstract class SyncActionSceneController : MonoBehaviour
    {
        [Serializable]
        public class RobotInfo
        {
            [SerializeField]
            public Robot Robot = null;
            [SerializeField]
            public Vector2Int StartPosition = Vector2Int.zero;
            [SerializeField]
            public Direction StartRotation = Direction.Undefined;
        }
        
        public RobotSyncActionWrapper MainRobot { get { return Robots[0]; } }
        public readonly List<RobotSyncActionWrapper> Robots = new List<RobotSyncActionWrapper>();
        
        public Field Field { get { return _field; } }

        // SerializeFields are set via reflection in SyncActionSceneControllerEditor
        // don't forget to fix the code if you change those
        #pragma warning disable 0414
        [SerializeField]
        private MainSettings _mainSettings = null;
        #pragma warning restore 0414
        [SerializeField]
        private AsyncExecutionController _executionController = null;
        [SerializeField]
        private List<RobotInfo> _robots = null;
        [SerializeField]
        private Field _field = null;

        private void Awake()
        {
            var settings = MainSettings.Instance;

            Time.timeScale = settings.StartTimeScale;
            
            foreach (var robotInfo in _robots)
            {
                var robot = robotInfo.Robot;
                robot.MoveSpeed = settings.RobotMoveSpeed;
                robot.RotationSpeed = settings.RobotRotationSpeed;

                var robotSyncWrapper = new RobotSyncActionWrapper(robot, _executionController, _field,
                    robotInfo.StartPosition, robotInfo.StartRotation);
                robotSyncWrapper.AsyncRobot.transform.position = _field.GetCell(
                    robotSyncWrapper.Position.x, robotSyncWrapper.Position.y).transform.position;
                robotSyncWrapper.AsyncRobot.transform.rotation = robotSyncWrapper.Rotation.ToRotationWorldSpace();
                Robots.Add(robotSyncWrapper);
            }
        }

        private void Start()
        {
            Program();
            _executionController.Execute();
        }

        protected abstract void Program();

        public void Move()
        {
            MainRobot.Move();
        }

        public void TurnLeft()
        {
            MainRobot.TurnLeft();
        }

        public void TurnRight()
        {
            MainRobot.TurnRight();
        }

        public void TakeBeeper()
        {
            MainRobot.TakeBeeper();
        }

        public void PutBeeper()
        {
            MainRobot.PutBeeper();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SyncActionSceneController), true)]
    public class SyncActionSceneControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Resolve dependencies"))
            {
                ResolveControllerDependencies();
            }
        }

        private void ResolveControllerDependencies()
        {
            var controller = (SyncActionSceneController)target;

            // MainSettings
            var mainSettingsGuids = AssetDatabase.FindAssets("t:MainSettings");
            if (mainSettingsGuids.Length != 0)
            {
                var settingsPath = AssetDatabase.GUIDToAssetPath(mainSettingsGuids[0]);
                var mainSettings = AssetDatabase.LoadAssetAtPath<MainSettings>(settingsPath);
                ReflectionUtils.FindAndSetPrivateField(typeof(SyncActionSceneController), "_mainSettings", controller, mainSettings);
            }
            else
            {
                Debug.Log("ResolveControllerDependencies: MainSettings has not been found in resources folder");
            }
            
            // Scene objects
            var executionController = FindObjectOfType<AsyncExecutionController>();
            if (executionController == null) 
                Debug.Log("ResolveControllerDependencies: AsyncExecutionController is not presented on current scene");
            ReflectionUtils.FindAndSetPrivateField(typeof(SyncActionSceneController), "_executionController", controller, executionController);
            
            var field = FindObjectOfType<Field>();
            if (field == null) 
                Debug.Log("ResolveControllerDependencies: Field is not presented on current scene");
            ReflectionUtils.FindAndSetPrivateField(typeof(SyncActionSceneController), "_field", controller, field);
            
            var robots = FindObjectsOfType<Robot>().ToList();
            var robotInfos = new List<SyncActionSceneController.RobotInfo>();
            foreach (var robot in robots)
            {
                robotInfos.Add(new SyncActionSceneController.RobotInfo() { Robot = robot });
            }

            if (robots.Count == 0)
            {
                Debug.Log("ResolveControllerDependencies: No robots presented on current scene");
                return;
            }
            
            ReflectionUtils.FindAndSetPrivateField(typeof(SyncActionSceneController), "_robots", controller, robotInfos);
        }
    }
#endif    
}

