/** Initializes a new Character */
function initialize(context) {
  let entity = context.entity;
  entity.displayName = 'New Character';
  let state = entity.customStatePublic[context.authorId];
  state.type = 'Character';
  state.hp = 100;
  state.xp = 0;
  state.strength = 1;
}
