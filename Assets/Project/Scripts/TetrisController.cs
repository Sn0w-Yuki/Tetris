using UnityEngine;

public class TetrisController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float rightLimit = 4f;
    public float leftLimit = -4f;
    public float floorHeight = 0f;
    public float fallTime = 1f;

    private float timer;
    private Vector3 playerPos;

    void Update()
    {
        playerPos = transform.position;

        // 自動落下
        timer += Time.deltaTime;
        if (timer >= fallTime)
        {
            if (playerPos.y < floorHeight)
            {
                playerPos.y = floorHeight - 1f;
            }
            else
            {
                playerPos.y -= moveSpeed;
            }
            timer = 0f;
        }

        // 右移動
        if (Input.GetKeyDown(KeyCode.RightArrow) && playerPos.x + moveSpeed <= rightLimit)
        {
            playerPos.x += moveSpeed;
        }

        // 左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow) && playerPos.x - moveSpeed >= leftLimit)
        {
            playerPos.x -= moveSpeed;
        }

        transform.position = playerPos;
        Debug.Log(playerPos);
    }
}
