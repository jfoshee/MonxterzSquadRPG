/** Attack an enemy in the same battle */
export function mutate(context) {
  if (context.entities.length != 2) {
    throw Error('Attack function requires 2 Entity targets: attacker, defender');
  }
  const attackerEntity = context.entities[0];
  const attacker = attackerEntity.customStatePublic[context.authorId];
  const defender = context.entities[1].customStatePublic[context.authorId];
  if (attackerEntity.systemState.ownerId != context.userId) {
    throw Error('The attacker character does not belong to the current Player. You cannot attack with another player\'s character.');
  }
  if (attacker.hp <= 0) {
    throw Error('The character cannot attack when dead.');
  }
  defender.hp -= attacker.strength;
  if (defender.hp < 0) {
    defender.hp = 0;
  }
}
