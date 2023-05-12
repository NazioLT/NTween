using System;
using UnityEngine;

namespace Nazio_LT.Tools.NTween
{
    /// <summary>
    /// This class is used to create and manage tweens, which are animations that interpolate between two values over a specified duration. 
    /// The class contains various methods and properties that allow for customization of the tween, such as setting the duration, loop settings, 
    /// and callbacks for when the tween starts or completes.
    /// </summary>
    public class NTweener : NTweenBase<NTweener>
    {
        public NTweener(Action<float> call, float duration)
        {
            m_call = call;
            m_duration = duration;

            m_remapFunc = PingRemap;
            m_updateAction = TweenPreUpdate;
        }

        //Tween settings
        private bool m_loop = false;
        private bool m_pingpong = false;
        private float m_waitingTimeBeforeStart = 0f;
        private Func<float, float> m_timeConversion = (t) => t;

        //Tween main parameters
        private float m_time = 0f;
        private float m_tValue = 0f;
        private bool m_registered = false;
        private bool m_isPing = true;
        private Func<float, float> m_remapFunc = null;
        private Action<float> m_updateAction = null;

        private readonly Action<float> m_call = null;
        private readonly float m_duration = 0f;

        protected override void Update(float deltaTime)
        {
            if (!m_running) return;

            m_updateAction(deltaTime);
        }

        #region Public Commands

        /// <summary>
        /// This function starts a tween animation.
        /// </summary>
        public NTweener StartTween(bool register = true)
        {
            if (m_running)
            {
                NTweenerCore.Error($"Tween is already running.");
                return this;
            }

            if (register)
            {
                NTweenerUpdater.instance.RegisterTweener(this);
                m_registered = true;
            }

            m_dead = false;
            m_running = true;

            m_updateAction = TweenPreUpdate;

            m_onStart();

            return this;
        }

        /// <summary>
        /// This function stops the NTweener animation and optionally calls the OnComplete function.
        /// </summary>
        /// <param name="endCall">The endCall parameter is a boolean value that determines whether or
        /// not to call the OnComplete event when stopping the NTweener..</param>
        public override NTweener Stop(bool endCall = false)
        {
            if (m_dead)
            {
                NTweenerCore.Error($"Tween is already stopped.");
                return this;
            }

            if (m_registered)
            {
                NTweenerUpdater.instance.UnRegisterTweener(this);
                m_registered = false;
            }
            m_dead = true;
            m_running = false;

            if (endCall)
            {
                m_call(m_isPing ? 1 : 0);
                m_onComplete();
            }

            return this;
        }

        public NTweener PingPong(bool value = true)
        {
            m_pingpong = value;

            return this;
        }

        public NTweener Loop(bool value = true)
        {
            m_loop = value;

            return this;
        }

        public NTweener AddTimeCurve(AnimationCurve timeCurve)
        {
            m_timeConversion = (t) => timeCurve.Evaluate(t);

            return this;
        }

        public NTweener AddTimeConversionMethod(Func<float, float> call)
        {
            m_timeConversion = call;

            return this;
        }

        public NTweener WaitBeforeStart(float timeToWait)
        {
            m_waitingTimeBeforeStart = timeToWait;

            return this;
        }

        #endregion

        #region Update Actions

        private void TweenPreUpdate(float deltaTime)
        {
            m_time += deltaTime;

            //Finished waiting time.
            if (m_time <= m_waitingTimeBeforeStart) return;

            m_updateAction = TweenUpdate;
            m_time -= m_waitingTimeBeforeStart;

            m_updateAction(0f);
        }

        private void TweenUpdate(float deltaTime)
        {
            //Time conversion
            m_time += deltaTime;
            try
            {
                m_tValue = m_timeConversion(m_remapFunc(m_time));
            }
            catch
            {
                NTweenerCore.UnRegisterError(this, $"Time Conversion Method not valid.");
            }

            //Execution
            if (m_time >= m_duration)
            {
                OnCompleteTime();
                return;
            }

            try
            {
                m_call(m_tValue);
            }
            catch
            {
                NTweenerCore.UnRegisterError(this, $"Cannot call tween.");
            }
        }

        #endregion

        #region Privates Methods

        private void OnCompleteTime()
        {
            if (m_pingpong && m_isPing)
            {
                m_time = 0f;
                m_isPing = !m_isPing;
                m_remapFunc = m_isPing ? PingRemap : PongRemap;
                return;
            }

            if (m_loop)
            {
                m_time = 0f;
                m_isPing = true;
                m_remapFunc = PingRemap;
                return;
            }

            Stop(true);
        }

        private float PingRemap(float time) => Mathf.InverseLerp(0f, m_duration, time);
        private float PongRemap(float time) => Mathf.InverseLerp(m_duration, 0f, time);

        #endregion
    }
}