# VGM

This is test. This project is created in Unity v5.5

### Task ###
Create a simple project in Unity using C# with the following requirements:

### GamePlay ###
In the scene, the player object has a starting health, moves at a constant speed, starting at (0, 0, 0), towards a list of destination points (P1, P2, P3 and so on).
The health and destination points can be changed by a designer.
Spawn 100 objects inside the bounding box of the destinations points at random positions that can collide with the player.
On arrival at each destination point, the player should begin movement towards the next point.
While moving If a collision is detected decrement the health by 1 and re-spawn the collided object at another random position and play a sound effect at that location.
If the health reaches zero before player reaches the last destination point show a Game Lost Screen.
Show a game win screen when the player reaches the last destination point. 

### Screens ###
Show Health bar.
GameLost And GameWin Screen should have a Game Lost or Game Won text and should have a replay button which restarts the game.

### Solution ###

Everything important is in **Assets/_Scripts/GameManager.cs**. 

**Health** and **Destination points** can be changed in runtime from inspector or scene view.
