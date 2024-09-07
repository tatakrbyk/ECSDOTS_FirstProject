using SpaceShipEcsDots.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace SpaceShipEcsDots.Systems
{
    [UpdateAfter(typeof(AttackSystem))]
    public partial class LaserSoundSystem : SystemBase
    {
        public event System.Action<LaserSoundEntity> OnLaserCreated;

        protected override void OnCreate()
        {
            RequireForUpdate<LaserSoundData>();
        }

        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (laserSoundDataRO, entity) in SystemAPI.Query<RefRO<LaserSoundData>>().WithEntityAccess())
            {
                OnLaserCreated?.Invoke(new LaserSoundEntity()
                {
                    IsPlayer = laserSoundDataRO.ValueRO.IsPlayer
                });

                entityCommandBuffer.RemoveComponent<LaserSoundData>(entity);
            }

            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();
        }

        protected override void OnDestroy()
        {
            OnLaserCreated = null;
        }
    }

    public struct LaserSoundEntity
    {
        public bool IsPlayer;
    }

}
