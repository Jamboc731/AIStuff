using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{

    [SerializeField] Node previousNode;
    [SerializeField] Node[] childNodes;
    [SerializeField] NodeType type;

    [SerializeField] Data behaviourData;


}

public enum NodeType
{
    leaf,
    sequence,
    selector
}

public enum Function
{

}