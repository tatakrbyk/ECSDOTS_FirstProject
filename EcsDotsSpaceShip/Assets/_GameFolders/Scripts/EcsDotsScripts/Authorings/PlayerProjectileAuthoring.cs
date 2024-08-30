
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Authorings
{
    public class PlayerProjectileAuthoring : MonoBehaviour
    {

    }

    public class PlayerProjectileBaker : Baker<PlayerProjectileAuthoring>
    {
        public override void Bake(PlayerProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
        }
    }

}

