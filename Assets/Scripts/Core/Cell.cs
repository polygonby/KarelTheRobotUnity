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
    }
#endif    
}