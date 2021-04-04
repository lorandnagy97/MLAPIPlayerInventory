# Enabling/Disabling the visibility of a tool's gameobject based on tool activation

Let's say that we have 5 different guns sitting dormant in the player's inventory; we don't want those to all be visible all the time.

So, there's a script to attach to any gameobject attached to the player's arms. You'll have to switch around the objects paths to match your own player's heirarchy, but otherwise it's pretty simple:

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

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
```

Just attach this to any tool gameobject you add. (with proper path to inventory, keeping in mind that the tool's relative paths will be different.)
In the inspector, once you've attached attached this script to the gameobject, make sure to enter the value for "Script Name" so that the tool knows if it's enabled or not.

