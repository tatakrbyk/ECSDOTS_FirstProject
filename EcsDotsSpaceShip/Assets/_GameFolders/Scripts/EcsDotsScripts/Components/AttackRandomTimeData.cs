using Unity.Entities;

namespace SpaceShipEcsDots.Components
{
    public struct AttackRandomTimeData : IComponentData
    {
        public float MaxFireRandomTime;
        public float MinFireRandomTime;
    }

}
