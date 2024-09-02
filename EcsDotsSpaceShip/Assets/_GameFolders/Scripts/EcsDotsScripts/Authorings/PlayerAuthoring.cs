using SpaceShipEcsDots.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;

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
        public GameObject projectilePrefab;
        public float MaxFireTime = 1f;
    }

    // make Entity
    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic); // Set Component

            AddComponent(entity, new PlayerMoveData()
            {
                MoveSpeed = authoring.MoveSpeed,
            });

            AddComponent(entity, new MoveBorderData()
            {
                Horizontal = authoring.Horizontal,
                Vertical1 = authoring.Vertical1,
                Vertical2 = authoring.Vertical2,
            });

            AddComponent(entity, new AttackData()
            {
                Projectile = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic),
                MaxFireTime = authoring.MaxFireTime,
            });

            AddComponent<InputData>(entity);
            AddComponent<PlayerTag>(entity);
        }
    }
}
