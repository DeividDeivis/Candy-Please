using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DialogText;
    /// <summary>
    /// Cuantos segundos de espera hay entre cada letra?
    /// </summary>
    [SerializeField][Range(0f, 1f)] private float textSpeed;

    public void WriteText(string dialog) 
    {
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
