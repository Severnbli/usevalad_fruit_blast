using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.DestroyableObject
{
    public class DestroyableObject : MonoBehaviour
    {
        protected virtual void Start()
        {
            if (!Context.TryGetComponentsFromContainer<ObjectDestroyer>(out var destroyers))
            {
                return;
            }

            foreach (var destroyer in destroyers)
            {
                destroyer.DestroyableObjects.Add(this);
            }
        }
        
        protected virtual void OnDestroy()
        {
            if (!Context.TryGetComponentsFromContainer<ObjectDestroyer>(out var destroyers))
            {
                return;
            }

            foreach (var destroyer in destroyers)
            {
                destroyer.DestroyableObjects.Remove(this);
            }
        }
    }
}