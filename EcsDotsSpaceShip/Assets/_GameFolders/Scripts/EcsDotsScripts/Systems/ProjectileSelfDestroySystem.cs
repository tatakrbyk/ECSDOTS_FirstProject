using SpaceShipEcsDots.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]

    partial struct ProjectileSelfDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            new ProjectileSelfDestroyJob()
            {
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),

            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ProjectileSelfDestroyJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;
        [BurstCompile]
        private void Execute(ProjectileSelfDestroyAspect projectileSelfDestroyAspect, [ChunkIndexInQuery]int sortkey) // [chunk] paralel çalýþtýðý için indeximi veriyor
        {
            if(projectileSelfDestroyAspect.CanDestroy)
            {
                MyEntityCommandBuffer.DestroyEntity(sortkey, projectileSelfDestroyAspect.entity);
            }
        }
    }
}

