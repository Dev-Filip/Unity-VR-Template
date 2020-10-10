using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Rigidbody rigidbody;
    [SerializeField] private float forceMultiplier = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Launch()
    {
        rigidbody.AddRelativeForce(Vector3.forward*forceMultiplier, ForceMode.Impulse);
    }

    
}
