using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{

    [SerializeField] Node previousNode;
    [SerializeField] Node[] childNodes;
    [SerializeField] NodeType type;

}

public enum NodeType
{
    leaf,
    sequence,
    selector
}