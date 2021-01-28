using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MveImage : MonoBehaviour
{
    public GameObject image;
    public RectTransform image1;
    void Start()
    {
        
    }

    void Update()
    {
        image.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
        image1.position += new Vector3(-0.1f, -0.1f, 0.0f);
    }
}
