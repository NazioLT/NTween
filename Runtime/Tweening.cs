using UnityEngine;

namespace Nazio_LT.NTween
{
    public static partial class Tweening
    {
        public static NTweener GoTo(this Transform _transform, Vector3 _end, float _duration)
        {
            Vector3 _start = _transform.position;

            return new NTweener((_t) => _transform.position = Vector3.Lerp(_start, _end, _t), _duration);
        }

        public static NTweener Move(this Transform _transform, Vector3 _delta, float _duration)
        {
            Vector3 _start = _transform.position;
            Vector3 _end = _transform.position + _delta;

            return new NTweener((_t) => _transform.position = Vector3.Lerp(_start, _end, _t), _duration);
        }
    }
}