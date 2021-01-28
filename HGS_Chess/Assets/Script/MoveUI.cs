using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    public Transform text;
    public float shake;

    Vector3 textpos;
    void Start()
    {
        textpos = text.position;
    }

    void Update()
    {
        text.position = textpos + Random.insideUnitSphere * shake;
    }
}
