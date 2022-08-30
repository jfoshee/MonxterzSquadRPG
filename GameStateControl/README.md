
## Need from Game State System

- CLI
  - Init
    - creates a new monxterz.proj.json
    - creates the unit test project
  - New script
    - mutator
    - initializer
  - Bootstrap the game owner entity
    - because it needs to be created by a different user (e.g. system)
- Game test harness
  - Publish project when tests startup (not per test)
  - game.State(xyz) return falsy for undefined state
