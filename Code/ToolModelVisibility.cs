using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

namespace Tools
{
    public class ToolModelVisibility : NetworkBehaviour
    {
        public string scriptName;
        private Tool tool;

        void Start() {
            tool = transform.parent.parent.parent.Find("Inventory").GetComponent(scriptName) as Tool;
        }
        void SetIsVisible(bool visibility) {
            Renderer[] children;
            children = GetComponentsInChildren<Renderer>();
            foreach(Renderer rend in children) {
                rend.enabled = visibility;
            }
        }

        void Update() {
            SetIsVisible(tool.IsActive);
        }
    }
}
