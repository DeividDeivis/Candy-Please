using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CandiesManager : MonoBehaviour
{
    [Header("List Of Candies")]
    [SerializeField] private List<GameObject> Candies;
    [Header("Candy who save the data")]
    [SerializeField] private RectTransform spawnedCandy;

    #region Singleton
    public static CandiesManager Instance;
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }
    #endregion

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

    public RectTransform CandyData() { return spawnedCandy; }
    public void BeginDragCandy(Candy candyInfo, Vector3 candyPos) 
    {
        spawnedCandy.GetComponent<Candy>().Type = candyInfo.Type;        
        spawnedCandy.GetComponent<Candy>().Description = candyInfo.Description;
        spawnedCandy.GetComponent<Candy>().Icon.sprite = candyInfo.Icon.sprite;
        spawnedCandy.transform.position = candyPos;
        spawnedCandy.gameObject.SetActive(true);
    }
    public void EndDragCandy() { spawnedCandy.gameObject.SetActive(false); }
}

