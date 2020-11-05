# Project Expodus

## The project is made by [jeyasurya](https://github.com/vjs22334/Project_Exodus) and forked by [TGL](https://github.com/theGameLearner)

The orignal project's last commit at the time is [this](https://github.com/vjs22334/Project_Exodus/commit/507e8e22276ce1b48ca4c2467a253aa20a5ddea4).<br>
The idea is to convert this project using the [SOLID](https://en.wikipedia.org/wiki/SOLID) principle.<br>

### Planned changes 01

#### ScriptableObjectsDataContainers folder
##### &emsp; PlayerAbilityBase.cs
&emsp; &emsp; Perfect.
##### &emsp; SheildAbility.cs
- [X] Needs to handle ability use when data reaches zero
##### &emsp; FloatData.cs
&emsp; &emsp; Perfect.

#### UI folder
##### &emsp; ArrowPointer.cs
- [ ] Create a class that handles all interactions with UI Camera
- [ ] function that takes a transform in world space and determines its on screen position
- [ ] in UICamera make a function that takes in two transforms and returns the direction from one to another
- [ ] ArrowPointers should all be in a pool which handles which pointer is visible and which is not.

#### Player folder
##### &emsp; Wings.cs
- [ ] using WaitForSecondsRealtime() to be avoided
- [ ] Coroutine name (r) to be improved for clarity
- [ ] Change toggleCollider() so it can take input on new state
- [ ] remove GameManager Dependency

##### &emsp; fuselage.cs
- [ ] remove GameManager Dependency
- [ ] The GameManager should control ending of game not the fuselage script
- [ ] remove Player Dependency

##### &emsp; Player.cs
- [ ] Make a different FloatData scriptable object to store player's current fuel
- [ ] use RequireComponent attribute on scripts when you have GetComponent dependencies(animator)
- [-] Edit AnimationCallbackMethodMethod() functionality
- [ ] Create a seperate class for InputHandler
- [ ] SheildAbility.cs does not have a override defined for OnAbilityKey() so whhy are we using it?
- [ ] Implement 'Pause', 'Shield' and 'Flip' on Android platform
- [ ] In FixedUpdate() use Vector3.back in place of Vector3.forward so functionality is easieer to understand

#### Pickups folder
##### &emsp; RepairScript.cs
- [ ] use object pool for repair kit
- [ ] remove GameManager Dependency

#### missiles_and_enemies folder
##### &emsp; missleSpawner.cs
- [ ] not in use, can be deleted

#### &emsp; missileLauncher.cs
 - [ ] Name all Coroutines
 - [ ] Use Object pool to store all Missiles
 - [ ] don't use time as variable name, it is confusing
 - [ ] if using a progress bar, use that to handle spawnning missiles as well
 - [ ] missile spawn direction should be consistent to the enemy firing it
 
#### &emsp; Missile.cs
 - [ ] Use two scrptable objects for missile speed and enemy speed. That way one can be edited without efecting other
 - [ ] all function calls inside collission and trigger do same thing except when hitting reflector, make it a single function and redirect
 
#### &emsp; enemyShip.cs
- [ ] Enemy ships to be handled in a pool
- [ ] Improve FixedUpdate flow using states
- [ ] Make a barManager which will manage all bars in a pool rather than so many bars being handled individually

#### scripts folder
#### &emsp; cameraFollow.cs
&emsp; &emsp; Perfect.

#### &emsp; FollowUV.cs
- [ ] Add RequiredComponent attribute

#### &emsp; FuelScript.cs
- [ ] remove dependency on Player script, check for an interface and handle fuel picking capablity on it

#### &emsp; GameManager.cs
- [ ] do not handle enemy and it's missile launcher. Let the enemy handle it's own missiles
- [ ] Use index of scene to reload current game scene rather than name

#### &emsp; UIManager.cs
- [ ] Add label for Ability guage (text for user understanding)
- [ ] Add UpdateAbilityGauge() call to Update() method.

#### Miscellaneous edits
- pointer for ship, pointer for missiles and pointer for repair all use same obect with different UI. Lets make a single configurable prefab
- Do not use 'FindGameObjectWithTag' so many times
- if the player collides with the ship and dies with the shields up, the shield should also get destroyed

## 25 - 10 - 2020
- reset the project to the state received in
- upgrade the project to unity version [2019.4.12f1 [LTS]](https://unity3d.com/get-unity/download/archive)
- created a GenericSingletonMonobehaviour generic class that can be used to define and lock singletons in monobehaviour 
- used a Finite State Machie asset shared among friends(no idea where it originated) to create a new FSM that can be used here


### Pending Tasks
- Test the new FSM system created.
- Create demo classes for both machine and a initial state.
- Create a centralised Messenger or Event System that can fire and listen for events with subscriping capablities


## 26 - 10 - 2020
- created demo for using the FSM
- used ['Advanced CSharp Messenger'](http://wiki.unity3d.com/index.php/Advanced_CSharp_Messenger) by Ilya Suzdalnitski for a messenger system.
- create a centralised class to store names of all EventNames that we want to fire as well as listen for.
- Setup done, we can begin the SOLID principle
- SheildAbility.cs adding checks for charge being Zero
- created UiCameraHandler.cs to do all UI related Calculations


## 05 - 10 - 2020
- old push