# EnemySpawner
An abstract class exposing the necessary methods for Spawners to be implemented. All Spawners should inherit from this class.

Includes the following methods:
- ReceiveTimePassed: Takes as an argument the time elapsed since the last frame from the GameManager of the Scene as a float.
- getStartingDirection: Returns a boolean value, true corresponding to right and false corresponding to left. This decides the direction to which the enemies spawned by this Spawn Point will initially face. 
- SpawnEnemy: Causes an enemy to be instantiated by this Spawn Point.

# FixedIntervalSingleTypeSpawner
A basic implementation of a Spawn Point, that instantiates a single type of enemy at regular intervals. This is meant to be an example as to how Spawners may be implemented.

A brief guide to how this Spawner was designed is given here:

The Spawner has 2 private fields:
- totalTimeElapsed, a float value tracking the amount of time passed
- random, a random number generator that will be used to decide the direction of spawned enemies

As well as 2 public fields:
- enemyToSpawn, a Prefab that will be cloned at regular intervals.
- intervalBetweenSpawns, the time in seconds between each successive spawning of an enemy.

Beyond that, it overrides the three methods of the abstract class EnemySpawner:
- ReceiveTimePassed: Adds the time elapsed according to the GameManager to the Spawner's totalTimeElapsed.
- getStartingDirection: Returns true or false (corresponding to right or left) with equal probability.
- SpawnEnemy: Instantiates a single enemy by cloning the enemyToSpawn field, with this Spawner's position, rotation and scale.
