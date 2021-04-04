using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

namespace Tools {

    public interface ToolI
    {
        bool IsActive {get; set;}
        int InputMouseKey {get; set;}
        GameObject ToolModel {get;}
    }

    public class Tool : NetworkBehaviour, ToolI {
        public NetworkVariableBool active;
        public bool isActive;
        public int inputMouseKey;
        public GameObject toolModel;

        public NetworkVariableULong toolModelNetId;
        public bool IsActive {
            get {
                return isActive;
            }
            set {
                if(value == true) {
                    foreach(Tool tool in transform.gameObject.GetComponents<Tool>()) {
                        if(tool.IsActive && tool.InputMouseKey == inputMouseKey) {
                            tool.active.Value = false;
                            tool.IsActive = false;
                            tool.Deactivate();
                        }
                    }
                }
                active.Value = value;
                isActive = value;
                Activate();
            }
        }

        public int InputMouseKey {
            get {
                return inputMouseKey;
            }
            set {
                inputMouseKey = value;
            }
        }

        public GameObject ToolModel {
            get {
                return null;
            }
        }

        public Tool() {
            this.isActive = false;
            this.inputMouseKey = 0;
            this.toolModel = null;
        }

        public virtual void Activate() {
            return;
        }

        public virtual void Deactivate() {
            return;
        }

        public virtual void Use()
        {
            return;
        }

        void Update() {
            isActive = active.Value;
            if(inputMouseKey == 0) {
                if(IsLocalPlayer && Input.GetKeyDown)
            }
        }
    }

    public class Tools : NetworkBehaviour
    {
        public Tool[] toolsList;
        void Start()
        {     
            toolsList = transform.gameObject.GetComponents<Tool>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButton(0) && IsOwner) {
                foreach(Tool tool in toolsList) {
                    if(tool.InputMouseKey == 0 && tool.IsActive) {
                        tool.Use();
                    }
                }
            }
            if(Input.GetMouseButton(1) && IsOwner) {
                foreach(Tool tool in toolsList) {
                    if(tool.InputMouseKey == 1 && tool.IsActive) {
                        tool.Use();
                    }
                }
            }
        }
    }
}
