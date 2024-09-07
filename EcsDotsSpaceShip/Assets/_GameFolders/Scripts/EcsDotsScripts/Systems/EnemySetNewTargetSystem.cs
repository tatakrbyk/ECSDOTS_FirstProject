using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemyMoveSystem))]
    partial struct EnemySetNewTargetSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new EnemySetNewTargetJob()
            {

            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct EnemySetNewTargetJob : IJobEntity
    {
        [BurstCompile]
        private void Execute(Entity entity, ref EnemyMoveData enemyMoveData, ref EnemyMoveTargetData enemyMoveTargetData, in EnemyPathData enemyPathData, ref DestroyData destroyData)
        {
            if (!enemyMoveData.canPassNextTarget || destroyData.IsDestroy) return;
            enemyMoveTargetData.NextTargetIndex++;

            if (enemyMoveTargetData.NextTargetIndex >= enemyMoveTargetData.MaxTargetIndex)
            {
                destroyData.IsDestroy = true;
            }
            else
            {
                enemyMoveTargetData.Target = enemyPathData.BlobValueReference.Value.Values[enemyMoveTargetData.NextTargetIndex];
                enemyMoveData.canPassNextTarget = false;
            }

        }
    }
}
