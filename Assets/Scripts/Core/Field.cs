using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector2Int Size { get { return new Vector2Int(Cells.Count, Cells[0].Count);} }
    public List<List<Cell>> Cells { get; private set; } = new List<List<Cell>>() { new List<Cell>() };

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= Cells.Count) throw new ArgumentOutOfRangeException("x is out of range");
        if (y < 0 || y >= Cells[x].Count) throw new ArgumentOutOfRangeException("y is out of range");

        return Cells[x][y];
    }
}
