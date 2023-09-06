using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sender sender = FindObjectOfType<Sender>();
        sender.OnSpacePressed += Sender_OnSpacePressed;
    }

    private void Sender_OnSpacePressed(object sender, System.EventArgs e)
    {
        Debug.Log("Space pressed");
    }
}
