using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisation : MonoBehaviour
{
    private void Start()
    {
        GameObject[] interfaces = GameObject.FindGameObjectsWithTag("Interfaces");
        CanvasGroup[] canvasGroups = new CanvasGroup[interfaces.Length];
        for (int i = 0; i < interfaces.Length; i++)
        {
            canvasGroups[i] = interfaces[i].GetComponent<CanvasGroup>();
        }
        HideAll(canvasGroups);
    }

    private void HideAll(CanvasGroup[] canvasGroups)
    {
        foreach (CanvasGroup _ in canvasGroups)
        {
            _.alpha = 0f;
            _.interactable = false;
            _.blocksRaycasts = false;
        }       
    }

}

