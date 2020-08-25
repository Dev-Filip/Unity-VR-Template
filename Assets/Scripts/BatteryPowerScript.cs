using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatteryPowerScript : MonoBehaviour
{
    [SerializeField] Light lightSource = null;

    [SerializeField] List<XRSocketInteractor> batterySockets = new List<XRSocketInteractor>();
    [SerializeField] bool outputingPower = false;


    public void UpdatePowerDelivery()
    {
        //check how many battery sockets are empty
        //int emptySockets = 0;
        outputingPower = true;
        foreach (XRSocketInteractor socket in batterySockets)
        {
            if(!socket.selectTarget || !socket.selectTarget.tag.Equals("Battery"))
            {
                outputingPower = false;
                break;
            }
        }

        //outputingPower = (emptySockets == 0);
        lightSource.enabled = outputingPower;
        Debug.Log("Light active: " + outputingPower);
    }
}
