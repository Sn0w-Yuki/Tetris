using System.Runtime.InteropServices;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    public bool isPause = false;
    public bool isFall = false;
    private float floorHeight;
    private float rLimit;
    private float lLimit;
    private Vector3 spawnPos;


    void Start()
    {
        floorHeight = GameObject.FindGameObjectWithTag("Floor").transform.position.y;
        rLimit = GameObject.FindGameObjectWithTag("RightWall").transform.localScale.x;
        lLimit = GameObject.FindGameObjectWithTag("LeftWall").transform.localScale.x;
        spawnPos = new Vector3(0, rLimit - 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPause = !isPause;
        }

        if (!isPause)
        {
            if (isFall)
            {
                //ブロック生成
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

            }








        }
    }
}
