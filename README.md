# **VR Final Project**
Unity and VR

<br/><br/>
# **Game Concept**

## **Log line**
Reach the end of the level while surviving the attacks of the enemies.

## Game genre
First person (VR) Shooter-Horror game.

### Game Platforms
PC and VR (Oculus Quest 2)


<br/><br/>
# **TO-DO List**

## **TODO**
- Player collider and limitations
- Weapons
- Spatial UI (UI in world but not on GO)
- Diegetic UI (In GO in the game world)


## **DONE**
- XR setup
- Skybox

# **Code Guideline**

## **Collisions and interactions**
 
When a collision happen, and we want to look to interact, via script, with what we hit, instead of using *tags* and checking for each tags with the one we are looking for, we use **Interfaces** like **IHit** or **IThouch** with *TryGetComponent* and for each different object that can react to the sender, we implement the interface.

This allows us to keep the code more reusable, we can easily change the collision response for each type of object.

## **UI**

Keep the communication between gameplay scripts and UI less tight as possible, using **events** (Delegates or Actions), this will allow to easily change UI and Gameplay scripts in without affecting each other too much.

## **Managers**

Keep the managers:
- Game Manager
- Audio Manager
- Level Manager

as **Singletons**.