/** Attack an enemy in the same battle */
export function mutate(context) {
  const attacker = context.entities[0].customStatePublic[context.authorId];
  const defender = context.entities[1].customStatePublic[context.authorId];
  defender.hp -= attacker.strength;
}
