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

        private async void StartTween()
        {
            await MainTween(false);

            while(pingpong)
            {
                await MainTween(true);
                await MainTween(false);
            }
        }

        private async Task MainTween(bool _reverse)
        {
            float _endTime = Time.time + duration;
            while (Time.time < _endTime)
            {
                float _normalizedTime = NormalizedTime(_reverse, duration, _endTime);

                action(_normalizedTime);

                await Task.Yield();
            }
        }

        private float NormalizedTime(bool _reverse, float _duration, float _endTime)
        {
            float _normalizedTime = (_endTime - Time.time) / duration;

            if (_reverse) return _normalizedTime;
            return 1 - _normalizedTime;
        }

        #region Settings

        public NTweener PingPong()
        {
            pingpong = true;
            return this;
        }

        #endregion
    }
}