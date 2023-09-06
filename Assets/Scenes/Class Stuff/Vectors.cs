using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    public Vector2 myVector2;

    public float myfloat = 1.0f;

    void Start()
    {
        myVector2.x = 1.0f;
        myVector2.y = 2.0f;
    }

    private void Update()
    {
        //multiply vector by scalar
        myVector2 = myVector2 * myfloat;
        Debug.Log(myVector2);

    }
}
