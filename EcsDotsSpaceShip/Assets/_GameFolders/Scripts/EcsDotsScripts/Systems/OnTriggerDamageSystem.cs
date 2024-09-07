using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderLast = true)]
    public partial struct OnTriggerDamageSystem : ISystem
    {
        struct ComponentDataHandler
        {
            public ComponentLookup<HealthData> HealthDataLookup;
            public ComponentLookup<DamageData> DamageDataLookup;

            public ComponentDataHandler(ref SystemState state)
            {
                HealthDataLookup = state.GetComponentLookup<HealthData>();
                DamageDataLookup = state.GetComponentLookup<DamageData>();
            }

            public void Update(ref SystemState state)
            {
                HealthDataLookup.Update(ref state);
                DamageDataLookup.Update(ref state);
            }
        }

        ComponentDataHandler _componentDataHandler;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
            _componentDataHandler = new ComponentDataHandler(ref state);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            _componentDataHandler.Update(ref state);

            state.Dependency = new OnTriggerDamageJob()
            {
                DamageDataLookup = _componentDataHandler.DamageDataLookup,
                HealthDataLookup = _componentDataHandler.HealthDataLookup,
                MyEntityCommandBuffer = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }
    }

    [BurstCompile]
    public partial struct OnTriggerDamageJob : ITriggerEventsJob
    {
        public EntityCommandBuffer MyEntityCommandBuffer;
        public ComponentLookup<HealthData> HealthDataLookup;
        public ComponentLookup<DamageData> DamageDataLookup;

        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;

            if (HealthDataLookup.HasComponent(entityA))
            {
                if (DamageDataLookup.HasComponent(entityB))
                {
                    EntityDamageProcess(entityB, entityA);
                }
            }
            else if (HealthDataLookup.HasComponent(entityB))
            {
                if (DamageDataLookup.HasComponent(entityA))
                {
                    EntityDamageProcess(entityA, entityB);
                }
            }
        }

        private void EntityDamageProcess(Entity entity1, Entity entity2)
        {
            var damage = DamageDataLookup.GetRefRO(entity1).ValueRO.Damage;
            var healthDataRW = HealthDataLookup.GetRefRW(entity2);
            healthDataRW.ValueRW.CurrentHealth -= damage;
            MyEntityCommandBuffer.DestroyEntity(entity1);
        }
    }
}