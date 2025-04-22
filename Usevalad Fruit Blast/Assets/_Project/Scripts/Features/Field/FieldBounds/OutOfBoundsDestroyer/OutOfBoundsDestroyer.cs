using System;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldBounds.OutOfBoundsDestroyer
{
    public class OutOfBoundsDestroyer: MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private BorderDestroyerDirection _direction;
        [SerializeField] private FieldProvider.FieldProvider _fieldProvider;

        [Serializable]
        public enum BorderDestroyerDirection
        {
            UP = 0,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3
        }
        
        public float Offset { get => _offset; set => _offset = value; }
        public BorderDestroyerDirection Direction { get => _direction; set => _direction = value; }
        public FieldProvider.FieldProvider FieldProvider { get => _fieldProvider; set => _fieldProvider = value; }

        private void FixedUpdate()
        {
            if (ReferenceEquals(_fieldProvider, null))
            {
                return;
            }
                
            if (IsOutOfBounds())
            {
                Destroy(gameObject);
            }
        }

        private bool IsOutOfBounds()
        {
            var halfFieldSize = _fieldProvider.GetFieldSize() / 2f;
            var fieldPosition = _fieldProvider.GetFieldPosition();
            
            switch (_direction)
            {
                case BorderDestroyerDirection.UP:
                {
                    if (transform.position.y > fieldPosition.y + halfFieldSize.y + _offset)
                    {
                        return true;
                    }

                    break;
                }
                case BorderDestroyerDirection.DOWN:
                {
                    if (transform.position.y < fieldPosition.y - halfFieldSize.y - _offset)
                    {
                        return true;
                    }

                    break;
                }
                case BorderDestroyerDirection.LEFT:
                {
                    if (transform.position.x < fieldPosition.x - halfFieldSize.x - _offset)
                    {
                        return true;
                    }

                    break;
                }
                case BorderDestroyerDirection.RIGHT:
                {
                    if (transform.position.x > fieldPosition.x + halfFieldSize.x + _offset)
                    {
                        return true;
                    }

                    break;
                }
                default:
                {
                    return false;
                }
            }
            
            return false;
        }
    }
}