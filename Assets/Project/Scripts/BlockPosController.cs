using Unity.VisualScripting;
using UnityEngine;

public class BlockPosController : MonoBehaviour
{
    [SerializeField] private GameObject[] Cube;
    [SerializeField] private Material LockedCube;

    private float width, height, depth;
    [SerializeField] private float spawnDiff = 2f;
    private int[,,] blockPos;
    private Vector3 spawnPos;
    public int nowBlock = 0;
    public int nowRoll = 0;
    [SerializeField] private TetrisManager tetrisManager;
    private GameObject centerBlock;
    private GameObject[] arroundBlock;

    void Start()
    {
        InitialSetting();




        SpawnBlock(nowBlock);
    }

    void Update()
    {

    }

    void InitialSetting()
    {
        width = tetrisManager.fieldWidth;
        height = tetrisManager.fieldHeight;
        depth = tetrisManager.fieldDepth;
        spawnPos = new Vector3(width / 2, height - 1, depth / 2);
        arroundBlock = new GameObject[3];

        blockPos = new int[(int)width, (int)height, (int)depth];

        nowBlock = Random.Range(0, 6);
    }

    void SpawnBlock(int blockNum)
    {
        Vector3 centerOffset = new Vector3(0, 0, 0);
        Vector3[] arroundOffset = new Vector3[3];
        switch (blockNum)
        {
            case 0:
                centerOffset = Vector3.zero;
                arroundOffset = new Vector3[]{
                    new Vector3(1, 0, 0),
                    new Vector3(2, 0, 0),
                    new Vector3(1, -1, 0),
                };
                break;
            case 1:
                centerOffset = Vector3.zero;
                arroundOffset = new Vector3[]{
                    new Vector3(1, 0, 0),
                    new Vector3(2, 0, 0),
                    new Vector3(2, -1, 0),
                };
                break;
            case 2:
                centerOffset = new Vector3(2, 0, 0);
                arroundOffset = new Vector3[]{
                    new Vector3(0, 0, 0),
                    new Vector3(1, 0, 0),
                    new Vector3(0, -1, 0),
                };
                break;
            case 3:
                centerOffset = Vector3.zero;
                arroundOffset = new Vector3[]{
                    new Vector3(1, 0, 0),
                    new Vector3(1, -1, 0),
                    new Vector3(2, -1, 0),
                };
                break;
            case 4:
                centerOffset = new Vector3(2, 0, 0);
                arroundOffset = new Vector3[]{
                    new Vector3(1, 0, 0),
                    new Vector3(0, -1, 0),
                    new Vector3(1, -1, 0),
                };
                break;
            case 5:
                centerOffset = Vector3.zero;
                arroundOffset = new Vector3[]{
                    new Vector3(0, -1, 0),
                    new Vector3(0, -2, 0),
                    new Vector3(0, -3, 0),
                };
                break;
            case 6:
                centerOffset = Vector3.zero;
                arroundOffset = new Vector3[]{
                    new Vector3(1, 0, 0),
                    new Vector3(0, -1, 0),
                    new Vector3(1, -1, 0),
                };
                break;
            default:
                break;
        }

        centerBlock = Instantiate(Cube[blockNum], spawnPos + centerOffset, Quaternion.identity);
        for (int i = 0; i < 3; i++)
        {
            arroundBlock[i] = Instantiate(Cube[blockNum], spawnPos + arroundOffset[i], Quaternion.identity);
        }
    }

    void LockBlock()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (blockPos[x, y, z] > 0 && blockPos[x, y, z] < 10)
                    {
                        Instantiate(Cube[nowBlock % 10], new Vector3(x, y, z), Quaternion.identity);
                    }
                }
            }
        }
    }

}