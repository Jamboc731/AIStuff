using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree : MonoBehaviour
{

    [SerializeField] Node rootNode;
    [SerializeField] Transform[] patrols;
    int currentPatrol = 0;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrols[0].position);
    }

    private void Update()
    {
        rootNode.Evaluate(agent, patrols, currentPatrol);
        //Debug.Log(Vector3.Distance(transform.position, patrols[currentPatrol].position));
    }

    public Transform NextPoint()
    {
        currentPatrol++;
        if (currentPatrol >= patrols.Length) currentPatrol = 0;
        return patrols[currentPatrol];
    }

}
