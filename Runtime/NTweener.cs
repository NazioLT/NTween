using UnityEngine;
using System;
using Nazio_LT.Tools.NTween.Internal;

namespace Nazio_LT.Tools.NTween
{
    public class NTweener
    {
        public NTweener(Action<float> _action, float _duration)
        {
            mainCallback = _action;
            duration = _duration;
        }

        private Func<bool> tweenMethod;

        private Action<float> mainCallback;
        private Action onCompleteCallback;

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

        private bool MainTween(bool _reverse, float _tweenTime)
        {
            if (paused) return false;

            if (_tweenTime > 1f) _tweenTime = 1f;
            float _t = _reverse ? 1 - _tweenTime : _tweenTime;
            float _convertedTime = timeConversionMethod(_t);

            mainCallback(_convertedTime);

            return _tweenTime >= 1;
        }

        private bool MainTween(bool _reverse) => MainTween(_reverse, tweenTime);


        private bool MainTweenPingPong()
        {
            if (tweenTime <= 1)
            {
                MainTween(false);
                return false;
            }

            float _t = tweenTime - 1;
            return MainTween(true, _t);
        }

        private bool CheckIfValueRemainsLower(ref float _value, float _incrementation, float _max)
        {
            _value += _incrementation;
            return _value <= _max;
        }

        public void Update(float _deltaTime)
        {
            if(CheckIfValueRemainsLower(ref startWaitingTime, _deltaTime, startWaitingDuration)) return;
            
            tweenTime += _deltaTime / duration;

            if (tweenMethod()) CompleteTween();
        }

        private void CompleteTween()
        {
            if (loop)
            {
                tweenTime = 0f;
                return;
            }

            Stop(true);
        }

        #region Orders

        public void Stop(bool _callCompleteCallback)
        {
            if (_callCompleteCallback && onCompleteCallback != null) onCompleteCallback();
            NTweenerUpdater.instance.UnRegisterTweener(this);
        }

        public void Pause() => paused = true;
        public void Resume() => paused = false;
        public void SetPause(bool _value) => paused = _value;
        public void InversePause() => paused = !paused;

        public NTweener StartTween()
        {
            tweenTime = 0f;
            tweenMethod = pingpong ? () => MainTweenPingPong() : () => MainTween(false);

            NTweenerUpdater.instance.RegisterTweener(this);

            return this;
        }

        #endregion

        #region Settings

        public NTweener PingPong()
        {
            pingpong = true;
            return this;
        }

        public NTweener Loop()
        {
            loop = true;
            return this;
        }

        public NTweener OnComplete(Action _callback)
        {
            onCompleteCallback = _callback;
            return this;
        }

        public NTweener AddTimeCurve(AnimationCurve _curve)
        {
            timeConversionMethod = (_t) => _curve.Evaluate(_t);
            return this;
        }

        public NTweener AddTimeConversionMethod(Func<float, float> _callback)
        {
            timeConversionMethod = _callback;
            return this;
        }

        public NTweener WaitBeforeStart(float _value)
        {
            startWaitingDuration = _value;
            return this;
        }

        #endregion
    }
}