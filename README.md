# DartTime

# Mqtt Requests

### Base topic

```
string baseTopic = 'x6et/q8zl/'
```

Description | Topic | Implemented
--- | --- | ---
A game was started | game/[Username]|✔️
A target was hit | game/[Username]/hit|✔️
A target was missed | game/[Username]/miss|✔️
The current score of a player | game/[Username]/score/total|✔️
Latest score addition or subtraction | game/[Username]/score/latest|✔️
Latest score addition or subtraction | game/[Username]/score/multiplier|✔️
The current amount of darts a player has left |game/[Username]/darts|✔️
The current amount of time a player has left |game/[Username]/time|✔️
The current difficulty |game/[Username]/difficulty|✔️
# ToDo

Name | Description | Implemented
--- | --- | ---
Base Gameplay | Being able to shot darts and get points by hitting targets. | ✔️
Score Mulitplier | Getting More Points for consecutive hit targets | ✔️
Integrated UI | Having the Score, Time and Dart Left UI integrated in the game world | ✔️
Light Indicator | Change the light when a dart hits or misses a target | ✔️
Real Dart Display | Being able to phyisically see the darts that are left in the game | ✔️
Difficulties | Have multiple Difficulties that  each spawn more Obstacles | ✔️
