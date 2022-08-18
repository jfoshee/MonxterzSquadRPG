/** Attack an enemy in the same battle */
export function mutate(context, enemy) {
  let entity = context.entity;
  let state = entity.customStatePublic[context.authorId];
  let strength = state.strength;
  enemy.hp -= strength;
}
