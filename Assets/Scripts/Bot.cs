using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Unit
{
   
    void Start()
    {
        Health = 100;
        Dead = false;
    }

  
    void Update()
    {
        if (Health <=0)
        {
            Destroy(gameObject, 2f);
            return;
        }
    }
}
