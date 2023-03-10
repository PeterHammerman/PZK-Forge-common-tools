Script controls on load all defined entities (especially enemies) in all project scenes
If enemy is encountered it will put into entity list with cooldown
while entity is on the list and still has cooldown, it wont appear in specific scene

Good to provide respawn

Script can be used as (enemy controller, resource appearance and restock)

each item must have different ID
use same ID for grouping

-ScenesEntityController must be placed in gameObject - its instance appear in all scenes
-EntityOnLoad must be placed in item/enemy gameobject to control its activation