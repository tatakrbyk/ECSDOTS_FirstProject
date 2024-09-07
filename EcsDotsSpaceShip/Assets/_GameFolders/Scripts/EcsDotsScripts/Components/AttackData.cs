using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct AttackData : IComponentData
    {
        public Entity Projectile;
        public float CurrentFireTime;
        public float MaxFireTime;
        public bool CanChange;
    }
}
