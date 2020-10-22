using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();

    [Client]
    private void Update()
    {
        if (!hasAuthority) { return; }


        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        transform.Translate(movement);
        //CmdMove();
    }
    [Command]
    private void CmdMove()
    {
        transform.Translate(movement);

    }
    [ClientRpc]
    private void RpcMove()
    {
        transform.Translate(movement);
    }
}
