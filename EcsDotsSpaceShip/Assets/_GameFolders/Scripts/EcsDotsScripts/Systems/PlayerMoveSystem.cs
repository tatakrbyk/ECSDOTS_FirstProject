using SpaceShipEcsDots.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SpaceShipEcsDots.Systems
{
    [BurstCompile] // Convert fast c/c++ 
    [UpdateInGroup(typeof(SimulationSystemGroup))] // Systems Window index change
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        { 
            float deltaTime = SystemAPI.Time.DeltaTime;

            new PlayerMoveJob()
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }

    }

    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;
        [BurstCompile]
        private void Execute(Entity entity, ref LocalTransform localTransform, in PlayerMoveData moveData, in InputData input,
            in MoveBorderData moveBorderData)
        {
            float3 direction = input.Direction;
            float3 movePosition = (DeltaTime * moveData.MoveSpeed * direction) + localTransform.Position;
            movePosition.x = math.clamp(movePosition.x, -moveBorderData.Horizontal, moveBorderData.Horizontal);
            movePosition.y = math.clamp(movePosition.y, moveBorderData.Vertical2, moveBorderData.Vertical1);

            localTransform.Position = movePosition;
        }
    }
}

