# Mqtt-AimTrainer

# Mqtt Requests

### Base topic

To make the whole Mqtt Part unique all topics will start with `x6et/q8zl/`
#### Implemented ✔️
#### Not Implemented ❌

Description | Topic
--- | ---
A game was started | game/[Username]❌
A target was hit | game/[Username]/hit✔️
A target was missded | game/[Username]/miss✔️
The current score of a player | game/[Username]/score/total❌
Latest score addition or subtraction | game/[Username]/score/latest❌
The current amount of darts a player has left |game/[Username]/darts❌
The current amount of time a player has left |game/[Username]/time❌

