using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    public Door door;
    BoxCollider coll=null;
    private void Start()
    {
        coll = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (door.open == true)
        {
            coll. enabled = false;
        }
        if (!door.open)
        {
            coll .enabled  = true;
        }
    }
}
