using UnityEngine;
using UnityEngine.UI;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.NTween
{
    public static partial class Tweening
    {
        #region Transform Tweening

        public static NTweener NTMoveTo(this Transform _transform, Vector3 _end, float _duration)
        {
            Vector3 _start = _transform.position;

            return _transform.NTMoveTo(_start, _end, _duration);
        }

        /// <summary>
        /// Will tween the transform position from start to end.
        /// </summary>
        /// <param name="_transform"></param>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        /// <param name="_duration"></param>
        /// <returns></returns>
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

        public static NTweener NTColorTo(this SpriteRenderer _sprite, Color _end, float _duration) => _sprite.NTColorTo(_sprite.color, _end, _duration);
        public static NTweener NTColorTo(this SpriteRenderer _sprite, Color _start, Color _end, float _duration) => new NTweener((_t) => _sprite.color = Color.LerpUnclamped(_start, _end, _t), _duration);


        public static NTweener NTColorTo(this Material _mat, Color _end, float _duration) => _mat.NTColorTo(_mat.color, _end, _duration);
        public static NTweener NTColorTo(this Material _mat, Color _start, Color _end, float _duration) => new NTweener((_t) => _mat.color = Color.LerpUnclamped(_start, _end, _t), _duration);


        public static NTweener NTColorTo(this Image _img, Color _end, float _duration) => _img.NTColorTo(_img.color, _end, _duration);
        public static NTweener NTColorTo(this Image _img, Color _start, Color _end, float _duration) => new NTweener((_t) => _img.color = Color.LerpUnclamped(_start, _end, _t), _duration);

        public static NTweener NTAlphaTo(this SpriteRenderer _sprite, float _end, float _duration) => _sprite.NTAlphaTo(_sprite.color.a, _end, _duration);
        public static NTweener NTAlphaTo(this SpriteRenderer _sprite, float _start, float _end, float _duration) => new NTweener((_t) => _sprite.color = NMath.SetAlpha(_sprite.color, Mathf.LerpUnclamped(_start, _end, _t)), _duration);

        public static NTweener NTAlphaTo(this Material _mat, float _end, float _duration) => _mat.NTAlphaTo(_mat.color.a, _end, _duration);
        public static NTweener NTAlphaTo(this Material _mat, float _start, float _end, float _duration) => new NTweener((_t) => _mat.color = NMath.SetAlpha(_mat.color, Mathf.LerpUnclamped(_start, _end, _t)), _duration);

        public static NTweener NTAlphaTo(this Image _img, float _end, float _duration) => _img.NTAlphaTo(_img.color.a, _end, _duration);
        public static NTweener NTAlphaTo(this Image _img, float _start, float _end, float _duration) => new NTweener((_t) => _img.color = NMath.SetAlpha(_img.color, Mathf.LerpUnclamped(_start, _end, _t)), _duration);

        #endregion

        /// <summary>
        /// Build a tweener with custom method callback.
        /// </summary>
        /// <param name="_callback">Callback Method</param>
        /// <param name="_duration">Duration of the tween</param>
        /// <returns>Tweener</returns>
        public static NTweener NTBuild(System.Action<float> _callback, float _duration) => new NTweener((_t) => _callback(_t), _duration);

        #region Spline Tweening

        public static NTweener NTFollowCurveDuration(this Transform _transform, NCurve _curve, float _animationTime) => new NTweener((_t) => _transform.position = _curve.ComputePoint(_t), _animationTime);

        public static NTweener NTFollowCurveDurationUniform(this Transform _transform, NCurve _curve, float _animationTime) => new NTweener((_t) => _transform.position = _curve.ComputePointUniform(_t), _animationTime);//=> new NTweener((_t) => _transform.position = _curve.ComputePointDistance(_t * _curve.curveLength), _animationTime);

        #endregion
    }
}