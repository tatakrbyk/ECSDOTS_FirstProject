using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    class EnemyProjectileAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 10f;
        public float Direction = -1f;
        public float MaxLifeTime = 10f;

        public float MaxDamage = 10f;
        public float MinDamage = 2f;
    }

    class EnemyProjectileBaker : Baker<EnemyProjectileAuthoring>
    {
        public override void Bake(EnemyProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<EnemyProjectileTag>(entity);
            AddComponent<DamageData>(entity);

            AddComponent(entity, new LaserSoundData()
            {
                IsPlayer = false
            }); ;

            AddComponent(entity, new DamageRandomData()
            {
                MaxDamage = authoring.MaxDamage,
                MinDamage = authoring.MinDamage,

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

