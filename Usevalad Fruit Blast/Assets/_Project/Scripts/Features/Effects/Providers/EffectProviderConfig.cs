using _Project.Scripts.Features.FeatureCore;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers
{
    [CreateAssetMenu(fileName = "EffectProviderConfig", menuName = "Configs/Effects/Providers/Effect Provider Config")]
    public class EffectProviderConfig: ScriptableObject, IFeatureConfig
    {
        [ValueDropdown("GetSortingLayerNames")]
        [SerializeField] private string _effectSortingLayerName;
        [SerializeField] private int _effectSortingLayerOrder;
        
        public string EffectSortingLayerName => _effectSortingLayerName;
        public int EffectSortingLayerOrder => _effectSortingLayerOrder;
        
        private static string[] GetSortingLayerNames()
        {
            var count = SortingLayer.layers.Length;
            var names = new string[count];
            for (var i = 0; i < count; i++)
            {
                names[i] = SortingLayer.layers[i].name;
            }
            return names;
        }
    }
}