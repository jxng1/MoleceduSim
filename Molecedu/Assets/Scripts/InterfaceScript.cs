using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    public void CloseOpenInterface()
    {
        if (this.GetComponent<CanvasGroup>().alpha == 1)
        {
            this.GetComponent<CanvasGroup>().alpha = 0f;
            this.GetComponent<CanvasGroup>().interactable = false;
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else if (this.GetComponent<CanvasGroup>().alpha == 0)
        {
            this.GetComponent<CanvasGroup>().alpha = 1f;
            this.GetComponent<CanvasGroup>().interactable = true;            
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

    }

}
