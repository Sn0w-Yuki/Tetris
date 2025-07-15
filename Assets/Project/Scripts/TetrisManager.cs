using UnityEngine;
using UnityEngine.UIElements;

public class TetrisManager : MonoBehaviour
{
    [Header("flag")]
    public bool isPause = false;
    public bool isTest = false;

    [Header("Width & Height")]
    public int fieldWidth = 10;
    public int fieldHeight = 20;
    public int fieldDepth = 10;
    public int stageHeight = -1;
    [Header("Position")]
    public Vector3 CenterPos;
    [SerializeField] private int cameraDistance = 25;
    [SerializeField] private GameObject StageCube;
    [SerializeField] private GameObject TestCube;

    [Header("Block Information")]
    public int nowBlock = 0;
    public int nowRoll = 0;

    private Vector3[] cameraPos;
    private int[] cameraRotate = { 0, -90, 180, 90 };
    private Vector3[] UpperRotate;
    private int nowCameraPos = 0;
    private int beforePos = 0;
    private GameObject cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        CenterPos = new Vector3(fieldWidth / 2, fieldHeight / 2, fieldDepth / 2);

        if (isTest)
        {
            Instantiate(TestCube, CenterPos, Quaternion.identity);
            Instantiate(TestCube, new Vector3(CenterPos.x, CenterPos.y + 1, CenterPos.z), Quaternion.identity);
            Instantiate(TestCube, new Vector3(CenterPos.x + 1, CenterPos.y + 1, CenterPos.z), Quaternion.identity);
            Instantiate(TestCube, new Vector3(CenterPos.x + 2, CenterPos.y + 1, CenterPos.z), Quaternion.identity);
        }

        cameraPos = new Vector3[5];
        cameraPos[0] = new Vector3(CenterPos.x, CenterPos.y - 1, CenterPos.z - cameraDistance);
        cameraPos[1] = new Vector3(CenterPos.x + cameraDistance, CenterPos.y - 1, CenterPos.z);
        cameraPos[2] = new Vector3(CenterPos.x, CenterPos.y - 1, CenterPos.z + cameraDistance);
        cameraPos[3] = new Vector3(CenterPos.x - cameraDistance, CenterPos.y - 1, CenterPos.z);
        cameraPos[4] = new Vector3(CenterPos.x, CenterPos.y + cameraDistance, CenterPos.z);

        UpperRotate = new Vector3[4];
        UpperRotate[0] = new Vector3(90, 0, 0);
        UpperRotate[1] = new Vector3(90, 0, 90);
        UpperRotate[2] = new Vector3(90, 180, 0);
        UpperRotate[3] = new Vector3(90, 0, -90);

        RotateCamera(nowCameraPos);
        GenerateStage();
        SetLight(CenterPos);

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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (nowCameraPos != 4)
            {
                UpperRotateCamera(nowCameraPos);
            }
            else
            {
                nowCameraPos = beforePos;
                RotateCamera(nowCameraPos);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (nowCameraPos != 4)
            {
                if (nowCameraPos == 3)
                {
                    RotateCamera(0);
                }
                else
                {
                    RotateCamera(nowCameraPos + 1);
                }
            }
            else
            {
                if (beforePos == 3)
                {
                    UpperRotateCamera(0);
                }
                else
                {
                    UpperRotateCamera(beforePos + 1);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (nowCameraPos != 4)
            {
                if (nowCameraPos == 0)
                {
                    RotateCamera(3);
                }
                else
                {
                    RotateCamera(nowCameraPos - 1);
                }
            }
            else
            {
                if (beforePos == 0)
                {
                    UpperRotateCamera(3);
                }
                else
                {
                    UpperRotateCamera(beforePos - 1);
                }
            }
        }
    }

    void GenerateStage()
    {
        GameObject stage = new GameObject("Stage");
        stage.transform.parent = this.transform;

        //床の生成
        for (int x = -1; x < fieldWidth + 2; x++)
        {
            for (int z = -1; z < fieldDepth + 2; z++)
            {
                Vector3 Pos = new Vector3(x, stageHeight, z);
                GameObject obj = Instantiate(StageCube, Pos, Quaternion.identity);
                obj.transform.parent = stage.transform;
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
                    GameObject obj = Instantiate(StageCube, Pos, Quaternion.identity);
                    obj.transform.parent = stage.transform;
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

    void UpperRotateCamera(int pos)
    {
        beforePos = pos;
        nowCameraPos = 4;
        cam.transform.position = cameraPos[nowCameraPos];
        cam.transform.rotation = Quaternion.Euler(UpperRotate[beforePos]);
    }

    void SetLight(Vector3 vec)
    {
        Vector3 pos = vec + new Vector3(0, cameraDistance, 0);

        GameObject lightGameObject = new GameObject("Light");
        lightGameObject.transform.parent = this.transform;
        Light lightComp = lightGameObject.AddComponent<Light>();

        lightComp.type = LightType.Spot;
        lightComp.intensity = 25f;
        lightComp.range = 35f;
        lightComp.spotAngle = 60f;
        lightComp.color = Color.white;

        lightGameObject.transform.position = pos;
        lightGameObject.transform.forward = Vector3.down;
    }
}