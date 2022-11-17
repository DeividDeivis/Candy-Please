using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogSystem : MonoBehaviour
{
    #region Singleton
    public static DialogSystem Instance;
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }
    #endregion

    [SerializeField] private TextMeshProUGUI DialogText;
    /// <summary>
    /// Cuantos segundos de espera hay entre cada letra?
    /// </summary>
    [SerializeField][Range(0f, 1f)] private float textSpeed;
    private Coroutine currentCoroutine;

    public void WriteText(string dialog) 
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        DialogText.text = "";
        char[] letters = dialog.ToCharArray();
        currentCoroutine = StartCoroutine(WriteMachine(letters));
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
