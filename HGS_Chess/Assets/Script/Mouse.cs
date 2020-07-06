using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mouse : MonoBehaviour
{
    private Vector3 position;
    void Start()
    {
        
    }
    void Update()
    {
        //マウスの位置情報の取得
        position = Input.mousePosition;
    }
     void OnCollisionStay(Collision collision)
    {
        
    }
}
