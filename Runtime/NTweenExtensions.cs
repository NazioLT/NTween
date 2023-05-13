using UnityEngine;

namespace Nazio_LT.Tools.NTween
{
    public static class NTweenExtensions
    {
        #region NTMoveTo

        public static NTweener NTMoveTo(this Transform transform, Vector3 destination, float duration) => NTween.NTMoveTo(transform, destination, duration);

        public static NTweener NTMoveTo(this Rigidbody body, Vector3 destination, float duration) => NTween.NTMoveTo(body, destination, duration);

        public static NTweener NTMoveTo(this Rigidbody2D body, Vector3 destination, float duration) => NTween.NTMoveTo(body, destination, duration);

        #endregion

        #region NTMove

        public static NTweener NTMove(this Transform transform, Vector3 delta, float duration) => NTween.NTMove(transform, delta, duration);

        public static NTweener NTMove(this Rigidbody body, Vector3 delta, float duration) => NTween.NTMove(body, delta, duration);

        public static NTweener NTMove(this Rigidbody2D body, Vector3 delta, float duration) => NTween.NTMove(body, delta, duration);

        #endregion

        #region NTRotateTo

        public static NTweener NTRotateTo(this Transform transform, Quaternion target, float duration) => NTween.NTRotateTo(transform, target, duration);

        public static NTweener NTRotateTo(this Transform transform, Vector3 eulerTarget, float duration) => NTween.NTRotateTo(transform, eulerTarget, duration);

        #endregion

        #region NTRotate

        public static NTweener NTRotate(this Transform transform, Quaternion delta, float duration) => NTween.NTRotate(transform, delta, duration);

        public static NTweener NTRotate(this Transform transform, Vector3 eulerDelta, float duration) => NTween.NTRotate(transform, eulerDelta, duration);

        #endregion

        #region NTScaleTo

        public static NTweener NTScaleTo(this Transform transform, Vector3 target, float duration) => NTween.NTScaleTo(transform, target, duration);

        #endregion
    }
}
