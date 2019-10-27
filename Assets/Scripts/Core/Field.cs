using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KarelTheRobotUnity.Core
{
    public class Field : MonoBehaviour
    {
        public Vector2Int Size => _size;

        private List<Cell> _cells;
        
        [SerializeField]
        private Vector2Int _size = new Vector2Int(8, 8);
        
        public Cell GetCell(int x, int y)
        {
            if (x < 0 || x >= Size.x) throw new ArgumentOutOfRangeException("x is out of range");
            if (y < 0 || y >= Size.y) throw new ArgumentOutOfRangeException("y is out of range");

            if (_cells == null) PopulateCellsList();
            
            return _cells[x * Size.y + y];
        }

        private void PopulateCellsList()
        {
            _cells = GetComponentsInChildren<Cell>().ToList();
            
            if (_cells.Count != Size.x * Size.y) throw new Exception("Wrong number of cells in field");
            
            _cells.Sort((x, y) =>
            {
                int compareXResult = x.transform.position.x.CompareTo(y.transform.position.x);
                return compareXResult == 0 ? x.transform.position.y.CompareTo(y.transform.position.y) : compareXResult;
            });
        }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(Field), true)]
    public class FieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var field = (Field)target;

            if (GUILayout.Button("Recreate field"))
            {
                var existingCells = field.GetComponentsInChildren<Cell>();
                foreach (var existingCell in existingCells)
                {
                    DestroyImmediate(existingCell.gameObject);
                }

                float gridCellSize = MainSettings.Instance.GridCellSize;
                float xOffset = -gridCellSize * field.Size.x / 2.0f + gridCellSize / 2.0f;
                float yOffset = -gridCellSize * field.Size.y / 2.0f + gridCellSize / 2.0f;
                
                for (int i = 0; i < field.Size.x; i++)
                {
                    for (int j = 0; j < field.Size.y; j++)
                    {
                        float positionX = xOffset + gridCellSize * i;
                        float positionY = yOffset + gridCellSize * j;
                        
                        var cell = Instantiate(MainSettings.Instance.MainCellPrefab, field.transform);
                        cell.name = cell.name.Replace("(Clone)", "");
                        cell.transform.position = new Vector3(positionX, 0.0f, positionY);
                    }    
                }
            }
        }
    }
#endif    
}