using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour {

    // This are reference for the sprites that are going to be assigned inside of the ide
    [SerializeField]
    private Sprite spU, spD, spR, spL,
                spUD, spRL, spUR, spUL, spDR, spDL,
                spULD, spRUL, spDRU, spLDR, spUDRL;


    //Variables that will be set from the room data
    public bool up, down, left, right;
    public int type; // 0: enter, 1: normal, 2: BossRoom possible exit


    //Variables to assign different colors to different rooms
    public Color normalColor, enterColor;
    private Color mainColor;

    //Reference to the sprite renderer
    private SpriteRenderer rend;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mainColor = normalColor;
        PickSprite(); //Method to pick the sprite depending on the door bools -> It is called in the start method of the script, which is going to be called by the lvlgen
        PickColor(); //Picks the right color for the sprite, depending on the type of the room
    }

    #region PickSprite- Method to pick the sprite depending on the door bools -> It is called in the start method of the script, which is going to be called by the lvlgen
    void PickSprite()
    { //picks correct sprite based on the four door bools
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        rend.sprite = spUDRL;
                    }
                    else
                    {
                        rend.sprite = spDRU;
                    }
                }
                else if (left)
                {
                    rend.sprite = spULD;
                }
                else
                {
                    rend.sprite = spUD;
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        rend.sprite = spRUL;
                    }
                    else
                    {
                        rend.sprite = spUR;
                    }
                }
                else if (left)
                {
                    rend.sprite = spUL;
                }
                else
                {
                    rend.sprite = spU;
                }
            }
            return;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    rend.sprite = spLDR;
                }
                else
                {
                    rend.sprite = spDR;
                }
            }
            else if (left)
            {
                rend.sprite = spDL;
            }
            else
            {
                rend.sprite = spD;
            }
            return;
        }
        if (right)
        {
            if (left)
            {
                rend.sprite = spRL;
            }
            else
            {
                rend.sprite = spR;
            }
        }
        else
        {
            rend.sprite = spL;
        }
    }
    #endregion

    #region PickColor - Picks the right color for the sprite, depending on the type of the room
    void PickColor()
    { //changes color based on what type the room is
        if (type == 0)
        {
            mainColor = enterColor;
        }
        else if (type == 1)
        {
            mainColor = normalColor;
        }
        else if (type == 2)
        {
            mainColor = Color.red;
        }
        rend.color = mainColor;
    }
    #endregion
}
