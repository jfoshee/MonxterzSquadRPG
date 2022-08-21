/** Attack an enemy in the same battle */
export function mutate(context) {
  if (context.entities.length != 2) {
    throw Error('Attack function requires 2 Entity targets: attacker, defender');
  }
  const attacker = context.entities[0].customStatePublic[context.authorId];
  const defender = context.entities[1].customStatePublic[context.authorId];
  if (attacker.hp <= 0) {
    throw Error('The character cannot attack when dead.');
  }
  defender.hp -= attacker.strength;
  if (defender.hp < 0) {
    defender.hp = 0;
  }
}
