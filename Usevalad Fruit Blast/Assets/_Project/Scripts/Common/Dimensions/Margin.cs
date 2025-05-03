using System;
using UnityEngine;

namespace _Project.Scripts.Common.Dimensions
{
    [Serializable]
    public struct Margin
    {
        [SerializeField] private float _top;
        [SerializeField] private float _left;
        [SerializeField] private float _right;
        [SerializeField] private float _bottom;
        
        public float Top { get => _top; set => _top = value; }
        public float Left { get => _left; set => _left = value; }
        public float Right { get => _right; set => _right = value; }
        public float Bottom { get => _bottom; set => _bottom = value; }
    }
}