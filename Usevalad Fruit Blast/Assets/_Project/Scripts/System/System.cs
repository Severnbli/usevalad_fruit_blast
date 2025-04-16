using _Project.Scripts.System.Installers;
using UnityEngine;

namespace _Project.Scripts.System
{
    public class System: MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _installers;

        public void Start()
        {
            Setup();
        }

        private void Setup()
        {
            foreach (var installer in _installers)
            {
                if (installer is ISystemInstaller systemInstaller)
                {
                    systemInstaller.Install();
                }
            }
        }
    }
}