using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerNetworking : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();

    [Client]
    private void Update()
    {
        if (!hasAuthority) { return; };


         if (Input.GetKeyDown(KeyCode.Space))
         {
            transform.Translate(movement);
         }
    }

}
