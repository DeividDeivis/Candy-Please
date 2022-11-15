using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiesManager : MonoBehaviour
{
    [Header("List Of Candies")]
    [SerializeField] private List<GameObject> Candies;

    public void SortCandiesList() 
    {
        List<GameObject> aux = new List<GameObject>();
        while (Candies.Count > 0)
        {
            int sortIndex = Random.Range(0, Candies.Count);
            aux.Add(Candies[sortIndex]);
            Candies.Remove(Candies[sortIndex]);
        }
        Candies = aux;
    }

    public List<GameObject> GetCandies() { return Candies; }
}

