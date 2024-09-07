using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [Header("Movements")]
        public float MoveSpeed = 5f;
        public float Horizontal = 2f;
        public float Vertical1;
        public float Vertical2;

        [Header("Attacks")]
        public GameObject ProjectilePrefab;
        public float MaxFireTime = 1f;
        public int MaxHealth = 100;
    }

    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new HealthData()
            {
                MaxHealth = authoring.MaxHealth,
                CurrentHealth = authoring.MaxHealth
            });

            AddComponent(entity, new PlayerMoveData()
            {
                MoveSpeed = authoring.MoveSpeed
            });

            AddComponent(entity, new MoveBorderData()
            {
                Horizontal = authoring.Horizontal,
                Vertical1 = authoring.Vertical1,
                Vertical2 = authoring.Vertical2
            });

            AddComponent(entity, new AttackData()
            {
                Projectile = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
                CurrentFireTime = 0f,
                MaxFireTime = authoring.MaxFireTime
            });

            AddComponent<InputData>(entity);
            AddComponent<PlayerTag>(entity);
        }
    }
}