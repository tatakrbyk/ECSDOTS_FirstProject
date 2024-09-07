using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct HealthData : IComponentData
    {
        public float CurrentHealth;
        public int MaxHealth;
    }
}
