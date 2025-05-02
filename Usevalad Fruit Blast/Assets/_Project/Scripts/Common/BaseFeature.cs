using UnityEngine;

namespace _Project.Scripts.Common
{
    public abstract class BaseFeature : MonoBehaviour
    {
        public abstract void Init();

        public abstract void Configure<T>(T config) where T : IBaseFeatureConfig;
    }
}