using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Visitor", menuName = "Trato o Truco/Create Visitor", order = 1)]
public class Visitor : ScriptableObject
{
    [Header("Visitor ID")]
    public string VisitorID;

    [Header("Visitor Dialog")]
    [TextArea(2, 10)] public List<string> Dialog;

    [Header("Animations")]
    public Sprite Normal;
    public Sprite Impatient;
    public Sprite Angry;
    public Sprite Happy;

    [Header("Candies he like")]
    public List<CandyType> CandiesLike;
}
