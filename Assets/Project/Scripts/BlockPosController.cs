using TMPro;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockPosController : MonoBehaviour
{
    public int[][] blPos;
    private TetrisManager tetrisManager;
    private float ColumnLength; //縦
    private float RowLength;    //横
    private float Depth;
    private int nowX;
    private int nowY;
    private int nowZ;
    private float xPos;
    private float yPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tetrisManager == null)
        {
            tetrisManager = GetComponent<TetrisManager>();
            if (tetrisManager == null)
            {
                Debug.Log("No TetrisManager");
            }
        }
        RowLength = tetrisManager.rLimit - 1;
        ColumnLength = tetrisManager.floorLimit;

        for (int y = 0; y < RowLength; y++)
        {
            for (int x = 0; x < ColumnLength; x++)
            {
                blPos[y][x] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tetrisManager.isChange)
        {
            //ブロック生成
        }

        if (!tetrisManager.isPause)
        {
            //ここで2の場所把握

            BlockMove();

            //左右移動処理
        }
    }

    /// <summary>
    /// 盤面を配列で管理
    /// 回転とブロックから配列を参照し、ブロックが移動できるかを確認
    /// </summary>
    void BlockMove()
    {
        int roll = tetrisManager.nowRoll;
        int block = tetrisManager.nowBlock;

        switch (roll)
        {
            case 0:
                switch (block)
                {
                    case 0:
                        CheckBlockMove(block, roll, 0);
                        //1と2を一個下にずらす
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        Debug.Log("nowBlockの値を見直して");
                        break;
                }
                break;
            case 1:
                switch (tetrisManager.nowBlock)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        Debug.Log("nowBlockの値を見直して");
                        break;
                }
                break;
            case 2:
                switch (tetrisManager.nowBlock)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        Debug.Log("nowBlockの値を見直して");
                        break;
                }
                break;
            case 3:
                switch (tetrisManager.nowBlock)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        Debug.Log("nowBlockの値を見直して");
                        break;
                }
                break;
            default:
                Debug.Log("存在しない回転です");
                break;
        }
    }

    void SearchCenter()
    {
        for (int y = 0; y < RowLength; y++)
        {
            for (int x = 0; x < ColumnLength; x++)
            {
                if (blPos[y][x] == 2)
                {
                    nowX = x;
                    nowY = y;

                }
            }
        }
        Debug.Log("(" + nowX + "," + nowY + "," + nowZ + ")");
    }

    /// <summary>
    /// ブロックの左右、下の動きを管理
    /// </summary>
    /// <param name="block">現在のブロックの種類</param>
    /// <param name="roll">現在の回転回数</param>
    /// <param name="Pos">配列内の場所</param>
    /// <param name="dir">移動方向 0:下,1:右,2:左</param>
    void CheckBlockMove(int block, int roll, int dir)
    {
        SearchCenter();
        int x = nowX;
        int y = nowY;
        int z = nowZ;

        switch (dir)
        {
            // 下方向
            case 0:
                switch (block)
                {
                    case 0: // Tブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 1][x] == 0 && blPos[y + 2][x + 1] == 0 && blPos[y + 1][x + 2] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y][x + 1] = 0;
                                    blPos[y][x + 2] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 1][x + 2] = 1;
                                    blPos[y + 2][x + 1] = 1;
                                }
                                break;
                            case 1:
                                if (blPos[y + 1][x] == 0 && blPos[y][x + 1] == 0)
                                {
                                    blPos[y - 2][x] = 0;
                                    blPos[y - 1][x + 1] = 0;
                                    blPos[y][x] = 1;
                                    blPos[y + 1][x] = 2;
                                    blPos[y][x + 1] = 1;
                                }
                                break;
                            case 2:
                                if (blPos[y + 1][x - 2] == 0 && blPos[y + 1][x - 1] == 0 && blPos[y + 1][x] == 0)
                                {
                                    blPos[y][x - 2] = 0;
                                    blPos[y - 1][x - 1] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x - 2] = 1;
                                    blPos[y + 1][x - 1] = 1;
                                    blPos[y + 1][x] = 2;
                                }
                                break;
                            default:
                                if (blPos[y + 2][x - 1] == 0 && blPos[y + 3][x] == 0)
                                {
                                    blPos[y + 1][x - 1] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y + 2][x - 1] = 1;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 3][x] = 1;
                                }
                                break;
                        }
                        break;

                    case 1: // Jブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 1][x] == 0 && blPos[y + 1][x + 1] == 0 && blPos[y + 2][x + 2] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y][x + 1] = 0;
                                    blPos[y][x + 2] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 1][x + 1] = 1;
                                    blPos[y + 2][x + 2] = 1;
                                }
                                break;
                            case 1:
                                if (blPos[y + 1][x] == 0 && blPos[y - 1][x + 1] == 0)
                                {
                                    blPos[y - 2][x] = 0;
                                    blPos[y - 2][x + 1] = 0;
                                    blPos[y - 1][x + 1] = 1;
                                    blPos[y][x] = 1;
                                    blPos[y + 1][x] = 2;
                                }
                                break;
                            case 2:
                                if (blPos[y + 1][x - 2] == 0 && blPos[y + 1][x - 1] == 0 && blPos[y + 1][x] == 0)
                                {
                                    blPos[y - 1][x - 2] = 0;
                                    blPos[y][x - 2] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x - 2] = 1;
                                    blPos[y + 1][x - 1] = 1;
                                    blPos[y + 1][x] = 2;
                                }
                                break;
                            default:
                                if (blPos[y + 3][x - 1] == 0 && blPos[y + 3][x] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y + 2][x - 1] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 3][x - 1] = 1;
                                    blPos[y + 3][x] = 1;
                                }
                                break;
                        }
                        break;
                    case 2: // Lブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 2][x - 2] == 0 && blPos[y + 1][x - 1] == 0 && blPos[y + 1][x] == 0)
                                {
                                    blPos[y][x - 2] = 0;
                                    blPos[y][x - 1] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y + 2][x - 2] = 1;
                                    blPos[y + 1][x - 1] = 1;
                                    blPos[y + 1][x] = 2;
                                }
                                break;
                            case 1:
                                if (blPos[y + 3][x] == 0 && blPos[y + 3][x + 1] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y + 2][x + 1] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 3][x] = 1;
                                    blPos[y + 3][x + 1] = 1;
                                }
                                break;
                            case 2:
                                if (blPos[y + 1][x] == 0 && blPos[y + 1][x + 1] == 0 && blPos[y + 1][x + 2] == 0)
                                {
                                    blPos[y - 1][x + 2] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y][x + 1] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 1][x + 1] = 1;
                                    blPos[y + 1][x + 2] = 1;
                                }
                                break;
                            default:
                                if (blPos[y - 1][x - 1] == 0 && blPos[y + 1][x] == 0)
                                {
                                    blPos[y - 2][x - 1] = 0;
                                    blPos[y - 2][x] = 0;
                                    blPos[y - 1][x - 1] = 1;
                                    blPos[y][x] = 1;
                                    blPos[y + 1][x] = 2;
                                }
                                break;
                        }
                        break;

                    case 3: // Zブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 1][x] == 0 && blPos[y + 2][x + 1] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y][x + 1] = 0;
                                    blPos[y + 1][x + 2] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 2][x + 1] = 1;
                                    blPos[y + 2][x + 2] = 1;
                                }
                                break;
                            case 1:
                                if (blPos[y + 3][x - 1] == 0 && blPos[y + 2][x] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x - 1] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 2][x] = 1;
                                    blPos[y + 3][x - 1] = 2;
                                }
                                break;
                        }
                        break;

                    case 4: // Sブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 2][x - 2] == 0 && blPos[y + 2][x - 1] == 0 && blPos[y + 1][x] == 0)
                                {
                                    blPos[y][x - 1] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x - 2] = 0;
                                    blPos[y + 2][x - 2] = 2;
                                    blPos[y + 2][x - 1] = 1;
                                    blPos[y + 1][x] = 0;
                                }
                                break;
                            case 1:
                                if (blPos[y + 2][x] == 0 && blPos[y + 3][x + 1] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x + 1] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 2][x] = 1;
                                    blPos[y + 3][x + 1] = 1;
                                }
                                break;
                        }
                        break;

                    case 5: // Iブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 4][x] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 4][x] = 1;
                                }
                                break;
                            case 1:
                                if (blPos[y + 1][x - 3] == 0 && blPos[y + 1][x - 2] == 0 && blPos[y + 1][x - 1] == 0 && blPos[y + 1][x] == 0)
                                {
                                    blPos[y][x - 3] = 0;
                                    blPos[y][x - 2] = 0;
                                    blPos[y][x - 1] = 0;
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x - 3] = 1;
                                    blPos[y + 1][x - 2] = 1;
                                    blPos[y + 1][x - 1] = 1;
                                    blPos[y + 1][x] = 2;
                                }
                                break;
                        }
                        break;

                    case 6: // Oブロック
                        switch (roll)
                        {
                            case 0:
                                if (blPos[y + 2][x] == 0 && blPos[y + 2][x + 1] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y][x + 1] = 0;
                                    blPos[y + 2][x] = 2;
                                    blPos[y + 2][x + 1] = 1;
                                }
                                break;
                            case 1:
                                if (blPos[y + 1][x] == 0 && blPos[y + 2][x + 1] == 0 && blPos[y + 2][x + 2] == 0 && blPos[y + 1][x + 3] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y + 1][x + 1] = 0;
                                    blPos[y + 1][x + 2] = 0;
                                    blPos[y][x + 3] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y + 2][x + 1] = 1;
                                    blPos[y + 2][x + 2] = 1;
                                    blPos[y + 1][x + 3] = 1;
                                }
                                break;
                            case 2:
                                if (blPos[y + 1][x] == 0 && blPos[y + 1][x + 1] == 0 && blPos[y + 1][x + 2] == 0 && blPos[y + 1][x + 3] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y][x + 1] = 0;
                                    blPos[y][x + 2] = 0;
                                    blPos[y][x + 3] = 0;
                                    blPos[y + 1][x] = 1;
                                    blPos[y + 1][x + 1] = 1;
                                    blPos[y + 1][x + 2] = 1;
                                    blPos[y + 1][x + 3] = 2;
                                }
                                break;
                            case 3:
                                if (blPos[y + 1][x] == 0 && blPos[y][x + 1] == 0 && blPos[y][x + 2] == 0 && blPos[y + 1][x + 3] == 0)
                                {
                                    blPos[y][x] = 0;
                                    blPos[y - 1][x + 1] = 0;
                                    blPos[y - 1][x + 2] = 0;
                                    blPos[y][x + 3] = 0;
                                    blPos[y + 1][x] = 2;
                                    blPos[y][x + 1] = 1;
                                    blPos[y][x + 2] = 1;
                                    blPos[y + 1][x + 3] = 1;
                                }
                                break;
                        }
                        break;

                }
                break;

            // 右方向
            case 1:

                break;

            // 左方向
            case 2:

                break;
        }
    }

}