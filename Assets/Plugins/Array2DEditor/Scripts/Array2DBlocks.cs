using UnityEngine;
using System.Collections.Generic;

namespace Array2DEditor
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Level", menuName = "MindBlocker/Create Level")]
    public class Array2DBlocks : Array2D<BlockData>
    {
        [SerializeField]
        CellRowBlocks[] cells = new CellRowBlocks[Consts.defaultGridSize];

        protected override CellRow<BlockData> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowBlocks : CellRow<BlockData> { }

    public enum AxisLocked { NONE, X, Z, BOTH };

    [System.Serializable]
    public class BlockData
    {
        public BlockData(int _index)
        {
            pairIndex = _index;
            axisLocked = AxisLocked.NONE;
            isBreakable = false;
            isFinal = false;
            isPlayer = false;
            isEmpty = false;
        }

        [Tooltip("Pon el mismo índice a todos los bloques que sean pareja, ejemplo: todos los bloques con índice 1 forman un grupo, si no tiene parejas el bloque déjalo en 0")]
        public int pairIndex;
        [Tooltip("Elige los ejes que están bloqueados")]
        public AxisLocked axisLocked;
        [Tooltip("Activa esto si el bloque es rompible")]
        public bool isBreakable;
        //[Tooltip("Activa esto si el bloque tiene parejas")]
        //public bool isPaired;
        [Tooltip("Activa esto si no hay bloque en esta posición")]
        public bool isEmpty;
        [Tooltip("Activa esto para que el jugador aparezca en esta posición")]
        public bool isPlayer;
        [Tooltip("Activa esto si es el final del nivel")]
        public bool isFinal;
        //[Tooltip("Pon los índices de los bloques que son su pareja separados por una ,    ejemplo  0,1,2,3  ")]
        //public string pairedBlocks;

        /*public int[] PairedBlocks()
        {
            if (pairedBlocks == "") return null;
            List<int> p = new List<int>();
            string[] pairs = pairedBlocks.Split(',');
            foreach (string c in pairs)
            {
                p.Add(int.Parse(c));
            }
            return p.ToArray();
        }*/

    }
}