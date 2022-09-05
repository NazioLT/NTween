using UnityEngine;
using System;
using System.Threading.Tasks;

namespace Nazio_LT.NTween
{
    public class NTweener
    {
        public NTweener(Action<float> _action, float _duration)
        {
            action = _action;
            duration = _duration;

            StartTween();
        }

        private float duration = 0;
        private Action<float> action;

        private bool pingpong = false;

        private bool stopped = false;
        private bool paused = false;

        private async void StartTween()
        {
            await MainTween(false);

            while (pingpong)
            {
                if (stopped) return;

                await MainTween(true);
                await MainTween(false);
            }
        }

        private async Task MainTween(bool _reverse)
        {
            float _endTime = Time.time + duration;
            while (Time.time < _endTime)
            {
                await Task.Yield();

                float _normalizedTime = NormalizedTime(_reverse, duration, _endTime);

                if (stopped) return;
                if (Paused(ref _endTime, duration, _normalizedTime)) continue;

                action(_normalizedTime);
            }
        }

        private float NormalizedTime(bool _reverse, float _duration, float _endTime)
        {
            float _deltaTime = (_endTime - Time.time);
            float _normalizedTime = _deltaTime / duration;

            if (_reverse) return _normalizedTime;
            return 1 - _normalizedTime;
        }

        private bool Paused(ref float _endTime, float _duration, float _normalizedTime)
        {
            if (!paused) return false;

            float _remainingDuration = (1 - _normalizedTime) * duration;
            _endTime = Time.time + _remainingDuration;

            return true;
        }

        #region Orders

        public void Stop() => stopped = true;

        public void Pause() => paused = true;
        public void Resume() => paused = false;
        public void SetPause(bool _value) => paused = _value;
        public void InversePause() => paused = !paused;

        #endregion

        #region Settings

        public NTweener PingPong()
        {
            pingpong = true;
            return this;
        }

        #endregion
    }
}