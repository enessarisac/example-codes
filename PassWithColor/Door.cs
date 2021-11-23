using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public string color;
    public bool open;
    

    private void Start()
    {
        open = true;

    }
    private void OnTriggerStay(Collider other)
    {
         if (other.gameObject.CompareTag(color))
        {
            
            open = false;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        

            open = true;
        
        
    }

   
}
    
   

