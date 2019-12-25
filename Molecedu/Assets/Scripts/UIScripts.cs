using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{

    public InputField passwordField;

    public void ShowHidePassword()
    {
        InputField input = GetComponentInParent<InputField>();
        if (this.passwordField != null)
        {
            if (this.passwordField.contentType == InputField.ContentType.Standard)
            {
                this.passwordField.contentType = InputField.ContentType.Password;
            }
            else
            {
                this.passwordField.contentType = InputField.ContentType.Standard;
            }
            this.passwordField.ActivateInputField();
            StartCoroutine(WaitForNextFrame());
        }
    }

    IEnumerator WaitForNextFrame()
    {
        yield return null;
        this.passwordField.MoveTextEnd(true);
    }




}
