using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{

    public int CurrentX { set; get; }
    public int CurrentV { set; get; }

    public bool isWhite;//白駒と黒駒を見るためのフラグ(白駒を手動でオンにする)
   
}
