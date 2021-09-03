# SALT2
2d run-n-gun action shooter project for STOP WAITING FOR GODOT game jam

## Legend
| Label | Meaning |
|-------|---------|
| L    | Low priorty|
| M     | Medium priorty|
| H     | High priorty|
| I     | Implemented (Done)|


## Game Overview
---
### Game Concept
Meta slug-like, 2.5/3d platformer featuring n-degrees of shooting with locked camera.

### Genre
Run-n-gun action shooter 

### Look and Feel
- 3D Models
- Cell shading
- Cartoon
- Feels similiar to Metal Slug

### Objectives
- Move right, basic platforming
- Eliminating obtacles with projectiles
- Collection (see collectables section)
- Make to the end of each level 

### Game Progression
1 boss level.

### Level Structure
**Start** [M]
- Black, fades to level
- Mission start sequence
    - Mission Start alert, 
    - fades out 
    - voice [L] mission start, go. 
    - Mission start sound
**Design** [H]
- Move from left to right
    - Blocked from moving left after moving the camera
- Sloping up / down
- Jumping over obstacles to advance
- Crouching under obstacles to advace
- Gates that can only be opened by meleeing it
- Gate that can only be opened by shooting a target
- Introduce enemies in a simple isolated manner, getting more complicated and advance encounters as the player progresses
- Fall out of level, repsawn to last valid position 
    -Do we want pitfalls?
**End**
- Mission complete
- fade to black

### Puzzle Structure
**Boss battle**
- Impassble wall
- After defeating
    - Move past to end level


## Mechanics [H]
---
### Rules
- Fixed-Camera on player (cannot move to left after advancing)
- Shooting modifiers

### Scene sequence
Make use of 2d and 3d nodes


### Objects [H]
| Description        | Type        | Modifier          |
|--------------------|-------------|-------------------|
| Cucumber seeds     | Collectable | Increase score    |
| Cucumber           | Collectable | Increase score    |
| Cucumber basket    | Collectable | Increase score    |
| Tank [L]          | Vehicle     | Shooting modifier |
| Slug Shotgun       | Weapon      | Shooting modifier |
| Heavy machine [L] | Weapon      | Shooting modifier |
| Spread shot [L]   | Weapon      | Shooting modifier |


### Actions and Controls [H]
- Keyboard
- Gamepad support (Xbox)

| Action                 | Keyboard   | Gamepad (Xbox)         |
| -----------------------|------------|------------------------|
| Aim up                 | W          | Analog +Y, D-pad up    |
| Crouch / Aim lower     | S          | Analog -Y, D-pad down  |
| Move right             | D          | Analog -X, D-pad right |  
| Move left              | A          | Analog +X, D-pad left  |
| Shoot (hold)           | J          | B, Right trigger       |
| Melee / Parry [L] | K          | X, Left trigger        |
| Jump                   | Space bar  | A                      |

### Combat [H]
- 8 way shooting
    - Starting weapon slug shotgun
- every weapon auto

### Game Options [M]
- Volume
- Windows / full screen

## Story and Narrative [L]
---
Cucumber jeans seed and salt marine are pissed.

## Game World
---

### Areas [H]
**Beach**
- Giant cucumber 
    - in far back ground
- Smaller pieces of cumber
    - back ground
    - fore ground
- Paralaxing
    - 2d with a background controller?
    - 3d?

## Characters [H]
---
### Salt Guy
**Main player character**

Abilities
- One movement speed
- Melee
- Crouch
- Shoot
    - Slug shotgun
        - Slugs are the projectiles
- Jump
    - Single
    - Can shoot while jumping
- Picking up weapons

Animations
- Hurt
- Running
- Jumping
- Crouching

## Enemies
---
### Frog
Different types
- ground basic [H]
    - runs around and shoots
- ground rusher [H]
    - runs at you and attempts to melee
- flying rusher [L]
    - dive bomb
- flying basic [L]
    - flies around and shots
- vehicle [L]
    - rides and shoots

## Levels
---
**Level 1 - Seed day**

## Interface
---
### Main menu [H]
- Play
- Options [M]
    - Full Screen vs Windowed
    - Volume
- View Leaderboard [L]
- Quit (Exits Application)


### Pause Menu [M]
- Freeze's game
- Grey screen
- Pause alter (Resume, restores original brightness)
- Options overlay [L]
    - Full Screen vs Windowed
    - Volume
- Quit (Sends player back to main menu)


### Score tracker [L]
- TODO define score incrementing

### Health bar [H]
- 3 Ticket (equivalent to hearts)
- Getting hit removes a Ticket
- On losing all Ticket
    - Die, Respawn in same spot 
    - Decrement score by fixed amount and reset number of ticket
    - Invulnerbility frames 
        - 1 seconds after taking damage
        - 3 seconds after respawning
- Melee elimination gives back ticket (Why? Is Meleeing that difficult?)
- Some objects may give tickets back

## Audio, music, sound effects [M]
---TODO Add something  ---

## Physics [H]
---
### Godot Kinematicbody
    - Feel like metal slug
    - Heavy reuse of existing solutions

## Help System [L]
---
### Start of level
- Controls drawn into environment (A la binding of isaac)

### Persistence
- Local Leaderboard [L]

### Cheats and Easter Eggs
- Environment reacts to projectiles [L]


