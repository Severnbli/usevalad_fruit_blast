using System.Threading;
using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Random;
using _Project.Scripts.Features.Stats.Experience;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    public class ExperienceEffectObject : MonoBehaviour
    {
        [SerializeField] private float moveDuration = 1.5f;
        [SerializeField] private AnimationCurve accelerationCurve;

        private ExperienceProvider _experienceProvider;
        private RandomProvider _randomProvider;
        private Transform _target;

        private Vector3 _start;
        private Vector3 _control1;
        private Vector3 _control2;
        private Vector3 _end;
        private float _timer;

        private CancellationToken _ct;

        public int Experience { get; set; }

        private void Start()
        {
            _ct = gameObject.GetCancellationTokenOnDestroy();

            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
                return;

            var context = systemCoordinator.Context;
            context.TryGetComponentFromContainer(out _experienceProvider);
            context.TryGetComponentFromContainer(out _randomProvider);

            _target = _experienceProvider.GetExperienceTarget();
            if (_target == null) return;

            _start = transform.position;
            _end = _target.position;

            var midPoint = (_start + _end) * 0.5f;
            _control1 = midPoint + new Vector3(
                _randomProvider.GetRandomInRange(-4f, 4f),
                _randomProvider.GetRandomInRange(2f, 4f)
            );

            _control2 = midPoint + new Vector3(
                _randomProvider.GetRandomInRange(-4f, 4f),
                _randomProvider.GetRandomInRange(2f, 4f)
            );

            FlyToTarget().Forget();
        }

        private async UniTask FlyToTarget()
        {
            _timer = 0f;

            while (_timer < moveDuration && !_ct.IsCancellationRequested)
            {
                var linearT = _timer / moveDuration;
                var t = accelerationCurve.Evaluate(linearT);

                var pos =
                    Mathf.Pow(1 - t, 3) * _start +
                    3 * Mathf.Pow(1 - t, 2) * t * _control1 +
                    3 * (1 - t) * Mathf.Pow(t, 2) * _control2 +
                    Mathf.Pow(t, 3) * _end;

                transform.position = pos;

                _timer += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (_ct.IsCancellationRequested)
            {
                return;
            }

            if (_experienceProvider != null && _experienceProvider.IsEffectNear(gameObject))
            {
                _experienceProvider.AddExperience(Experience);
            }

            Destroy(gameObject);
        }
    }
}
