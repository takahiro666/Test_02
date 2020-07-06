using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    //[SerializeField] private GameObject MainCam;
    //[SerializeField] private GameObject SubCam;

    public Rigidbody rdoby;
    private float speed;
    private float radiuds;  //直径
    private float yPosition;//カメラY軸のポジション
    private float xPosition;//カメラX軸のポジション

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        radiuds = 5.0f;
        //yPosition = 7;
        xPosition = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("1"))
        //{
        //    MainCam.SetActive(!MainCam.activeSelf);
        //    SubCam.SetActive(!SubCam.activeSelf);
        //}

        //if(Input.GetKeyDown(KeyCode.A))
        //{
            rdoby.MovePosition(new Vector3(xPosition,
                radiuds * Mathf.Sin(Time.time * -speed),
                radiuds * Mathf.Cos(Time.time * speed)));
        //}
        
    }
}
