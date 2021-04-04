# MLAPI Player Inventory

I've noticed that both myself and others have been struggling when it comes to utilizing MLAPI's spawning functionality to create tools (guns, etc), due to various errors having to do with MLAPI's inability to handle nested networkobjects. In some cases it's rather important to have the tool be nested inside the playerobject (for example positioning and animations), but calling NetworkObject.Spawn() will not allow this.

***Quick disclaimer before we continue: This is in the very early stages of development and has not been fully explored, I'm sure there'll be some roadblocks ahead. However, this is the farthest I've managed to come regarding spawning network-ready tools for the player, so hopefully it helps others as well.***

_Links to code examples and more details are at the bottom of this document._


<br />
<br />


<h1 align="center">
  Concept
</h1>

In order to circumvent the need for spawning these tools and all the trouble that comes with it, I've been able to add an "Inventory" to the Player Object that contains all the tools that the player may run into during the course of the game, waiting in a dormant inactive state controlled by a NetworkVariableBool until activated by an event (in my case, OnTriggerEnter calls). This Inventory object only contains script components, which are responsible for knowing whether or not they are active, and performing the correct action based on the active tool.

However, some tools, like a gun, would also need a GameObject associated to represent the physical aspect of the tool and perform things like shooting. To achieve this, I've placed and positioned the prefabs that each tool script requires onto the PlayerPrefab's arm manually using the editor. The GameObjects in the arm store String parameters with the names of the script components they belong to, and can perform checks against the script using the reference and their relative path to the player's inventory to see what they should be doing.

By doing all this, and a few more little tweaks, we get to piggyback on the PlayerObject's network properties, doing away with the need for ownership and spawning.


### Here is what my PlayerObject prefab looks like, hopefully it helps illuminate the above.
![image](https://user-images.githubusercontent.com/56968310/113514217-0a837900-9576-11eb-9770-996deb05e2df.png)


_The Inventory object is saved as a prefab for organization and ease-of-access reasons, and the pistol object is a prefab as well, however they are not spawned separately from the PlayerObject._

<br /><br /><br />

### [Tools Namespace, Tools Class, ToolI(Tool Interface), Tool Base Class](./Documentation/Tools.md)
<br /><br />
### [Activating tool using OnTriggerEnter](./Documentation/ActivatingOnTriggerEnter.md)
<br /><br />
### [Tool Visibility Script](./Documentation/ToolVisibility.md)
<br /><br />
### [Example Tool Without GameObject](./Code/PathTool.cs)
<br /><br />
### [Example Tool With GameObject](./Code/PistolTool.cs)
