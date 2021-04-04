using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

namespace Tools
{
    public class PathTool : Tool
    {
        public GameObject PathObject;
        Vector3 GetPathLocation() {
            Vector3 newPos = transform.parent.parent.position;
            newPos.y = newPos.y - transform.parent.parent.GetComponent<BoxCollider>().size.y+2f;
            return newPos;
        }
        
        Quaternion GetPathRotation() {
            Quaternion newRot = transform.parent.rotation;
            newRot *= Quaternion.Euler(0, 180f, 0);
            return newRot;
        }

        [ServerRpc]
        void SubmitSpawnPathRequestServerRpc()
        {
            GameObject path = Instantiate(PathObject, GetPathLocation(), GetPathRotation());
            path.GetComponent<NetworkObject>().Spawn();
        }

        void SpawnPath() {
            SubmitSpawnPathRequestServerRpc();
        }
        
        public override void Use() {
            SpawnPath();
        }
    }
}
