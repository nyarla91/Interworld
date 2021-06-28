using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolHelper : MonoBehaviour
{
    public bool random => Random.value < 0.5f;
    
    public static int BoolToInt(bool value){
        if (value)
            return 1;
        return 0;
    }

    public static int BoolToIntReversed(bool value){
        if (value)
            return 1;
        return -1;
    }
}
