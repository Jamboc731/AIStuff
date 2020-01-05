using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager x;

    [SerializeField] Transform player;

    private void Awake()
    {
        x = this;
    }

    public Transform GetPlayer()
    {
        return player;
    }

}
