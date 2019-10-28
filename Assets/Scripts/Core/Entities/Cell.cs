using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KarelTheRobotUnity.Core
{
    public class Cell : MonoBehaviour
    {
        private Beeper _beeper;
        private Wall _wall;

        private void Awake()
        {
            _beeper = GetComponentInChildren<Beeper>();
            _wall = GetComponentInChildren<Wall>();
        }

        public Beeper GetBeeper()
        {
            return _beeper;
        }
        
        public bool IsBeeperPresented()
        {
            return GetBeeper() != null;
        }

        public bool IsBeeperNotPresented()
        {
            return !IsBeeperPresented();
        }

        public bool IsClear()
        {
            return _wall;
        }

        public bool IsNotClear()
        {
            return !IsClear();
        }
    }
    
#if UNITY_EDITOR    
    [InitializeOnLoad]
    public class SceneGuiGenericCellMenu : Editor
    {
        private static float s_lastMouseDownEventTime;
        
        static SceneGuiGenericCellMenu() 
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
 
        private static void OnSceneGUI(SceneView sceneView)
        {
            if (Event.current.button == 1 && Event.current.type == EventType.MouseDown)
            {
                s_lastMouseDownEventTime = Time.realtimeSinceStartup;
                return;
            }
            
            if (Event.current.button == 1 && Event.current.type == EventType.MouseUp 
                                          && Time.realtimeSinceStartup - s_lastMouseDownEventTime < 0.5f)
            {
                // Ray from editor camera.
                var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                var materialIndex = 0;
                var selectedObject = HandleUtility.PickGameObject(Event.current.mousePosition, out materialIndex);
                if (selectedObject != null)
                {
                    var cell = selectedObject.GetComponentInParent<Cell>();
                    if (cell != null)
                    {
                        GenericMenu menu = new GenericMenu();
                        menu.AddItem(new GUIContent("Create wall"), false, CreateWall, cell);
                        menu.AddItem(new GUIContent("Create beeper"), false, CreateBeeper, cell);
                        menu.AddItem(new GUIContent("Set starting position/Facing North"), 
                            false, MarkCellAsStartingPosition, System.Tuple.Create(cell, Direction.North));
                        menu.AddItem(new GUIContent("Set starting position/Facing East"), 
                            false, MarkCellAsStartingPosition, System.Tuple.Create(cell, Direction.East));
                        menu.AddItem(new GUIContent("Set starting position/Facing South"), 
                            false, MarkCellAsStartingPosition, System.Tuple.Create(cell, Direction.South));
                        menu.AddItem(new GUIContent("Set starting position/Facing West"), 
                            false, MarkCellAsStartingPosition, System.Tuple.Create(cell, Direction.West));
                        menu.ShowAsContext();
                        
                        Event.current.Use();
                    }
                }
            }
        }
 
        private static void CreateWall(object userData)
        {
            var cell = (Cell)userData;
            var wall = Instantiate(MainSettings.Instance.MainWallPrefab, cell.transform);
            wall.name = wall.name.Replace("(Clone)", "");
        }
        
        private static void CreateBeeper(object userData)
        {
            var cell = (Cell)userData;
            var wall = Instantiate(MainSettings.Instance.MainBeeperPrefab, cell.transform);
            wall.name = wall.name.Replace("(Clone)", "");
        }

        private static void MarkCellAsStartingPosition(object userData)
        {
            var robot = GetSelectedRobot();
            if (robot == null)
            {
                Debug.Log("There is no robot on scene to assign starting position");
                return;
            }
            
            var tuple = (System.Tuple<Cell, Direction>)userData;
            var cell = tuple.Item1;
            var direction = tuple.Item2;
            
            var field = cell.GetComponentInParent<Field>();
            var controller = FindObjectOfType<SyncActionSceneController>();
            var cellCoordinates = field.GetCellCoordinates(cell);
            
            var robotInfos = ReflectionUtils.GetValueFromPrivateField<List<SyncActionSceneController.RobotInfo>>(
                typeof(SyncActionSceneController), "_robots", controller);

            if (robotInfos == null)
            {
                robotInfos = new List<SyncActionSceneController.RobotInfo>();
                ReflectionUtils.FindAndSetPrivateField(typeof(SyncActionSceneController), "_robots", 
                    controller, robotInfos);
            }
            
            if (!robotInfos.Exists(i => i.Robot == robot))
                robotInfos.Add(new SyncActionSceneController.RobotInfo() { Robot = robot });

            var robotInfo = robotInfos.Find(i => i.Robot == robot);
            robotInfo.StartPosition = cellCoordinates;
            robotInfo.StartRotation = direction;
            
            robot.transform.position = cell.transform.position;
            robot.transform.rotation = direction.ToRotationWorldSpace();
        }

        private static Robot GetSelectedRobot()
        {
            Robot robot = null;
            
            var selectedObject = Selection.activeGameObject;
            if (selectedObject != null)
                robot = selectedObject.GetComponentInParent<Robot>();

            // If no robot is selected - selecting random one on scene
            if (robot == null)
                robot = FindObjectOfType<Robot>();

            return robot;
        }
    }
#endif    
}