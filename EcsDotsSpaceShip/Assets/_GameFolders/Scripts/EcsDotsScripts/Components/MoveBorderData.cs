using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Components
{
    public struct MoveBorderData : IComponentData
    {
        public float Horizontal;
        public float Vertical1;
        public float Vertical2;
    }

}