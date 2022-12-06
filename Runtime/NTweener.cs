using UnityEngine;
using System;
using Nazio_LT.Tools.NTween.Internal;
using System.Collections.Generic;

namespace Nazio_LT.Tools.NTween
{
    public class NTweenerSquencer
    {
        private List<NTweener> instructions = new List<NTweener>();
        private int currentInstruction;

        public NTweenerSquencer Add(NTweener _tweener)
        {
            instructions.Add(_tweener);
            return this;
        }

        public void Start()
        {
            currentInstruction = 0;
            PlayCurrentTweener();
        }

        private void PlayCurrentTweener()
        {
            Action _currentOnComplete = CurrentInstruction.onCompleteCallback;

            CurrentInstruction.OnComplete(() =>
            {
                _currentOnComplete();
                PlayNextTweener();
            }).StartTween();
        }

        private void PlayNextTweener()
        {
            currentInstruction++;

            if(currentInstruction >= instructions.Count) return;

            PlayCurrentTweener();
        }

        private NTweener CurrentInstruction => instructions[currentInstruction];
    }

    public class NTweener
    {
        public NTweener(Action<float> _action, float _duration)
        {
            mainCallback = _action;
            duration = _duration;
        }

        private Func<bool> tweenMethod;

        public Action<float> mainCallback { private set; get; }
        public Action onCompleteCallback { private set; get; }
        public Action onStartCallBack { private set; get; }

        private Func<float, float> timeConversionMethod = (_t) => _t;

        //Behaviour informations
        private bool pingpong = false;
        private bool loop = false;
        private float duration = 0;
        private float startWaitingDuration = 0;

        private bool paused = false;
        public bool unscaledTime { private set; get; }

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
            if (CheckIfValueRemainsLower(ref startWaitingTime, _deltaTime, startWaitingDuration)) return;

            tweenTime += _deltaTime / duration;

            try
            {
                if (tweenMethod()) CompleteTween();
            }
            catch
            {
                Stop(false);
            }
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
        public NTweener StartTween()
        {
            tweenTime = 0f;
            tweenMethod = pingpong ? () => MainTweenPingPong() : () => MainTween(false);

            NTweenerUpdater.instance.RegisterTweener(this);

            if (onStartCallBack != null) onStartCallBack();

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

        public NTweener UnscaledTime()
        {
            unscaledTime = true;
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

        public NTweener OnStart(Action _callback)
        {
            onStartCallBack = _callback;
            return this;
        }

        #endregion
    }
}