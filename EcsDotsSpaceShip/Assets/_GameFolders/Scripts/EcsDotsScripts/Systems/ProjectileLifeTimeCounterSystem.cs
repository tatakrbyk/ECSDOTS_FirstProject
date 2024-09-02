using SpaceShipEcsDots.Aspects;
using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    partial struct ProjectileLifeTimeCounterSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new ProjectileLifeTimeCounterJob()
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();

        }
    }

    [BurstCompile]
    public partial struct ProjectileLifeTimeCounterJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ProjectileSelfDestroyAspect projectileSelfDestroyAspect)
        {
            projectileSelfDestroyAspect.BackCountingProcess(DeltaTime);
        }
    }
}
