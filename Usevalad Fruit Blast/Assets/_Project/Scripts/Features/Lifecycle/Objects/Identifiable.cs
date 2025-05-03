using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects
{
    public class Identifiable : MonoBehaviour
    {
        [SerializeField] private int _id;
        
        public int Id { get => _id; set => _id = value; }
    }
}