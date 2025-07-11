using UnityEditor.SceneManagement;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("flag")]
    public bool isPause = false;

    [Header("Width & Height")]
    public int fieldWidth = 10;
    public int fieldHeight = 20;
    public int fieldDepth = 10;
    public int stageHeight = -1;
    [Header("Position")]
    public Vector3 CenterPos;
    [SerializeField] private int cameraDistance = 25;
    [SerializeField] private GameObject StageCube;

    [Header("Block Information")]
    public int nowBlock = 0;
    public int nowRoll = 0;

    private Vector3[] cameraPos;
    private int[] cameraRotate = { 0, -90, 180, 90 };
    private int nowCameraPos = 0;
    private GameObject cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        CenterPos = new Vector3(fieldWidth / 2, fieldHeight / 2, fieldDepth / 2);
        Instantiate(StageCube, CenterPos, Quaternion.identity);

        cameraPos = new Vector3[4];
        cameraPos[0] = new Vector3(CenterPos.x, CenterPos.y - 1, CenterPos.z - cameraDistance);
        cameraPos[1] = new Vector3(CenterPos.x + cameraDistance, CenterPos.y - 1, CenterPos.z);
        cameraPos[2] = new Vector3(CenterPos.x, CenterPos.y - 1, CenterPos.z + cameraDistance);
        cameraPos[3] = new Vector3(CenterPos.x - cameraDistance, CenterPos.y - 1, CenterPos.z);

        RotateCamera(nowCameraPos);
        GenerateStage();
        SetLight();

        nowBlock = Random.Range(0, 7);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPause = !isPause;
        }

        if (isPause)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (nowCameraPos > 2)
            {
                RotateCamera(0);
            }
            else
            {
                RotateCamera(nowCameraPos + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (nowCameraPos < 1)
            {
                RotateCamera(3);
            }
            else
            {
                RotateCamera(nowCameraPos - 1);
            }
        }
    }

    void GenerateStage()
    {
        //床の生成
        for (int x = 0; x < fieldWidth + 1; x++)
        {
            for (int z = 0; z < fieldDepth + 1; z++)
            {
                Vector3 Pos = new Vector3(x, stageHeight, z);
                Instantiate(StageCube, Pos, Quaternion.identity);
            }
        }

        //柱の生成
        for (int x = -1; x <= fieldWidth + 2; x += fieldWidth + 2)
        {
            for (int y = 0; y < fieldHeight; y++)
            {
                for (int z = -1; z <= fieldDepth + 2; z += fieldDepth + 2)
                {
                    Vector3 Pos = new Vector3(x, y, z);
                    Instantiate(StageCube, Pos, Quaternion.identity);
                }
            }
        }
    }

    void RotateCamera(int pos)
    {
        nowCameraPos = pos;
        // カメラを位置と角度にセット
        cam.transform.position = cameraPos[pos];
        cam.transform.rotation = Quaternion.Euler(0, cameraRotate[pos], 0);
    }

    void SetLight()
    {
        GameObject lightGameObject = new GameObject("Point Light");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.type = LightType.Point;
        lightComp.color = Color.white;
        lightComp.range = 20f;
        lightComp.intensity = 200f;
        lightGameObject.transform.position = CenterPos;
    }
}
