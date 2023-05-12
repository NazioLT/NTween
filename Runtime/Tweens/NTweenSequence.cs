using System.Collections.Generic;

namespace Nazio_LT.Tools.NTween
{
    public class NTweenSequence : NTweenBase<NTweenSequence>
    {
        private List<NTweener> m_sequence = new();

        private ITweenable m_currentTween = null;
        private int m_currentTweenID = 0;

        #region Public Commands

        public override NTweenSequence Stop(bool endCall = false)
        {
            if (m_dead)
            {
                NTweenerCore.Error($"TweenSequence is already stopped.");
                return this;
            }

            m_running = false;
            m_dead = true;

            if(endCall) m_onComplete();

            NTweenerUpdater.instance.UnRegisterTweener(this);

            return this;
        }

        public NTweenSequence Add(NTweener tweener)
        {
            m_sequence.Add(tweener);

            return this;
        }

        public NTweenSequence StartTween()
        {
            if (m_running)
            {
                NTweenerCore.Error($"TweenSequence is already running.");
                return this;
            }

            m_running = true;
            m_dead = false;

            m_currentTweenID = 0;
            AssignCurrentTween();

            NTweenerUpdater.instance.RegisterTweener(this);

            m_onStart();

            return this;
        }

        #endregion

        protected override void Update(float deltaTime)
        {
            if(!m_running) return;

            if(m_dead) Stop();

            if (m_currentTween == null)
            {
                Next();
                return;
            }

            if (m_currentTween.Dead)
            {
                Next();
                return;
            }

            m_currentTween.Update(deltaTime);
        }

        private void Next()
        {
            m_currentTweenID++;

            if (m_currentTweenID >= m_sequence.Count)
            {
                NTweenerUpdater.instance.UnRegisterTweener(this);
                return;
            }

            AssignCurrentTween();
        }

        private void AssignCurrentTween()
        {
            m_currentTween = m_sequence[m_currentTweenID];
            m_sequence[m_currentTweenID].StartTween(false);
        }
    }
}