using SpaceShipEcsDots.Components;
using SpaceShipEcsDots.Inputs;
using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)] // first run

    public partial class PlayerInputSystems : SystemBase
    {
        GameInputActions _input;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            _input = new GameInputActions();
        }

        protected override void OnStartRunning()
        {
            _input.Enable();
        }

        protected override void OnStopRunning()
        {
            _input.Disable();
        }

        protected override void OnUpdate()
        {
            var directionWithVector2 = _input.Player.Move.ReadValue<Vector2>();
            float3 directionWithFloat3 = new float3(directionWithVector2.X, directionWithVector2.Y, 0f);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var inputData = SystemAPI.GetComponent<InputData>(playerEntity);
            inputData.Direction = directionWithFloat3;

            SystemAPI.SetSingleton(inputData);
        }
    }
}
