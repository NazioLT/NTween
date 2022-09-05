using UnityEngine;

namespace Nazio_LT.NTween
{
    public static partial class Tweening
    {
        public static NTweener GoTo(this Transform _transform, Vector3 _pos, float _duration)
        {
            Vector3 _start = _transform.position;

            return new NTweener((_t) => _transform.position = Vector3.Lerp(_start, _pos, _t), _duration);
        }
    }
}