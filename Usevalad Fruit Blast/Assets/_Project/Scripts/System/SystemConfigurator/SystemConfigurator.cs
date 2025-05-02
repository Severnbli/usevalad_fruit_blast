using UnityEngine;

namespace _Project.Scripts.System.SystemConfigurator
{
    public class SystemConfigurator : MonoBehaviour
    {
        [SerializeField] private SystemConfig _systemConfig;
        
        public SystemConfig SystemConfig => _systemConfig;
        
        private void OnEnable()
        {
            Setup();
        }

        private void OnDisable()
        {
            Context.ClearContext();
        }
        
        private void Setup()
        {
            SetupContext();
            SetupFeatures();
        }

        private void SetupContext()
        {
            Context.SetupContext(out var container, out var otherScopeFeature);
            container.transform.parent = transform;
        }

        private void SetupFeatures()
        {
            
        }
    }
}