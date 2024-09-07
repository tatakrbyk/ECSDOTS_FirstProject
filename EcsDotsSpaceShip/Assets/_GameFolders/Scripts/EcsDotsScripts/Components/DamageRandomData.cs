using Unity.Entities;

namespace SpaceShipEcsDots.Components
{ 
    public struct DamageRandomData : IComponentData, IEnableableComponent
    {
        public float MaxDamage;
        public float MinDamage;
    }
}
