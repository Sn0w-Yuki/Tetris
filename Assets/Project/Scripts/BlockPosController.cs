using UnityEngine;

public class BlockPosController : MonoBehaviour
{
    public int nowBlock = 0, nowRoll = 0;
    [SerializeField] private GameObject[] Cube;
    [SerializeField] private TetrisManager tetrisManager;
    private float width, height, depth, floor;
    private float dropTimer = 1f, time = 0f;
    private Vector3Int spawnPos;
    private GameObject[] Block = new GameObject[4];
    private Vector3Int[] spawnOffset;
    private Vector3Int[] dropOffset;
    private int[,,] posArray;
    private int score = 0, level = 0, totalDelete = 0;

    void Start()
    {
        InitialSetting();
        SpawnBlock(nowBlock);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= dropTimer)
        {
            if (DropCheck(nowBlock, nowRoll))
            {
                Drop();
            }
            else
            {
                Lock();
                SpawnBlock(nowBlock);
            }
            time = 0;
        }
    }

    void InitialSetting()
    {
        width = tetrisManager.fieldWidth;
        height = tetrisManager.fieldHeight;
        depth = tetrisManager.fieldDepth;
        floor = tetrisManager.floorHeight;
        spawnPos = new Vector3Int((int)width / 2, (int)height - 1, (int)depth / 2);
        posArray = new int[(int)width, (int)height, (int)depth];

        nowBlock = Random.Range(0, 6);
    }

    void SpawnBlock(int blockNum)
    {
        switch (blockNum)
        {
            case 0:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(0,0,0),
                    new Vector3Int(1,0,0),
                    new Vector3Int(2,0,0),
                    new Vector3Int(1,-1,0),
                };
                break;
            case 1:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(0,0,0),
                    new Vector3Int(1,0,0),
                    new Vector3Int(2,0,0),
                    new Vector3Int(2,-1,0),
                };
                break;
            case 2:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(-2,0,0),
                    new Vector3Int(-1,0,0),
                    new Vector3Int(0,0,0),
                    new Vector3Int(-2,-1,0),
                };
                break;
            case 3:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(0,0,0),
                    new Vector3Int(1,0,0),
                    new Vector3Int(1,-1,0),
                    new Vector3Int(2,-1,0),
                };
                break;
            case 4:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(-1,0,0),
                    new Vector3Int(0,0,0),
                    new Vector3Int(-2,-1,0),
                    new Vector3Int(-1,-1,0),
                };
                break;
            case 5:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(0,0,0),
                    new Vector3Int(0,-1,0),
                    new Vector3Int(0,-2,0),
                    new Vector3Int(0,-3,0),
                };
                break;
            case 6:
                spawnOffset = new Vector3Int[]{
                    new Vector3Int(0,0,0),
                    new Vector3Int(1,0,0),
                    new Vector3Int(0,-1,0),
                    new Vector3Int(1,-1,0),
                };
                break;
        }

        for (int i = 0; i < 4; i++)
        {
            int x = spawnPos.x + spawnOffset[i].x;
            int y = spawnPos.y + spawnOffset[i].y;
            int z = spawnPos.z + spawnOffset[i].z;

            if (spawnOffset[i].x == 0 && spawnOffset[i].y == 0 && spawnOffset[i].z == 0)
            {
                posArray[x, y, z] = 2;
            }
            else
            {
                posArray[x, y, z] = 1;
            }
            Block[i] = Instantiate(Cube[blockNum], new Vector3Int(x, y, z), Quaternion.identity);
        }
    }

    bool DropCheck(int blockNum, int blockRoll)
    {
        switch (blockNum)
        {
            case 0:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(2,-1,0),
                            new Vector3Int(1,-2,0),
                        };
                        break;
                    case 1:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(1,0,0),
                        };
                        break;
                    case 2:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(-2,-1,0),
                            new Vector3Int(-1,-1,0),
                            new Vector3Int(0,-2,0),
                        };
                        break;
                    case 3:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(-1,-2,0),
                            new Vector3Int(0,-3,0),
                        };
                        break;
                }
                break;
            case 1:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(1,-1,0),
                            new Vector3Int(2,-2,0),
                        };
                        break;
                    case 1:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(1,1,0),
                        };
                        break;
                    case 2:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(-2,-1,0),
                            new Vector3Int(-1,-1,0),
                            new Vector3Int(0,-1,0),
                        };
                        break;
                    case 3:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(-1,-3,0),
                            new Vector3Int(0,-3,0),
                        };
                        break;
                }
                break;
            case 2:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(-1,-1,0),
                            new Vector3Int(0,-1,0),
                            new Vector3Int(-2,-2,0),
                        };
                        break;
                    case 1:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-3,0),
                            new Vector3Int(1,-3,0),
                        };
                        break;
                    case 2:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(1,-1,0),
                            new Vector3Int(2,-1,0),
                        };
                        break;
                    case 3:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(-1,1,0),
                        };
                        break;
                }
                break;
            case 3:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(1,-2,0),
                            new Vector3Int(2,-2,0),
                        };
                        break;
                    case 1:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-2,0),
                            new Vector3Int(-1,-3,0),
                        };
                        break;
                }
                break;
            case 4:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-1,0),
                            new Vector3Int(-2,-2,0),
                            new Vector3Int(-1,-2,0),
                        };
                        break;
                    case 1:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-2,0),
                            new Vector3Int(1,-3,0),
                        };
                        break;
                }
                break;
            case 5:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-4,0),
                        };
                        break;
                    case 1:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(-3,-1,0),
                            new Vector3Int(-2,-1,0),
                            new Vector3Int(-1,-1,0),
                            new Vector3Int(0,-1,0),
                        };
                        break;
                }
                break;
            case 6:
                switch (blockRoll)
                {
                    case 0:
                        dropOffset = new Vector3Int[]{
                            new Vector3Int(0,-2,0),
                            new Vector3Int(1,-2,0),
                        };
                        break;
                }
                break;
        }

        int checkCount, clearCount = 0;
        checkCount = dropOffset.Length;
        Vector3Int centerPos;
        centerPos = getCenterPos();

        for (int i = 0; i < checkCount; i++)
        {
            int x = centerPos.x + dropOffset[i].x;
            int y = centerPos.y + dropOffset[i].y;
            int z = centerPos.z + dropOffset[i].z;

            if (checkInBound(x, y, z) && posArray[x, y, z] == 0)
            {
                clearCount++;
            }
        }

        return clearCount == checkCount ? true : false;
    }

    void Drop()
    {
        Debug.Log("Drop");
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (posArray[x, y, z] == 2 || posArray[x, y, z] == 1)
                    {
                        posArray[x, y - 1, z] = posArray[x, y, z];
                        posArray[x, y, z] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            Vector3 vec = Block[i].transform.position;
            vec.y--;
            Block[i].transform.position = vec;
        }
    }

    void Lock()
    {
        Debug.Log("Lock");
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (posArray[x, y, z] == 2 || posArray[x, y, z] == 1)
                    {
                        posArray[x, y, z] = 10 + nowBlock;
                    }
                }
            }
        }


        nowBlock = Random.Range(0, 6);
    }

    void CheckLine()
    {
        int deleteCount = 0;
        for (int y = 0; y < height; y++)
        {
            int count = 0;
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    if (posArray[x, y, z] >= 10)
                    {
                        count++;
                    }
                }
            }

            if (count == width * depth)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        posArray[x, y, z] = posArray[x, y + 1, z];
                    }
                }
                deleteCount++;
                totalDelete++;
            }
        }

        AddScore(deleteCount);

    }

    Vector3Int getCenterPos()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (posArray[x, y, z] == 2)
                    {
                        return new Vector3Int(x, y, z);
                    }
                }
            }
        }
        Debug.Log("No CenterPos");
        return spawnPos;
    }

    bool checkInBound(int x, int y, int z)
    {
        return x >= 0 && x < width &&
               y > floor && y < height &&
               z >= 0 && z < depth;
    }

    void AddScore(int deleteCount)
    {
        score += deleteCount switch
        {
            1 => 50 * level,
            2 => 150 * level,
            3 => 250 * level,
            4 => 400 * level,
            _ => 0
        };

        level = totalDelete / 10;
        Debug.Log("score:" + score + "\nlevel:" + level);
    }
}