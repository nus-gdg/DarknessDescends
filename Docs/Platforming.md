# PlatformEdgeDemarcator
A script to be attached to GameObjects demarcating the ends of a Platform. Each of these objects should also have a component Trigger Collider.

# InteractsWithPlatformEdgeDemarcator
An interface to be implemented by enemy movement controller scripts. Contains a single method, InteractWithPlatformEdgeDemarcator that will be called by a Platform Edge Demarcator when a GameObject with a script implementing this method enters its collider. See the script "PatrollingEnemyMovement" for an example.