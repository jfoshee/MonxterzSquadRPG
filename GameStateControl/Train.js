/** Attack an enemy in the same battle */
export function mutate(context, attribute, seconds) {
  if (context.entities.length != 1) {
    throw Error('Train function requires 1 Entity targets: trainee');
  }
  const traineeEntity = context.entities[0];
  const trainee = traineeEntity.customStatePublic[context.authorId];
  if (traineeEntity.systemState.ownerId != context.userId) {
    throw Error('The trainee character does not belong to the current Player. You cannot train another player\'s character.');
  }
  if (trainee.hp <= 0) {
    throw Error('The character cannot train when dead.');
  }
  // Convert milliseconds to seconds
  var start = Math.round(Date.now() / 1000);
  trainee.trainingStart = start;
  trainee.trainingEnd = start + seconds;
  trainee.trainingAttribute = attribute;
  trainee.isTraining = true;
}
