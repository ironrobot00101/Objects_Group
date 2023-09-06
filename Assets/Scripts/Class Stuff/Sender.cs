using System;
using UnityEngine;
using UnityEngine.Events;

public class Sender : MonoBehaviour
{
    public event EventHandler<EventArgs> OnSpacePressed;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //invoke the event - space bar pressed
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
