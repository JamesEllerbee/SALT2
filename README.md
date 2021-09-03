# SALT2
2d platformer project for STOP WAITING FOR GODOT game jam

## Legend
| Label | Meaning |
|-------|---------|
| NC    | Not confirmed, extra |
| C     | Confimed |

## Game Overview
---
### Game Concept
Meta slug-like, 2.5/3d platformer featuring n-degrees of shooting with locked camera.

### Genre
platformer, action, shoot em' up

### Look and Feel
- Cell shading
- Cartoon

### Objectives
- Move left, platforming
- Eliminating obtacles with projectiles
- Collection
- Make to the end of each level 

### Game Progression
1 boss level.

### Level Structure
**Start**
- Black, fades to level
- Mission start sequence
    - Mission Start alert, 
    - fades out 
    - voice [NC] mission start, go. 
    - Mission start sound
**Design**
- Move from left to right
    - Blocked from moving left after moving the camera
- Sloping up / down
- Fall out of level, repsawn to last valid position
**End**
- Mission complete
- fade to black

### Puzzle Structure
**Boss battle**
- Impassble wall
- After defeating
    - Move past to end level


## Mechanics
---
### Rules
- Auto-scroll [NC]
- Shooting modifiers

### Scene sequence
Make use of 2d and 3d nodes

### Physcics
Godot Kinematicbody
- Feel like metal slug
- Heavy reuse of existing solutions

### Objects
| Description        | Type        | Modifier          |
|--------------------|-------------|-------------------|
| Cucumber seeds     | Collectable | Increase score    |
| Cucumber           | Collectable | Increase score    |
| Cucumber basket    | Collectable | Increase score    |
| Tank [NC]          | Vehicle     | Shooting modifier |
| Slug Shotgun       | Weapon      | Shooting modifier |
| Heavy machine [NC] | Weapon      | Shooting modifier |
| Spread shot [NC]   | Weapon      | Shooting modifier |


### Actions and Controls
- Keyboard
- Gamepad support (Xbox)

| Action                 | Keyboard   | Gamepad (Xbox)         |
| -----------------------|------------|------------------------|
| Aim up                 | W          | Analog +Y, D-pad up    |
| Crouch / Aim lower     | S          | Analog -Y, D-pad down  |
| Move right             | D          | Analog -X, D-pad right |  
| Move left              | A          | Analog +X, D-pad left  |
| Shoot (hold)           | J          | B, Right trigger       |
| Melee [C] / Parry [NC] | K          | X, Left trigger        |
| Jump                   | Space bar  | A                      |

### Combat 
- 8 way shooting
    - Starting weapon slug shotgun
- every weapon auto

### Game Options
- Volume
- Windows / full screen

### Persistence
- Local Leaderboard [NC]

### Cheats and Easter Eggs
- Environment reacts to projectiles [NC]

## Story and Narrative
---
Cucumber jeans seed and salt marine are pissed.

## Game World
---

### Areas
**Beach**
- Giant cucumber 
    - in far back ground
- Smaller pieces of cumber
    - back ground
    - fore ground
- Paralaxing
    - 2d with a background controller?
    - 3d?

## Characters
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
- Picking up weapons

Animations
- Hurt
- Running

## Enemies
---
### Frog
Different types
- ground basic
    - runs around and shoots
- ground rusher
    - runs at you and attempts to melee
- flying rusher
    - dive bomb
- flying basic
    - flies around and shots
- vehicle [NC]
    - rides and shoots

## Levels
---
**Level 1 - Seed day**

## Interface
---
### Main menu
- Play
- Options [NC]
- Quit

### Pausing
- Freeze
- Grey screen
- Pause alter
- Option for quiting [NC]

### Score tracker
- TODO define score incrementing

### Health bar
- 3 Ticket (equivalent to hearts)
- Getting hit removes a Ticket
- On losing all Ticket
    - Die, Respawn in same spot
    - Decrement score by fixed amount and reset number of ticket
    - Invulnerbility frames 
        - 1 seconds after taking damage
        - 3 seconds after respawning
- Melee elimination gives back ticket
- Some object may give tickets back

## Audio, music, sound effects
---

## Help System
---
### Start of level
- Controls drawn into