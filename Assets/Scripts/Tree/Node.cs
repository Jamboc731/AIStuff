﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Node
{

    [SerializeField] Node previousNode;
    [SerializeField] Node[] childNodes;
    [SerializeField] NodeType type;
    [SerializeField] Data behaviourData;

    //0 done, 1 inProgress, 2 failed
    int result = 2;

    public Node[] GetChildren()
    {
        return childNodes;
    }

    public NodeType GetNodeType()
    {
        return type;
    }

    public int Evaluate(NavMeshAgent agent, Transform[] patrols, int currentPatrol)
    {
        //Debug.Log(type);
        bool inProgress = false;
        switch (type)
        {
            case NodeType.leaf:
                //Debug.Log(behaviourData.function);
                Run(agent, GameManager.x.GetPlayer(), patrols[currentPatrol], false, false);
                break;

            case NodeType.sequence:
                //Debug.Log("Doing sequence of " + childNodes.Length + " length");
                foreach (var n in childNodes)
                {
                    int result = n.Evaluate(agent, patrols, currentPatrol);
                    switch (result)
                    {
                        case 0:
                            break;
                        default:
                            return result;
                    }
                }
                if (inProgress) return 1;
                else return 0;

            case NodeType.selector:
                foreach (var n in childNodes)
                {
                    int result = n.Evaluate(agent, patrols, currentPatrol);
                    switch (result)
                    {
                        case 0:
                            return result;
                        case 1:
                            inProgress = true;
                            break;
                        default:
                            break;
                    }
                }
                if (inProgress) return 1;
                else return 2;

            default:
                return 2;
        }

        return result;
    }

    private void Run(NavMeshAgent agent, Transform player, Transform targetLocation, bool toCheck, bool invert)
    {
        switch (behaviourData.function)
        {
            case Function.patrol0:
                if (behaviourData.CheckAtPos(agent.transform, targetLocation.position, 0.5f, false))
                {
                    Debug.Log("at pos");
                    result = 0;
                    targetLocation = GetNewTargetLocation(agent);
                    behaviourData.GoTo(agent, targetLocation);
                }
                else result = 1;
                break;

            case Function.goTo1:
                try
                {
                    behaviourData.GoTo(agent, targetLocation);
                    result = 0;
                }
                catch
                {
                    result = 2;
                }
                break;

            case Function.checkBool2:
                if (behaviourData.CheckBool(toCheck, invert)) result = 0;
                else result = 2;
                break;

            case Function.checkLOS3:
                if (behaviourData.CheckLOS(agent.transform, player, invert)) result = 0;
                else result = 2;
                break;

            case Function.checkAtPos4:
                if (behaviourData.CheckAtPos(agent.transform, targetLocation.position, 0.5f, invert)) result = 0;
                else result = 2;
                break;

            case Function.chase5:
                behaviourData.Chase(agent);
                break;

            default:
                result = 2;
                break;
        }
    }

    private Transform GetNewTargetLocation(NavMeshAgent agent)
    {
        return agent.GetComponent<BehaviourTree>().NextPoint();
    }

}

public enum NodeType
{
    leaf = 0,
    sequence = 1,
    selector = 2
}