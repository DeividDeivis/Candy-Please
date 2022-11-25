using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VisitorsManager : MonoBehaviour
{
    [SerializeField] private List<Visitor> visitors;
    private Visitor currentVisitor;
    [SerializeField] private VisitorController controller; 

    /// <summary>
    /// Cuantos segundos puede esperar un Visitante para recibir su caramelo?.
    /// </summary>
    public List<Patience> VisitorPatience = new List<Patience>();

    [System.Serializable]
    public class Patience 
    {
        [Range(0, 100)]public int gamePercentage;
        public float _patience;
    }

    public void Initialize() 
    { 
        currentVisitor = null;
        controller.Initialized();
    }

    public Visitor GetNewVisitor() 
    {
        Visitor visitor = visitors[Random.Range(0, visitors.Count)];
        if (visitor != currentVisitor) currentVisitor = visitor;
        else GetNewVisitor();
        return currentVisitor;
    }

    public void SpawnVisitor(float maxTime, float currentTime) 
    { 
        float currentGamePercentage = (currentTime/ maxTime) * 100;
        Visitor newVisitor = GetNewVisitor();
        float newPatient = 0;

        foreach (Patience patience in VisitorPatience) 
        {
            if (currentGamePercentage >= patience.gamePercentage)
                newPatient = patience._patience;
        }

        controller.LoadVisitorInfo(newVisitor, newPatient);
        GameManager.Instance._visitorsInDoor++;
    }
}
