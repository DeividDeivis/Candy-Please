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
    /// Cunatos segundos puede esperar un Visitante para recibir su caramelo?.
    /// </summary>
    public float VisitorPatience = 5;

    public Visitor GetNewVisitor() 
    {
        Visitor visitor = visitors[Random.Range(0, visitors.Count)];
        if (visitor != currentVisitor) currentVisitor = visitor;
        else GetNewVisitor();
        return currentVisitor;
    }

    public void FirstVisitor() 
    { 
        Visitor newVisitor = GetNewVisitor();
        controller.LoadVisitorInfo(newVisitor, VisitorPatience);
    }
}
