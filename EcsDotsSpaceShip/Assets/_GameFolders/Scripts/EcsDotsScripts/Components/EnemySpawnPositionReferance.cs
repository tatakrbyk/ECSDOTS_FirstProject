using Unity.Entities;
using Unity.Mathematics;

namespace SpaceShipEcsDots.Components
{
    public struct EnemySpawnPositionReferance : IComponentData
    {
        public BlobAssetReference<EnemySpawnPositionBlob> BlobValueReference;
    }

    public struct EnemySpawnPositionBlob
    {
        public BlobArray<float3> Values; // Enemy Prefableri spawnlamak i�in blob array kuullanmam�z gerekli ECSDOTS
    }

}
