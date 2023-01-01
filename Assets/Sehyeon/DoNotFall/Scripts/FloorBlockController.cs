using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBlockController : MonoBehaviour
{
    public static FloorBlockController Instance { get; private set; }

    [System.Serializable]
    public class Floor
    {
        public FloorBlock[] Blocks;
    }

    public Floor[] FloorBlocks;

    private void Start()
    {
        Instance = this;
    }

    public void DeleteFloorBlock(int floorIndex, int blockIndex)
    {
        Debug.Log($"[{floorIndex}, {blockIndex}]");
        FloorBlocks[floorIndex].Blocks[blockIndex].OnDelete();
    }
}
