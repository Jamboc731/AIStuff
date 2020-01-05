using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct Data
{

    [SerializeField] public Function function;

    public void GoTo(NavMeshAgent agent, Transform pos)
    {
        agent.SetDestination(pos.position);
    }
    
    public bool CheckBool(bool val, bool invert)
    {
        if (invert) return !val;
        else return val;
    }
    
    public bool CheckLOS(Transform _origin, Transform check, bool invert)
    {
        bool canSee = false;

        RaycastHit hit;

        Physics.Raycast(_origin.position, (check.position - _origin.position).normalized, out hit, 32);

        if (hit.collider.transform == check) canSee = true;

        return CheckBool(canSee, invert);

    }
    
    public bool CheckAtPos(Transform ob, Vector3 pos, float range, bool invert)
    {
        return CheckBool((pos - ob.position).magnitude <= range, invert);
    }

}

public enum Function
{

    patrol = 0,
    goTo = 1,
    checkBool = 2,
    checkLOS = 3,
    checkAtPos = 4

}