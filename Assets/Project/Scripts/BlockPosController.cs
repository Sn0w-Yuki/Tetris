using System;
using UnityEngine;

public class BlockPosController : MonoBehaviour
{
    public GameObject[] Cube;
    private int width, height, depth;

    public int[,,] blPos;
    private GameObject[] nowBlock;
    private float Timer = 0f;
    private TetrisManager tetrisManager;

    public enum BlockType { T, J, L, Z, S, I, O }

    public class BlockShape
    {
        public Vector3Int[][] Offsets; // [rotation][tileIndex]
    }

    public BlockShape[] blockShapes;

    private BlockType currentBlockType;
    private int currentRoll;
    private Vector3 spawnPos;
    private int currentX, currentY, currentZ;
    private Vector3 boardOffset;

    private void Start()
    {
        tetrisManager = GetComponent<TetrisManager>();

        width = tetrisManager.fieldWidth;
        height = tetrisManager.fieldHeight;
        depth = tetrisManager.fieldDepth;

        blPos = new int[height, width, depth];
        boardOffset = new Vector3(-width / 2f, 0, -depth / 2f);
        spawnPos = new Vector3(width / 2, height - 1, depth / 2);

        InitBlockShapes();
        SpawnNewBlock();
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 1f)
        {
            DropBlock();
            Timer = 0f;
        }
    }

    void InitBlockShapes()
    {
        blockShapes = new BlockShape[7];

        blockShapes[(int)BlockType.T] = new BlockShape
        {
            Offsets = new Vector3Int[][]
            {
                new Vector3Int[] { new(0, 0, 0), new(1, 0, 0), new(2, 0, 0), new(1, -1, 0) },
                new Vector3Int[] { new(0, 0, 0), new(0, -1, 0), new(0, -2, 0), new(1, -1, 0) },
                new Vector3Int[] { new(0, 0, 0), new(-1, 0, 0), new(-2, 0, 0), new(-1, -1, 0) },
                new Vector3Int[] { new(0, 0, 0), new(0, 1, 0), new(0, 2, 0), new(-1, 1, 0) }
            }
        };

        // 他ブロックも同様に定義（必要に応じてZ軸方向の形も追加可）

        blockShapes[(int)BlockType.O] = new BlockShape
        {
            Offsets = new Vector3Int[][]
            {
                new Vector3Int[] { new(0, 0, 0), new(1, 0, 0), new(0, -1, 0), new(1, -1, 0) }
            }
        };

        // ※他のブロック（J, L, Z, S, I）も同様に Vector3Int で定義してください
    }

    public bool CanPlaceBlock(BlockType block, int roll, int x, int y, int z)
    {
        foreach (var offset in blockShapes[(int)block].Offsets[roll])
        {
            int nx = x + offset.x;
            int ny = y + offset.y;
            int nz = z + offset.z;

            if (!InBounds(nx, ny, nz)) return false;
            if (ny >= 0 && blPos[ny, nx, nz] != 0) return false;
        }
        return true;
    }

    public void PlaceBlock(BlockType block, int roll, int x, int y, int z)
    {
        ClearCurrentBlock();
        nowBlock = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            Vector3Int offset = blockShapes[(int)block].Offsets[roll][i];
            int nx = x + offset.x;
            int ny = y + offset.y;
            int nz = z + offset.z;

            if (InBounds(nx, ny, nz))
            {
                blPos[ny, nx, nz] = 1;
            }

            nowBlock[i] = Instantiate(Cube[(int)block], new Vector3(nx, ny, nz) + boardOffset, Quaternion.identity);
            nowBlock[i].tag = "currentBlock";
        }
    }

    public void ClearBlock(BlockType block, int roll, int x, int y, int z)
    {
        foreach (var offset in blockShapes[(int)block].Offsets[roll])
        {
            int nx = x + offset.x;
            int ny = y + offset.y;
            int nz = z + offset.z;

            if (InBounds(nx, ny, nz))
            {
                blPos[ny, nx, nz] = 0;
            }
        }
    }

    public void DropBlock()
    {
        ClearBlock(currentBlockType, currentRoll, currentX, currentY, currentZ);

        if (CanPlaceBlock(currentBlockType, currentRoll, currentX, currentY - 1, currentZ))
        {
            currentY -= 1;
            PlaceBlock(currentBlockType, currentRoll, currentX, currentY, currentZ);
        }
        else
        {
            PlaceBlock(currentBlockType, currentRoll, currentX, currentY, currentZ); // 固定
            SpawnNewBlock();
        }
    }

    void SpawnNewBlock()
    {
        ClearCurrentBlock();
        currentBlockType = (BlockType)UnityEngine.Random.Range(0, 7);
        currentRoll = 0;
        currentX = (int)spawnPos.x;
        currentY = (int)spawnPos.y;
        currentZ = (int)spawnPos.z;

        if (!CanPlaceBlock(currentBlockType, currentRoll, currentX, currentY, currentZ))
        {
            Debug.Log("Game Over!");
            enabled = false;
            return;
        }

        PlaceBlock(currentBlockType, currentRoll, currentX, currentY, currentZ);
    }

    public void ClearCurrentBlock()
    {
        GameObject[] oldBlocks = GameObject.FindGameObjectsWithTag("currentBlock");
        foreach (GameObject obj in oldBlocks)
        {
            Destroy(obj);
        }
    }

    bool InBounds(int x, int y, int z)
    {
        return x >= 0 && x < width &&
               y >= 0 && y < height &&
               z >= 0 && z < depth;
    }
}
