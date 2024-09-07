using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    partial struct AddRandomDamageSystem : ISystem
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
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            new AddRandomDamageJob()
            {
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                ElapsedTime = (float)elapsedTime,
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct AddRandomDamageJob : IJobEntity
    {
        public float ElapsedTime;
        public EntityCommandBuffer.ParallelWriter MyEntityCommandBuffer;
        [BurstCompile]
        private void Execute(Entity entity, in DamageRandomData damageRandomData, ref DamageData damageData, [ChunkIndexInQuery] int sortkey)
        {
            uint seed = (uint)ElapsedTime * (uint)entity.Index;
            var randomDamageData = Random.CreateFromIndex(seed).NextFloat(damageRandomData.MinDamage, damageRandomData.MaxDamage);
            damageData.Damage = randomDamageData;

            MyEntityCommandBuffer.SetComponentEnabled<DamageRandomData>(sortkey, entity, false); // DamageRandomData 1 kere çalýþýyor yani 2-10 arasýnda deðerini verip kaçýyor. yani enabled = false;
        }
    }
}
