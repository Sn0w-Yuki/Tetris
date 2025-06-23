using UnityEditor.SceneManagement;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("flag")]
    public bool isPause = false;
    public bool isChange = false;

    [Header("Width & Height")]
    public int fieldWidth = 10;
    public int fieldHeight = 20;
    public float wallThickness = 1f;
    public GameObject StageCube;
    private GameObject Floor;
    private GameObject rWall;
    private GameObject lWall;

    [Header("Block Information")]
    public int nowBlock = 0;
    public int nowRoll = 0;

    void Start()
    {
        GenerateStage(Floor, 0.5f, 0, 0, fieldWidth + 2, 1, 1);
        GenerateStage(rWall, fieldWidth / 2 + 1, fieldHeight / 2, 0, 1, fieldHeight, 1);
        GenerateStage(lWall, fieldWidth / -2, fieldHeight / 2, 0, 1, fieldHeight, 1);

        nowBlock = Random.Range(0, 7);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPause = !isPause;
        }
    }

    void GenerateStage(GameObject obj, float xPos, float yPos, float zPos, float xSize, float ySize, float zSize)
    {
        obj = Instantiate(StageCube, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        obj.transform.localScale = new Vector3(xSize, ySize, zSize);
    }
}
