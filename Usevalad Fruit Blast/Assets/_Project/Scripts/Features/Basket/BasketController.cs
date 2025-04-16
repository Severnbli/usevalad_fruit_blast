using _Project.Scripts.Utils;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace _Project.Scripts.Features.Basket
{
    public class BasketController: MonoBehaviour
    {
        private Camera _targetCamera;
        
        public Vector2Int Size { get; set; }
        public Margin Margin { get; set; }

        public void SetAll(Vector2Int size, Margin margin, Camera targetCamera)
        {
            Size = size;
            Margin = margin;
            _targetCamera = targetCamera;
            
            UpdateBasket();
        }

        public void UpdateBasket()
        {
            UpdateSize();
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            float edgeY = _targetCamera.transform.position.y
                             + _targetCamera.orthographicSize
                             - Margin.Top
                             - transform.localScale.y / 2;
            
            transform.position = new Vector3(_targetCamera.transform.position.x, edgeY, transform.position.z);
        }

        public void UpdateSize()
        {
            float cameraHeight = 2 * _targetCamera.orthographicSize;
            float cameraWidth = cameraHeight * _targetCamera.aspect;

            float widthScale = cameraWidth - Margin.Left - Margin.Right;
            float heightScaleByWidth = widthScale * Size.y / Size.x;
            
            float heightScale = cameraHeight - Margin.Top - Margin.Bottom;
            float widthScaleByHeight = heightScale * Size.x / Size.y;
            
            if (Size.x > Size.y && heightScaleByWidth <= heightScale || Size.y > Size.x && widthScaleByHeight > widthScale)
            {
                heightScale = heightScaleByWidth;
            }
            else
            {
                widthScale = widthScaleByHeight;
            }
            
            transform.localScale = new Vector3(widthScale, heightScale, transform.localScale.z);
        }
    }
}