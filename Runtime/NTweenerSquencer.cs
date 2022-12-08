using System;
using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.NTween.Internal;

namespace Nazio_LT.Tools.NTween
{
    public class NTweenerSquencer : NTweenBase
    {
        private Func<bool> tweenMethod;

        public Action<float> mainCallback { private set; get; }
        public Action onCompleteCallback { private set; get; }
        public Action onStartCallBack { private set; get; }

        private int currentTweener;
        private List<NTweener> tweeners = new List<NTweener>();

        private Func<float, float> timeConversionMethod = (_t) => _t;

        //Behaviour informations
        private bool pingpong = false;
        private bool loop = false;
        private float duration = 0;
        private float startWaitingDuration = 0;

        private bool paused = false;

        //Running Infos
        private float tweenTime = 0;
        private float startWaitingTime = 0;

        private bool CheckIfValueRemainsLower(ref float _value, float _incrementation, float _max)
        {
            _value += _incrementation;
            return _value <= _max;
        }

        public override void Update(float _deltaTime)
        {
            if (currentTweener >= tweeners.Count)
            {
                if (loop) currentTweener = 0;
                else Stop(true);

                return;
            }

            tweeners[currentTweener].Update(_deltaTime);
        }

        #region Orders

        /// <summary>Stop tweening.</summary>
        public void Stop(bool _callCompleteCallback)
        {
            if (_callCompleteCallback && onCompleteCallback != null) onCompleteCallback();
            NTweenerUpdater.instance.UnRegisterTweener(this);
        }

        /// <summary>Pause tweening.</summary>
        public void Pause() => paused = true;

        /// <summary>Resume tweening.</summary>
        public void Resume() => paused = false;

        /// <summary>Set tweening pause to.</summary>
        public void SetPause(bool _value) => paused = _value;

        /// <summary>Inverse tweening pause value (True if false for exemple).</summary>
        public void InversePause() => paused = !paused;

        /// <summary>Start tweening.</summary>
        public NTweenerSquencer StartTween()
        {
            tweenTime = 0f;
            currentTweener = 0;

            NTweenerUpdater.instance.RegisterTweener(this);
            if (currentTweener <= tweeners.Count) tweeners[currentTweener].StartSequenceTween();

            if (onStartCallBack != null) onStartCallBack();

            return this;
        }

        public void PassTween()
        {
            currentTweener++;
            if (currentTweener <= tweeners.Count) tweeners[currentTweener].StartSequenceTween();
        }

        #endregion

        #region Settings

        public NTweenerSquencer Add(NTweener _tweener)
        {
            tweeners.Add(_tweener);
            _tweener.PutInSequencer(this);
            return this;
        }

        public NTweenerSquencer PingPong()
        {
            pingpong = true;
            return this;
        }

        public NTweenerSquencer Loop()
        {
            loop = true;
            return this;
        }

        public NTweenerSquencer UnscaledTime()
        {
            unscaledTime = true;
            return this;
        }

        public NTweenerSquencer OnComplete(Action _callback)
        {
            onCompleteCallback = _callback;
            return this;
        }

        public NTweenerSquencer AddTimeCurve(AnimationCurve _curve)
        {
            timeConversionMethod = (_t) => _curve.Evaluate(_t);
            return this;
        }

        public NTweenerSquencer AddTimeConversionMethod(Func<float, float> _callback)
        {
            timeConversionMethod = _callback;
            return this;
        }

        public NTweenerSquencer WaitBeforeStart(float _value)
        {
            startWaitingDuration = _value;
            return this;
        }

        public NTweenerSquencer OnStart(Action _callback)
        {
            onStartCallBack = _callback;
            return this;
        }

        #endregion
    }
}