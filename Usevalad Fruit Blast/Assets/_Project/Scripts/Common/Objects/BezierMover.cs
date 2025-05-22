using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace _Project.Scripts.Common.Objects
{
    public class BezierMover : MonoBehaviour
    {
        [SerializeField] private Vector2 _controlPointsRandomRangeX = new Vector2(-3f, 3f);
        [SerializeField] private Vector2 _controlPointsRandomRangeY = new Vector2(1f, 4f);
        [SerializeField] private float _effectDuration = 1f;
        [SerializeField, Range(0, 1)] private float _effectCalmPercentage = 0.2f;
        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        
        private CancellationToken _ct;
        private Vector3 _controlPoint1;
        private Vector3 _controlPoint2;
        
        public Transform Target { get; set; }
        public Random Random { get; set; }

        public event Action OnEndMoving;

        public void Setup(Transform target, Random random)
        {
            Random = random;
            Target = target;
            
            CalculateControlPoints();
            
            StartAnimation().Forget();
        }
        
        private void Awake()
        {
            _ct = gameObject.GetCancellationTokenOnDestroy();
        }

        private void CalculateControlPoints()
        {
            var midpoint = (transform.position + Target.position) * 0.5f;

            _controlPoint1 = midpoint + GetRandomVector();
            _controlPoint2 = midpoint + GetRandomVector();
        }

        private Vector3 GetRandomVector()
        {
            return new Vector3(
                (float)Random.NextDouble() * (_controlPointsRandomRangeX.y - _controlPointsRandomRangeX.x)
                + _controlPointsRandomRangeX.x,
                (float)Random.NextDouble() * (_controlPointsRandomRangeY.y - _controlPointsRandomRangeY.x)
                + _controlPointsRandomRangeY.x,
                0f
            );
        }

        private async UniTaskVoid StartAnimation()
        {
            var delaySeconds = _effectDuration * _effectCalmPercentage;
            
            await UniTask.WaitForSeconds(delaySeconds, cancellationToken: _ct);
            
            FlyToTarget().Forget();
        }

        private async UniTaskVoid FlyToTarget()
        {
            var elapsedTime = 0f;
            var startPosition = transform.position;
    
            while (elapsedTime < _effectDuration && Target != null && !_ct.IsCancellationRequested)
            {
                elapsedTime += Time.deltaTime;
                var normalizedTime = elapsedTime / _effectDuration;
                var curveValue = _animationCurve.Evaluate(normalizedTime);

                var currentTargetPos = Target.position;
                var currentPosition = GetBezierPoint(
                    startPosition,
                    _controlPoint1,
                    _controlPoint2,
                    currentTargetPos,
                    curveValue
                );

                transform.position = currentPosition;
                await UniTask.Yield(_ct);
            }
            
            OnEndMoving?.Invoke();
        }
        
        private Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            var u = 1 - t;
            var uu = u * u;
            var uuu = uu * u;
            var tt = t * t;
            var ttt = tt * t;

            return uuu * p0 + 
                   3 * uu * t * p1 + 
                   3 * u * tt * p2 + 
                   ttt * p3;
        }
    }
}