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

    public void WriteText(List<string> dialogs) 
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        DialogText.text = "";
        currentCoroutine = StartCoroutine(WriteMachine(dialogs));
    }

    public void WriteText(string dialogs)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        DialogText.text = "";
        currentCoroutine = StartCoroutine(WriteMachine(new List<string>() { dialogs }));
    }

    private IEnumerator WriteMachine(List<string> dialogs) 
    {
        foreach (string dialog in dialogs) 
        {
            DialogText.text = "";
            char[] letters = dialog.ToCharArray();

            foreach (char _char in letters) 
            { 
                DialogText.text += _char;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(2f);
        }       
    }
}
