/** Check status and, if applicable, complete character recovery and training */
export function mutate(context) {
  if (context.entities.length != 1) {
    throw Error('CheckStatus function requires 1 Entity targets: trainee');
  }
  const traineeEntity = context.entities[0];
  if (traineeEntity.systemState.ownerId != context.userId) {
    throw Error('The trainee character does not belong to the current Player. You cannot train another player\'s character.');
  }
  const trainee = traineeEntity.customStatePublic[context.authorId];
  if (trainee.hp <= 0) {
    throw Error('The character is dead and cannot complete training.');
  }
  // Convert milliseconds to seconds
  const now = Math.round(Date.now() / 1000);
  if (trainee.trainingEnd && trainee.trainingEnd <= now) {
    // Training is complete
    trainee.trainingStart = 
    trainee.trainingEnd = 
    trainee.trainingAttribute = null;
    trainee.isTraining = false;
    // TODO: How much strength to add? training rate...
    trainee.strength += 1;
  }
  if (trainee.recoveringEnd && trainee.recoveringEnd <= now) {
    trainee.recoveringStart =
    trainee.recoveringEnd = null;
    trainee.isRecovering = false;
  }
}
