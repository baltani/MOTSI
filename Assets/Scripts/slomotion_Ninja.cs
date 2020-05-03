using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slomotion_Ninja : MonoBehaviour
{
    public Rigidbody2D r2d;
    Transform t;
    
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = r2d.position;
        
    }
    
}
