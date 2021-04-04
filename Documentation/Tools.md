# Tools.cs
#### [You can find the full code for this file here.](../Code/Tools.cs)


The tools script is a bit jumbled as I wasn't really sure what I was going for, so apologies for that, but here goes:
<br /><br /><br />


This script is attached directly onto the Inventory prefab. The "active" class defined within the namespace is "Tools" (lazy naming, sorry) 

It's a short and sweet class that simply creates an array of all the tool scripts attached to the inventory, and routes the proper input to whichever tool happens to be active:


```C#
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
```

### Here's what the attached tools look like in the editor
![image](https://user-images.githubusercontent.com/56968310/113515700-bb8e1180-957e-11eb-8603-4e50829d30c7.png)

<br />
<br />
<br />
<br />

The Tool base class is defined in the same script file right above the Tools class. The code for this is a bit longer, so let's take it step-by-step.
First, there is a Tool Interface (ToolI) defined which contains properties for getting/setting the tool's active status, as well as which mouse click the tool is looking for regarding input.

The Tool base class itself then starts out by declaring the following:

```C#
        public NetworkVariableBool active;
        public bool isActive;
```
The two declarations above work so that the interface can return a simple bool, further simplifying interaction with the tool.


Activation/Deactivation is done below:
```C#
        public bool IsActive {
            get {
                return isActive;
            }
            set {
                if(value == true) {
                    foreach(Tool tool in transform.gameObject.GetComponents<Tool>()) {
                        // This clause deactivates a tool if it was previously active and using the same mouse input as the newly activated tool.
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
```

To make sure that the network shares the same value for the tool's active status, there's a one-liner update function, where isActive is the primitive bool and active is the networkvariable:
```C#
        void Update() {
            isActive = active.Value;
        }
```

The class also has overridable Activate and Deactivate methods that are called if the tool need to run any logic when the activation is changed (i.e. changing cursor lockstate and visibility when equipping a gun).
