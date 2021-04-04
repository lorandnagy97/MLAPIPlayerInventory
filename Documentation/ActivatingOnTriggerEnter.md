## Enabling the tool when a player touches a giver object.

Here's a quick snippet showing how you can activate a tool from an OnTriggerEnter event.

The code that sets the player transform is specific to my player object's heirarchy, but otherwise this can be pasted into any gameobject capable of calling OnTriggerEnter and it will enable the tool.
A string toolName should be provided in the inspector for the gameObject that this script is assigned to. This will be what the script looks for in the player's inventory to perform the activation.

```C#
public class ToolGiver : NetworkBehaviour
{
    public string toolName;
    private void OnTriggerEnter(Collider other) {
        Transform player;
        GameObject inventory;
        if(other.transform.parent!=null && other.transform.parent.parent != null && other.transform.parent.parent.parent != null && other.transform.parent.parent.parent.tag == "Player") {
            player = other.transform.parent.parent.parent;
        } else if(other.transform.parent!=null && other.transform.parent.parent != null && other.transform.parent.parent.tag == "Player") {
            player = other.transform.parent.parent;
        } else return;
        inventory = player.Find("Player").Find("Inventory").gameObject;
        Tool tool = inventory.GetComponent(toolName) as Tool;
        if(tool.IsActive) return;
        tool.IsActive = true;
    }
}
```

Nothing else is needed, the Tool Interface's getters/setters take care of the rest.
