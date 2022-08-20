/** Attack an enemy in the same battle */
export function mutate(context) {
  const attacker = context.entities[0].customStatePublic[context.authorId];
  const enemy = context.entities[1].customStatePublic[context.authorId];
  enemy.hp -= attacker.strength;
}
