using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Rigidbody rigidBody;
    [SerializeField] private float forceMultiplier = 10f;
    public void FireBullet()
    {
        rigidBody.AddRelativeForce(Vector3.forward*forceMultiplier,ForceMode.Impulse);
    }
}
