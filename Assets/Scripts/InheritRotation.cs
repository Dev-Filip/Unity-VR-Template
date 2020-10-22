using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritRotation : MonoBehaviour
{
    public GameObject objectToFollow;
    
    private void Update()
    {

        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, objectToFollow.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
