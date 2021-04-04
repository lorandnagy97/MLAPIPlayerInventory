using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

namespace Tools
{
    public class PistolTool : Tool
    {

        public string toolModelName;

        private Transform gunModel;
        private ParticleSystem bulletParticleSystem;
        private ParticleSystem.EmissionModule em;
        NetworkVariableBool shooting = new NetworkVariableBool(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.OwnerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        }, false);

        void Start() {
            gunModel = transform.parent.Find("Arms").Find("Arm").Find(toolModelName);
            bulletParticleSystem = gunModel.Find("BulletParticleSystem").gameObject.GetComponent<ParticleSystem>();
            em = bulletParticleSystem.emission;
        }

        public override void Activate()
        {
            if(IsLocalPlayer) {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        public override void Deactivate()
        {
            if(IsLocalPlayer) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        bool CheckShooting() {
            return Input.GetMouseButton(0);
        }

        void Update(){
            if(IsLocalPlayer && IsActive)
            shooting.Value = CheckShooting();
            em.rateOverTime = shooting.Value == true ? 10f : 0f;
        }
    }
}
