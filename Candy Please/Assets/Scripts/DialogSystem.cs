using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogSystem : MonoBehaviour
{
    #region Singleton
    private static DialogSystem instance;
    public static DialogSystem Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField] private TextMeshProUGUI DialogText;
    /// <summary>
    /// Cuantos segundos de espera hay entre cada letra?
    /// </summary>
    [SerializeField][Range(0f, 1f)] private float textSpeed;

    public void WriteText(string dialog) 
    {
        DialogText.text = "";
        char[] letters = dialog.ToCharArray();
        StartCoroutine(WriteMachine(letters));
    }

    private IEnumerator WriteMachine(char[] letters) 
    { 
        foreach (char _char in letters) 
        { 
            DialogText.text += _char;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
