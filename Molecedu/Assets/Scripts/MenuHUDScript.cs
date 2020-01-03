﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHUDScript : MonoBehaviour
{    
    public void MoveHUD()
    {
        Vector3 originalPosition = new Vector3(0f, -450f, 0f);
        Vector3 targetPosition = new Vector3(0f, -705f, 0f);
        if (this.transform.localPosition == originalPosition)
        {
            this.transform.localPosition = targetPosition;
        }
        else
        {
            this.transform.localPosition = originalPosition;
        }
    }
    
}

