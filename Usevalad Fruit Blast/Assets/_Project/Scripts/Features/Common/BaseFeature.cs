using UnityEngine;

namespace _Project.Scripts.Features.Common
{
    public abstract class BaseFeature : MonoBehaviour
    { 
        public abstract void Init(IFeatureConfig config);
    }
}