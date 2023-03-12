using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Data
{
    public int dataValue;
    public int dataUp;
    public int dataDown;
    public int dataLeft;
    public int dataRight;

    public Data(int dataValue, int dataUp, int dataDown, int dataLeft, int dataRight)
    {
        this.dataValue = dataValue;
        this.dataUp = dataUp;
        this.dataDown = dataDown;
        this.dataLeft = dataLeft;
        this.dataRight = dataRight;
    }
}
public class Dice : MonoBehaviour
{
    public int valueStart;
    public int diceID;
    public int value;
    public TMP_Text valueDisplay;

    public TMP_Text previewUp, previewDown, previewLeft, previewRight;
    public GameObject previews;

    public List<Data> diceData = new List<Data>();

    public int up;
    public int down;
    public int left;
    public int right;
    
    public void Awake()
    {
        InitDice();
        TextUpdate();
    }
    public void RollDice(Vector3 direction)
    {
        int valueOld = value;

        if (direction.x == 0 && direction.y == 1)
        {
            //up
            value = down;
            
            up = valueOld;
            down = 7 - valueOld;
        }
        else
        {
            if (direction.x == 0 && direction.y == -1)
            {
                //down
                value = up;

                up = 7 - valueOld;
                down = valueOld;
            }
            else
            {
                if (direction.x == -1 && direction.y == 0)
                {
                    //left
                    value = right;

                    right = 7 - valueOld;
                    left = valueOld;
                }
                else
                {
                    if (direction.x == 1 && direction.y == 0)
                    {
                        //right
                        value = left;

                        right = valueOld;
                        left = 7 - valueOld;
                    }
                }
            }
        }

        TextUpdate();
    }

    void InitDice()
    {
        value = valueStart;

        if(value == 1)
        {
            up = 5;
            down = 2;
            left = 4;
            right = 3;
        } else
        {
            if (value == 2)
            {
                up = 1;
                down = 6;
                left = 4;
                right = 3;
            } else
            {
                if (value == 3)
                {
                    up = 1;
                    down = 6;
                    left = 2;
                    right = 5;
                } else
                {
                    if (value == 4)
                    {
                        up = 1;
                        down = 6;
                        left = 5;
                        right = 2;
                    } else
                    {
                        if (value == 5)
                        {
                            up = 1;
                            down = 6;
                            left = 3;
                            right = 4;
                        } else
                        {
                            if (value == 6)
                            {
                                up = 5;
                                down = 2;
                                left = 3;
                                right = 4;
                            }
                        }
                    }
                }
            }
        }
    }

    public void TextUpdate()
    {
        valueDisplay.text = value.ToString();
        previewUp.text = down.ToString();
        previewDown.text = up.ToString();
        previewLeft.text = right.ToString();
        previewRight.text = left.ToString();
    }

    public void PreviewsOn()
    {
        previews.SetActive(true);
    }

    public void PreviewsOff()
    {
        previews.SetActive(false);
    }
}
