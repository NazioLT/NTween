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

        #region Material Set Values

        public static NTweener NTMatSetFloat(this Material material, string name, float minValue, float maxValue, float duration) => NTween.NTMatSetFloat(material, name, minValue, maxValue, duration);

        public static NTweener NTMatSetVector(this Material material, string name, Vector2 minValue, Vector2 maxValue, float duration) => NTween.NTMatSetVector(material, name, minValue, maxValue, duration);

        public static NTweener NTMatSetVector(this Material material, string name, Vector3 minValue, Vector3 maxValue, float duration) => NTween.NTMatSetVector(material, name, minValue, maxValue, duration);

        public static NTweener NTMatSetColor(this Material material, string name, Color minValue, Color maxValue, float duration) => NTween.NTMatSetColor(material, name, minValue, maxValue, duration);

        #endregion

        #region Material Set Values To

        public static NTweener NTMatSetFloatTo(this Material material, string name, float targetValue, float duration) => NTween.NTMatSetFloatTo(material, name, targetValue, duration);

        public static NTweener NTMatSetVectorTo(this Material material, string name, Vector2 targetValue, float duration) => NTween.NTMatSetVectorTo(material, name, targetValue, duration);

        public static NTweener NTMatSetVectorTo(this Material material, string name, Vector3 targetValue, float duration) => NTween.NTMatSetVectorTo(material, name, targetValue, duration);

        public static NTweener NTMatSetColorTo(this Material material, string name, Color targetValue, float duration) => NTween.NTMatSetColorTo(material, name, targetValue, duration);

        #endregion
    }
}
