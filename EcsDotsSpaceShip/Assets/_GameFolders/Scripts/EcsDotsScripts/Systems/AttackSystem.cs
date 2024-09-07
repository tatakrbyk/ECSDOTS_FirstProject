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
    public partial struct AttackSystem : ISystem
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
            var deltaTime = SystemAPI.Time.DeltaTime;

            new AttackJob()
            {
                DeltaTime = deltaTime,
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct AttackJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;

        [BurstCompile]
        private void Execute(Entity entity, ref AttackData attackData, in LocalTransform localTransform, [ChunkIndexInQuery] int sortKey)
        {
            attackData.CurrentFireTime += DeltaTime;
            if (attackData.CurrentFireTime > attackData.MaxFireTime)
            {
                attackData.CurrentFireTime = 0f;
                attackData.CanChange = true;
                var projectileEntity = MyEntityCommandBuffer.Instantiate(sortKey, attackData.Projectile);
                MyEntityCommandBuffer.SetComponent(sortKey, projectileEntity, new LocalTransform()
                {
                    Position = localTransform.Position,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
        }
    }
}