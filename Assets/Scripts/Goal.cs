﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ballcontroller ball = other.GetComponent<Ballcontroller>();

        if (!ball || GameManager.instance.GameEnded)
            return;

        Debug.Log("Goal was touched");

        GameManager.instance.EndGame(true);
    }
}
