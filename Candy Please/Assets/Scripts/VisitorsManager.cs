using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsManager : MonoBehaviour
{
    [SerializeField] private List<Visitor> visitors;

    public Visitor GetVisitor() 
    { 
        return visitors[0];
    }
}
