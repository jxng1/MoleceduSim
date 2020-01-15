using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    public void CloseOpenInterface()
    {
        if (this.GetComponent<CanvasGroup>().alpha == 1f)
        {
            this.GetComponent<CanvasGroup>().alpha = 0f;
            this.GetComponent<CanvasGroup>().interactable = false;
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else if (this.GetComponent<CanvasGroup>().alpha == 0f)
        {
            this.GetComponent<CanvasGroup>().alpha = 1f;
            this.GetComponent<CanvasGroup>().interactable = true;            
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

    }
    public void CloseOpenInterface(CanvasGroup canvasGroup)
    {
        if (canvasGroup.GetComponent<CanvasGroup>().alpha == 1f)
        {
            canvasGroup.GetComponent<CanvasGroup>().alpha = 0f;
            canvasGroup.GetComponent<CanvasGroup>().interactable = false;
            canvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            canvasGroup.GetComponent<CanvasGroup>().alpha = 1f;
            canvasGroup.GetComponent<CanvasGroup>().interactable = true;
            canvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

}

