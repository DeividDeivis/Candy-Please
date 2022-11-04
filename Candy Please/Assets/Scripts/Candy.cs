using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [Header("Type of Candy")]
    public CandyType Type;
    [Header("Description of the candy")]
    public string description; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum CandyType 
{ 
    Apple,
    Alfajor,
    Cookie,
    PinkCandy,
    Chocolate,
    IceCream,
    GreenCandy,
    PinkPalette,
    BlueCandy,
    Mate,
    GreyCandy,
    RedPalette,
    PuerpleCandy,
    OrangeCandy,
    RedCandy,
    Eye
}
