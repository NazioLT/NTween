using UnityEngine;

namespace Nazio_LT.NTween
{
    public static partial class Tweening
    {
        #region Transform Tweening

        public static NTweener EXEMoveTo(this Transform _transform, Vector3 _end, float _duration)
        {
            Vector3 _start = _transform.position;

            return _transform.EXEMoveTo(_start, _end, _duration);
        }

        public static NTweener EXEMoveTo(this Transform _transform, Vector3 _start, Vector3 _end, float _duration) => new NTweener((_t) => _transform.position = Vector3.Lerp(_start, _end, _t), _duration);

        public static NTweener EXEMove(this Transform _transform, Vector3 _delta, float _duration)
        {
            Vector3 _start = _transform.position;
            Vector3 _end = _transform.position + _delta;

            return _transform.EXEMoveTo(_start, _end, _duration);
        }

        public static NTweener EXERotateTo(this Transform _transform, Quaternion _end, float _duration)
        {
            Quaternion _start = _transform.rotation;

            return new NTweener((_t) => _transform.rotation = Quaternion.Slerp(_start, _end, _t), _duration);
        }

        public static NTweener EXERotate(this Transform _transform, Quaternion _delta, float _duration)
        {
            Quaternion _end = _transform.rotation * _delta;

            return _transform.EXERotateTo(_end, _duration);
        }

        #endregion
    }
}