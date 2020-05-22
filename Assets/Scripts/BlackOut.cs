using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOut : Unit
{
   
    void Start()
    {
        Health = 1;
        Dead = false;
    }

    
    void Update()
    {
        {
            if (Health <= 0)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
