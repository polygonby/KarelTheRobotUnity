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
        public RobotSyncActionWrapper MainRobot { get { return Robots[0]; } }
        public readonly List<RobotSyncActionWrapper> Robots = new List<RobotSyncActionWrapper>();
        
        public Field Field { get { return _field; } } 

        // SerializeFields are set via reflection in SyncActionSceneControllerEditor
        // don't forget to fix the code if you change those
        [SerializeField]
        private AsyncExecutionController _executionController = null;
        [SerializeField]
        private List<Robot> _robots = null;
        [SerializeField]
        private Field _field = null;

        private void Awake()
        {
            var settings = MainSettings.Instance;

            Time.timeScale = settings.StartTimeScale;
            
            foreach (var robot in _robots)
            {
                robot.MoveSpeed = settings.RobotMoveSpeed;
                robot.RotationSpeed = settings.RobotRotationSpeed;
                
                Robots.Add(new RobotSyncActionWrapper(robot, _executionController));
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

            var baseType = ReflectionUtils.GetTypeFromInheritanceHierarchy(controller.GetType(),
                typeof(SyncActionSceneController));

            var executionController = FindObjectOfType<AsyncExecutionController>();
            if (executionController == null) 
                Debug.Log("ResolveControllerDependencies: AsyncExecutionController is not presented on current scene");
            ReflectionUtils.FindAndSetPrivateField(baseType, "_executionController", controller, executionController);
            
            var field = FindObjectOfType<Field>();
            if (field == null) 
                Debug.Log("ResolveControllerDependencies: Field is not presented on current scene");
            ReflectionUtils.FindAndSetPrivateField(baseType, "_field", controller, field);
            
            var robots = FindObjectsOfType<Robot>().ToList();
            if (robots.Count == 0) 
                Debug.Log("ResolveControllerDependencies: No robots presented on current scene");
            ReflectionUtils.FindAndSetPrivateField(baseType, "_robots", controller, robots);
        }
    }
#endif    
}

