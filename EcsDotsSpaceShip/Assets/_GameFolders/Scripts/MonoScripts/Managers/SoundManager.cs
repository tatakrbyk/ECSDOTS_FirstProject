using System;
using System.Collections.Generic;
using SpaceShipEcsDots.Controllers;
using SpaceShipEcsDots.Enums;
using SpaceShipEcsDots.Systems;
using Unity.Entities;
using UnityEngine;

namespace SpaceShipEcsDots.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] SoundInspector[] _soundInspectors;

        Dictionary<SoundType, SoundController> _sounds;

        World _ecsWorld;

        void Awake()
        {
            _sounds = new Dictionary<SoundType, SoundController>();

            for (int i = 0; i < _soundInspectors.Length; i++)
            {
                var soundInspector = _soundInspectors[i];
                _sounds.TryAdd(soundInspector.SoundType, soundInspector.SoundController);
            }

            _ecsWorld = World.DefaultGameObjectInjectionWorld;
        }

        void OnEnable()
        {
            var laserSoundSystem = _ecsWorld.GetExistingSystemManaged<LaserSoundSystem>();
            laserSoundSystem.OnLaserCreated += HandleOnLaserCreated;
        }

        void HandleOnLaserCreated(LaserSoundEntity entity)
        {
            if (entity.IsPlayer)
            {
                _sounds[SoundType.PlayerLaser].Play();
            }
            else
            {
                _sounds[SoundType.EnemyLaser].Play();
            }
        }
    }

    [System.Serializable]
    public class SoundInspector
    {
        public SoundType SoundType;
        public SoundController SoundController;
    }
}