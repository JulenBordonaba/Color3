using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    [CustomEditor(typeof(Array2DBlocks))]
    public class Array2DBlocksEditor : Array2DEnumEditor<BlockData>
    {
        // If your enum has long names, you can replace 70 by 150, for example.
        protected override int CellWidth { get { return 0; } }
        // For enums, the cell height will just change the vertical spacing. 
        protected override int CellHeight { get { return 0; } }

        /*protected override void SetValue(SerializedProperty cell, int i, int j)
        {
            BlockData[,] previousCells = (target as Array2D<BlockData>).GetCells();
            for(int ii=0;ii<previousCells.GetLength(0);ii++)
            {
                for (int jj = 0; jj < previousCells.GetLength(1); jj++)
                {
                    Debug.Log(previousCells[ii,jj].index);
                }
            }
            

            //previousCells[i, j].index = int.Parse(i.ToString()+j.ToString());
        }*/
        

    }
}