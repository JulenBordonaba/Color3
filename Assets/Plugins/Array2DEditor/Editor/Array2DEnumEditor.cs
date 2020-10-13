using UnityEditor;
using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    public class Array2DEnumEditor<T> : Array2DEditor
    {
        protected override int CellWidth { get { return 100; } }
        protected override int CellHeight { get { return 16; } }

        protected override void SetValue(SerializedProperty cell, int i, int j)
        {
            T[,] previousCells = (target as Array2D<T>).GetCells();      

            int width = previousCells.GetLength(1);

            //cell.enumValueIndex = 0;

            if (i < gridSize.vector2IntValue.x && j < gridSize.vector2IntValue.y)
            {
                //cell.enumValueIndex = previousCells.Cast<int>().ToArray()[i * width + j];
            }

            if (typeof(T).Equals(typeof(BlockData)))
            {
                Debug.Log("entra a tipo " + previousCells.GetLength(0) + " " + previousCells.GetLength(1));
                //BlockData bd = previousCells[j, i] as BlockData;
                //bd.index = int.Parse(i.ToString() + j.ToString());
            }
        }
    }
}