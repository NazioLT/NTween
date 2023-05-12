using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nazio_LT.Tools.NTween
{
    public static class NTween
    {
        public static NTweener NTBuild(Action<float> call, float duration) => new NTweener(call, duration);

        public static NTweener NTWait(float duration, Action onCompleteCall) => NTBuild((t) => { }, 0f).WaitBeforeStart(duration).OnComplete(onCompleteCall);

        #region NTMoveTo

        public static NTweener NTMoveTo(Transform transform, Vector3 destination, float duration)
        {
            Vector3 origin = transform.position;

            return NTBuild((t) =>
            {
                transform.position = Vector3.LerpUnclamped(origin, destination, t);
            }, duration);
        }

        public static NTweener NTMoveTo(Rigidbody body, Vector3 destination, float duration)
        {
            Vector3 origin = body.position;

            return NTBuild((t) =>
            {
                body.MovePosition(Vector3.LerpUnclamped(origin, destination, t));
            }, duration);
        }

        public static NTweener NTMoveTo(Rigidbody2D body, Vector3 destination, float duration)
        {
            Vector3 origin = body.position;

            return NTBuild((t) =>
            {
                body.MovePosition(Vector3.LerpUnclamped(origin, destination, t));
            }, duration);
        }

        #endregion

        #region NTMove

        public static NTweener NTMove(Transform transform, Vector3 delta, float duration)
        {
            Vector3 dest = transform.position + delta;

            return NTMoveTo(transform, dest, duration);
        }

        public static NTweener NTMove(Rigidbody body, Vector3 delta, float duration)
        {
            Vector3 dest = body.position + delta;

            return NTMoveTo(body, dest, duration);
        }

        public static NTweener NTMove(Rigidbody2D body, Vector3 delta, float duration)
        {
            Vector3 origin = body.position;
            Vector3 dest = origin + delta;

            return NTMoveTo(body, dest, duration);
        }

        #endregion

        #region NTRotateTo

        public static NTweener NTRotateTo(Transform transform, Quaternion target, float duration)
        {
            Quaternion origin = transform.localRotation;

            return NTBuild((t) => transform.localRotation = Quaternion.SlerpUnclamped(origin, target, t), duration);
        }

        public static NTweener NTRotateTo(Transform transform, Vector3 eulerTarget, float duration)
        {
            Vector3 origin = transform.localEulerAngles;

            return NTBuild((t) => transform.localEulerAngles = Vector3.SlerpUnclamped(origin, eulerTarget, t), duration);
        }

        #endregion

        #region NTRotate

        public static NTweener NTRotate(Transform transform, Quaternion delta, float duration)
        {
            Quaternion target = transform.localRotation * delta;

            return NTRotateTo(transform, target, duration);
        }

        public static NTweener NTRotate(Transform transform, Vector3 eulerDelta, float duration)
        {
            Vector3 target = transform.localEulerAngles + eulerDelta;

            return NTRotateTo(transform, target, duration);
        }

        #endregion

        #region NTScaleTo

        public static NTweener NTScaleTo(Transform transform, Vector3 target, float duration)
        {
            Vector3 origin = transform.localScale;

            return NTBuild((t) =>
            {
                transform.localScale = Vector3.LerpUnclamped(origin, target, t);
            }, duration);
        }

        #endregion
    }
}
