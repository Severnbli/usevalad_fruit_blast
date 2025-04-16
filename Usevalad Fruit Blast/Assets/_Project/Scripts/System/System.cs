using _Project.Scripts.System.Installers;
using UnityEngine;

namespace _Project.Scripts.System
{
    public class System: MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] installers;

        public void Start()
        {
            Setup();
        }

        private void Setup()
        {
            foreach (var installer in installers)
            {
                if (installer is ISystemInstaller systemInstaller)
                {
                    systemInstaller.Install();
                }
            }
        }
    }
}