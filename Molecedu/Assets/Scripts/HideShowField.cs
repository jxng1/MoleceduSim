using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideShowField: MonoBehaviour
{
    public void ShowHidePassword()
    {
        InputField input = transform.parent.GetComponent<InputField>();
        if (input != null)
        {
            if (input.contentType == InputField.ContentType.Standard)
            {
                input.contentType = InputField.ContentType.Password;
            }
            else
            {
                input.contentType = InputField.ContentType.Standard;
            }
            input.ActivateInputField();
            StartCoroutine(WaitForNextFrame(input));
        }
    }

    IEnumerator WaitForNextFrame(InputField input)
    {
        yield return null;
        input.MoveTextEnd(true);
    }
         
}