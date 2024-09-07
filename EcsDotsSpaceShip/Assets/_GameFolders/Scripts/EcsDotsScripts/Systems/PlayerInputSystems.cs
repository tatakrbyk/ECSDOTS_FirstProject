using SpaceShipEcsDots.Components;
using SpaceShipEcsDots.Inputs;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceShipEcsDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial class PlayerInputSystem : SystemBase
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
            float3 directionWithFloat3 = new float3(directionWithVector2.x, directionWithVector2.y, 0f);

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var inputData = SystemAPI.GetComponent<InputData>(playerEntity);
            inputData.Direction = directionWithFloat3;

            SystemAPI.SetSingleton(inputData);
        }
    }
}