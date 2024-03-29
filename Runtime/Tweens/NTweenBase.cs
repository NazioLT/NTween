using System;

namespace Nazio_LT.Tools.NTween
{
    public abstract class NTweenBase<T> : ITweenable where T : NTweenBase<T>
    {
        //Tween settings
        private bool m_unscaledTime = false;
        private bool m_fixedUpdate = false;

        //Tween main parameters
        protected bool m_dead = false;
        protected bool m_running = false;

        protected Action m_onStart = () => { };
        protected Action m_onComplete = () => { };
        protected Action m_onDie = () => { };

        #region Public Commands

        public abstract T Stop(bool endCall = false);

        public T OnComplete(System.Action call)
        {
            m_onComplete = call;

            return (T)this;
        }

        public T OnStart(System.Action call)
        {
            m_onStart = call;

            return (T)this;
        }

        public T Pause(bool value)
        {
            m_running = !value;

            return (T)this;
        }

        public T UnScaledTime(bool value = true)
        {
            m_unscaledTime = value;

            return (T)this;
        }

        public T FixedUpdate(bool value = true)
        {
            m_fixedUpdate = value;

            return (T)this;
        }

        public T OnDie(Action call)
        {
            m_onDie = call;

            return (T)this;
        }

        #endregion

        #region Commands Extensions

        public T Pause() => Pause(m_running);

        #endregion

        void ITweenable.Update(float deltaTime) => Update(deltaTime);
        protected abstract void Update(float deltaTime);

        public bool UnscaledTime => m_unscaledTime;
        public bool Dead => m_dead;
        public bool Running => m_running;
        public bool IsInFixedUpdate => m_fixedUpdate;
    }
}