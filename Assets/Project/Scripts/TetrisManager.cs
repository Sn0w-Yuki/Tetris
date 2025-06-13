using System.Runtime.InteropServices;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    public bool isPause = false;
    public bool isChange = false;
    public float floorHeight;
    public float floorLimit;
    public float rLimit;
    public float lLimit;
    public Vector3 spawnPos;
    public int nowBlock = 0;
    public int nowRoll = 0;


    void Start()
    {
        floorHeight = GameObject.FindGameObjectWithTag("Floor").transform.position.y;
        floorLimit = GameObject.FindGameObjectWithTag("Floor").transform.position.x;
        rLimit = GameObject.FindGameObjectWithTag("RightWall").transform.localScale.x;
        lLimit = GameObject.FindGameObjectWithTag("LeftWall").transform.localScale.x;
        spawnPos = new Vector3(0, rLimit - 1, 0);
        nowBlock = Random.Range(0, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPause = !isPause;
        }
    }
}
