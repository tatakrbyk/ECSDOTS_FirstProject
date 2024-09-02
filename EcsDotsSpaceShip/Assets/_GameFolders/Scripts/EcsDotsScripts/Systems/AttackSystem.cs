using SpaceShipEcsDots.Aspects;
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
    partial struct AttackSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            float deltaTime = SystemAPI.Time.DeltaTime;

            new AttackJob()
            {
                DeltaTime = deltaTime,
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            }.ScheduleParallel();
        }

}

    [BurstCompile]
    public partial struct AttackJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;

        [BurstCompile]
        private void Execute(Entity entity, ref AttackData attackData, in LocalTransform localTransform, [ChunkIndexInQuery] int sortkey)
        {
            attackData.CurrentFireTime += DeltaTime;
            if(attackData.CurrentFireTime > attackData.MaxFireTime)
            {
                attackData.CurrentFireTime = 0f;
                var projectileEntity = MyEntityCommandBuffer.Instantiate(sortkey, attackData.Projectile);

                MyEntityCommandBuffer.SetComponent(sortkey, projectileEntity, new LocalTransform()
                {
                    Position = localTransform.Position,
                    Rotation = quaternion.identity,
                    Scale = 1f

                });
            }
        }
    }
}

