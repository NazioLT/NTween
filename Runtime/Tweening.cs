using UnityEngine;
using UnityEngine.UI;

namespace Nazio_LT.NTween
{
    public static partial class Tweening
    {
        #region Transform Tweening

        public static NTweener NTMoveTo(this Transform _transform, Vector3 _end, float _duration)
        {
            Vector3 _start = _transform.position;

            return _transform.NTMoveTo(_start, _end, _duration);
        }

        public static NTweener NTMoveTo(this Transform _transform, Vector3 _start, Vector3 _end, float _duration) => new NTweener((_t) => _transform.position = Vector3.LerpUnclamped(_start, _end, _t), _duration);

        public static NTweener NTMove(this Transform _transform, Vector3 _delta, float _duration)
        {
            Vector3 _start = _transform.position;
            Vector3 _end = _transform.position + _delta;

            return _transform.NTMoveTo(_start, _end, _duration);
        }

        public static NTweener NTRotateTo(this Transform _transform, Quaternion _end, float _duration)
        {
            Quaternion _start = _transform.rotation;

            return new NTweener((_t) => _transform.rotation = Quaternion.SlerpUnclamped(_start, _end, _t), _duration);
        }

        public static NTweener NTRotate(this Transform _transform, Quaternion _delta, float _duration)
        {
            Quaternion _end = _transform.rotation * _delta;

            return _transform.NTRotateTo(_end, _duration);
        }

        public static NTweener NTScaleTo(this Transform _transform, Vector3 _end, float _duration)
        {
            Vector3 _start = _transform.localScale;

            return _transform.NTScaleTo(_start, _end, _duration);
        }

        public static NTweener NTScaleTo(this Transform _transform, Vector3 _start, Vector3 _end, float _duration) => new NTweener((_t) => _transform.localScale = Vector3.LerpUnclamped(_start, _end, _t), _duration);

        #endregion

        #region Color Tweening

        public static NTweener NTColorTo(this SpriteRenderer _sprite, Color _end, float _duration)
        {
            Color _start = _sprite.color;

            return _sprite.NTColorTo(_end, _duration);
        }

        public static NTweener NTColorTo(this SpriteRenderer _sprite, Color _start, Color _end, float _duration) => new NTweener((_t) => _sprite.color = Color.LerpUnclamped(_start, _end, _t), _duration);

        public static NTweener NTColorTo(this Material _mat, Color _end, float _duration)
        {
            Color _start = _mat.color;

            return _mat.NTColorTo(_end, _duration);
        }

        public static NTweener NTColorTo(this Material _mat, Color _start, Color _end, float _duration) => new NTweener((_t) => _mat.color = Color.LerpUnclamped(_start, _end, _t), _duration);

        public static NTweener NTColorTo(this Image _img, Color _end, float _duration)
        {
            Color _start = _img.color;

            return _img.NTColorTo(_end, _duration);
        }

        public static NTweener NTColorTo(this Image _img, Color _start, Color _end, float _duration) => new NTweener((_t) => _img.color = Color.LerpUnclamped(_start, _end, _t), _duration);

        #endregion

        public static NTweener NTBuild(System.Action<float> _callback, float _duration) => new NTweener((_t) => _callback(_t), _duration);
    }
}