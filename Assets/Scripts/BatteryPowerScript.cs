using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BatteryPowerScript : MonoBehaviour
{
    private MeshRenderer lightMeshRenderer = null;

    [SerializeField] Light lightSource = null;
    [SerializeField] List<XRSocketInteractor> batterySockets = new List<XRSocketInteractor>();
    [SerializeField] bool outputingPower = false;


    private void Awake()
    {
        if(lightSource) lightMeshRenderer = lightSource.GetComponent<MeshRenderer>();
    }

    public void UpdatePowerDelivery()
    {
        //check how many battery sockets are empty
        //int emptySockets = 0;
        outputingPower = true;
        foreach (XRSocketInteractor socket in batterySockets)
        {
            if (!socket.selectTarget || !socket.selectTarget.tag.Equals("Battery"))
            {
                outputingPower = false;
                break;
            }
        }

        SetLight(outputingPower);

        Debug.Log("Light active: " + outputingPower);
    }

    private void SetLight(bool isEnabled)
    {
        lightSource.enabled = isEnabled;
        if (lightMeshRenderer)
        {
            if(isEnabled)
                lightMeshRenderer.material.EnableKeyword("_EMISSION");
            else
                lightMeshRenderer.material.DisableKeyword("_EMISSION");
        }
    }

    private void OnValidate()
    {
        if(lightSource.enabled != outputingPower)
            SetLight(outputingPower);
    }
}
