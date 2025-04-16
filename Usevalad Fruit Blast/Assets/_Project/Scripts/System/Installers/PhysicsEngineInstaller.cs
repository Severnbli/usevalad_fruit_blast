using _Project.Scripts.Features.Physics;
using _Project.Scripts.Repositories;
using UnityEngine;

namespace _Project.Scripts.System.Installers
{
    public class PhysicsEngineInstaller: MonoBehaviour, ISystemInstaller
    {
        public void Install()
        {
            Context.Container.AddComponent<PhysicsEngine>();
        }
    }
}