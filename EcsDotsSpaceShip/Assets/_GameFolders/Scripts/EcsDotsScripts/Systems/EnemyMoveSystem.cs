using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]    
    partial struct EnemyMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new EnemyMoveJob()
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }

    }

    [BurstCompile]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(Entity entity, ref LocalTransform localTransform, ref EnemyMoveData enemyMoveData, in EnemyMoveTargetData enemyMoveTargetData)
        {
            if (enemyMoveData.canPassNextTarget) return;

            if (math.distance(enemyMoveTargetData.Target, localTransform.Position) < 0.1f)
            {
                enemyMoveData.canPassNextTarget = true;
                return;
            }
            var direction = math.normalize(enemyMoveTargetData.Target - localTransform.Position);

            localTransform.Position += DeltaTime * enemyMoveData.MoveSpeed * direction;
        }   
        
    }

}
