using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject Pawn;
    public GameObject Night;
    public GameObject Bishop;
    public GameObject Luke;
    public GameObject Queen;
    public GameObject King;
    void Start()
    {
        
    }

    void Update()
    {
    }
    void P_pos()
    {
        Vector3 pawn = GameObject.Find("Pawn").transform.position;

    }
}
