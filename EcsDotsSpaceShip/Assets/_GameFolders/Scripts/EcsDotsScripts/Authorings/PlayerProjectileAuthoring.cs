
using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class PlayerProjectileAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 10f;
        public float Direction = 1f;
        public float MaxLifeTime= 2f;
        public float MaxDamage = 10f;

    }

    public class PlayerProjectileBaker : Baker<PlayerProjectileAuthoring>
    {
        public override void Bake(PlayerProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerProjectileTag>(entity);

            AddComponent(entity, new LaserSoundData()
            {
                IsPlayer = true,
            });

            AddComponent(entity, new DamageData()
            {
                Damage = authoring.MaxDamage,
            });
            AddComponent(entity, new ProjectileMoveData()
            {
                Direction = authoring.Direction,
                MoveSpeed = authoring.MoveSpeed,
            });

            AddComponent(entity, new ProjectileSelfDestroyData()
            {
                MaxLifeTime = authoring.MaxLifeTime,
                CanDestroy = false,
                CurrentLifeTime = 0f
            });
        }
    }

}

