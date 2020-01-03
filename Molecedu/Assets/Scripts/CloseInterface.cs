using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInterface : MonoBehaviour
{

    public void CloseCurrentInterface()
    {
        if (this.GetComponent<CanvasGroup>().alpha == 1)
        {
            this.GetComponent<CanvasGroup>().alpha = 0;
            this.GetComponent<CanvasGroup>().interactable = false;
        }

    }

}
