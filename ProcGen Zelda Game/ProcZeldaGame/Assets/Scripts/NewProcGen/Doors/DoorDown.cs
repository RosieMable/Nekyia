﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDown : Doors {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == FindObjectOfType<Hero>().gameObject)
        {
            base.MoveDown();
        }
    }
}
