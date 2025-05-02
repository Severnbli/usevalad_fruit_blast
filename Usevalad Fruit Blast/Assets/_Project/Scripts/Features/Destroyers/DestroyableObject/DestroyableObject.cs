using System;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.DestroyableObject
{
    public class DestroyableObject : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Type _destroyableType = Type.Default;
        
        public int Id { get => _id; set => _id = value; }
        public Type DestroyableType { get => _destroyableType; set => _destroyableType = value; }
        
        [Serializable]
        public enum Type
        {
            Default = 0,
            Infectious = 1
        }
        
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

        public virtual void Destroy(ObjectDestroyer destroyer, float destroyDelay)
        {
            Destroy(destroyer.gameObject, destroyDelay);
        }
    }
}