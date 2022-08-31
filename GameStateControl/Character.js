/** Initializes a new Character */
function initialize(context) {
  const entity = context.entity;
  entity.displayName = 'New Character';
  const state = entity.customStatePublic[context.authorId];
  state.type = 'Character';
  state.hp = 100;
  state.xp = 0;
  state.strength = 1;
  state.isTraining = false;
  state.isRecovering = false;
  state.recoveryTime = 10;
}
