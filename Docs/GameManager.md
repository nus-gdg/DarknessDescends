# GameManager

The GameManager class keeps track of the following statistics:
- The player's score
- The number of enemies defeated
- The time elapsed since the start of the game
- The state of the game e.g. if the player is dead or not

In addition, it exposes the following fields in the Unity Editor:
- scoreDisplay, in charge of displaying the player's score
- timeDisplay, in charge of displaying the time elapsed
- endgameDisplay, a Game Over message for the player when the game has ended
- SpawnerList, a list that should contain references to each Spawn Point in the current scene

The method enemyDies should be called when an enemy is killed by the player. It accepts as a single argument an _int_ value corresponding to the score earned from 
killing the enemy. The total score is incremented by the same amount.

The method playerCharacterDies should be called when the player dies. The GameManager will handle the game's state accordingly.

The private method SendMessagesToSpawnPoints will be called at each frame, and will update the time elapsed of each Spawn Point.