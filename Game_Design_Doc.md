# Untitled Explorer Game

*Game Design Document · Setting: a space ship*

---

## 1. Required Features

### Movement System

Forward, backward, turn left/right, strafe left/right, jump, crouch accompanied by player graphics (shadows - first person view). 

### World Story and narrative 

Interacting with the environment reveals more of the story progressively. Endings vary with the amount of information collected, or the narrative branches depending on player choices.

1. Player has amnesia after crash landing 
2. Has dialogue boxes that update story background information
3. Collecting resources unlocks memories
4. Ending 1: Getting all resources - repair ship and go home
5. Ending 2: Ship explodes (how does this work? is it a time limit thing?)

### Environment 1: Inside the Ship

Various locations where the player can repair ship systems through puzzles that require resources. When all systems are fixed, the player wins, and the story tells of their return to Earth.

- Repair panel room
- Oxygen tank room repair
- Passageways for exploration and storytelling

### Environment 2: The Outside World

Several locations where the player can collect resources. Creatures roam around, and some are aggressive towards the player (health + damage system).

- Different terrains with varying levels of parkour tasks
- Enemies in various locations that affect player health but aren't inherently aggressive

### Procedural Generation and Photography

Creature properties and/or spawn locations are procedurally generated. The player can take pictures of the creatures, which are saved in an album with a short description. This album is the log the player returns to Earth with.

### Oxygen (Time-Bound Gameplay)

The player has a limited oxygen supply, and loses when it runs out. Oxygen tanks placed around the ship extend the game time if collected.

### User Interface

- Game start menu
- Game end menu
- Pause menu
- Settings menu: audio, mouse sensitivity, key bindings

### HUD

Displays player health, inventory and controls.
- Inventory: shows resources to be collected and the ones they already have
- Objectives: explains the game's tasks in the form of a checklist

### Other

- Post-processing effects
- Checkpoints
- Cutscene introduction for each environment, and for the ending
- Background atmospheric music, plus sound effects for player movement, creatures and environment interactions
- 3D models, animations and particle effects for the player character, creatures and environmental objects

---

## 2. Additional Features

- Repair animation
- Multiple ship repair points

---

## 3. Core Features

### Movement

Walking, jumping, crouching and strafing.

### Environment

Ship environment and outside world, with ship repairs.

### Characters

- Damage system (oxygen / enemies) with health bar
- NPCs
- HUD

### UI

- Damage system (oxygen / enemies) with health bar
- HUD
