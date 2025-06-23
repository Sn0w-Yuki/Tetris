using System;
using UnityEngine;

public class BlockPosController : MonoBehaviour
{
    public TetrisManager tetrisManager;
    public GameObject[] Cube;
    private int width = 0;
    private int height = 0;

    public int[,] blPos;
    private GameObject[] nowBlock;
    private float Timer = 0;

    public enum BlockType { T, J, L, Z, S, I, O }

    public class BlockShape
    {
        public Vector2Int[][] Offsets; // [rotation][tileIndex]
    }

    public BlockShape[] blockShapes;

    // 現在のブロック情報
    private BlockType currentBlockType;
    private int currentRoll;
    private Vector3 spawnPos;
    private int currentX;
    private int currentY;
    private Vector3 boardOffset;

    private void Start()
    {
        width = tetrisManager.fieldWidth;
        height = tetrisManager.fieldHeight;
        blPos = new int[height, width];
        boardOffset = new Vector3(-width / 2f, 0, 0); // 中央揃え
        spawnPos = new Vector3(width / 2, height - 1, 0);

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
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] { new(0, 0), new(1, 0), new(2, 0), new(1, -1) },
                new Vector2Int[] { new(0, 0), new(1, 0), new(1, 1), new(2, 0) },
                new Vector2Int[] { new(0, 0), new(-1, 0), new(-2, 0), new(-1, -1) },
                new Vector2Int[] { new(0, 0), new(0, -1), new(-1, -1), new(0, -2) },
            }
        };

        blockShapes[(int)BlockType.J] = new BlockShape
        {
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] {new(0,0), new(1,0),new(2,0),new(2,-1)},
                new Vector2Int[] {new(0,0), new(0,1), new(0,2), new(1,2)},
                new Vector2Int[] {new(0,0), new(-1,0), new(-2,0), new(-2,1)},
                new Vector2Int[] {new(0,0), new(0,-1), new(0,-2), new(-1,-2)},
            }
        };

        blockShapes[(int)BlockType.L] = new BlockShape
        {
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] {new(0,0), new(-1,0),new(-2,0),new(-2,-1)},
                new Vector2Int[] {new(0,0), new(0,-1), new(0,-2), new(1,-2)},
                new Vector2Int[] {new(0,0), new(1,0), new(2,0), new(2,1)},
                new Vector2Int[] {new(0,0), new(0,1), new(0,2), new(-1,2)},
            }
        };

        blockShapes[(int)BlockType.Z] = new BlockShape
        {
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] {new(0,0), new(1,0),new(1,-1),new(2,-1)},
                new Vector2Int[] {new(0,0), new(0,-1), new(-1,-1), new(-1,-2)},
            }
        };

        blockShapes[(int)BlockType.S] = new BlockShape
        {
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] {new(0,0), new(-1,0),new(-1,-1),new(-2,-1)},
                new Vector2Int[] {new(0,0), new(0,-1), new(1,-1), new(1,-2)},
            }
        };

        blockShapes[(int)BlockType.I] = new BlockShape
        {
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] {new(0,0), new(0,-1),new(0,-2),new(0,-3)},
                new Vector2Int[] {new(0,0), new(-1,0), new(-2,0), new(-3,0)},
            }
        };

        blockShapes[(int)BlockType.O] = new BlockShape
        {
            Offsets = new Vector2Int[][]
            {
                new Vector2Int[] {new(0,0), new(1,0),new(0,-1),new(1,-1)},
            }
        };
    }

    public bool CanPlaceBlock(BlockType block, int roll, int x, int y)
    {
        foreach (var offset in blockShapes[(int)block].Offsets[roll])
        {
            int nx = x + offset.x;
            int ny = y + offset.y;

            if (nx < 0 || nx >= width || ny >= height) return false; // xは範囲外禁止、yが下にはみ出し禁止
            if (ny >= 0 && blPos[ny, nx] != 0) return false; // y >= 0 の範囲で重なり判定
        }
        return true;
    }


    public void PlaceBlock(BlockType block, int roll, int x, int y)
    {
        ClearCurrentBlock();
        nowBlock = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            Vector2Int offset = blockShapes[(int)block].Offsets[roll][i];
            int nx = x + offset.x;
            int ny = y + offset.y;
            if (InBounds(nx, ny))
            {
                blPos[ny, nx] = 1;
            }
            nowBlock[i] = Instantiate(Cube[(int)block], new Vector3(nx, ny, 0) + boardOffset, Quaternion.identity);
            nowBlock[i].tag = "currentBlock";
        }
    }

    public void ClearBlock(BlockType block, int roll, int x, int y)
    {
        foreach (var offset in blockShapes[(int)block].Offsets[roll])
        {
            int nx = x + offset.x;
            int ny = y + offset.y;
            if (InBounds(nx, ny))
            {
                blPos[ny, nx] = 0;
            }
        }
    }

    bool InBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public void DropBlock()
    {
        ClearBlock(currentBlockType, currentRoll, currentX, currentY);

        if (CanPlaceBlock(currentBlockType, currentRoll, currentX, currentY - 1))
        {
            currentY -= 1;
            PlaceBlock(currentBlockType, currentRoll, currentX, currentY);
        }
        else
        {
            PlaceBlock(currentBlockType, currentRoll, currentX, currentY); // 固定
            SpawnNewBlock(); // 次のブロック
        }
    }

    void SpawnNewBlock()
    {
        ClearCurrentBlock();
        currentBlockType = (BlockType)UnityEngine.Random.Range(0, 7);
        currentRoll = 0;
        currentX = (int)spawnPos.x;
        currentY = (int)spawnPos.y;
        Debug.Log(currentX + "," + currentY);

        if (!CanPlaceBlock(currentBlockType, currentRoll, currentX, currentY))
        {
            Debug.Log("Game Over!");
            enabled = false;
            return;
        }

        PlaceBlock(currentBlockType, currentRoll, currentX, currentY);
    }

    public void ClearCurrentBlock()
    {
        GameObject[] oldBlocks = GameObject.FindGameObjectsWithTag("currentBlock");
        foreach (GameObject obj in oldBlocks)
        {
            Destroy(obj);
        }
    }
}
