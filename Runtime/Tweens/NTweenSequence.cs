using System.Collections.Generic;

namespace Nazio_LT.Tools.NTween
{
    public enum SequenceMode { Sequence = 0, Parallel = 1 }

    public sealed class NTweenSequence : NTweenBase<NTweenSequence>
    {
        public NTweenSequence(SequenceMode mode = SequenceMode.Sequence)
        {
            m_mode = mode;
        }

        private List<NTweener> m_sequence = new();

        private ITweenable m_currentTween = null;
        private ITweenable[] m_Tweens = null;
        private int m_currentTweenID = 0;

        private SequenceMode m_mode;


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
            m_onDie();

            if (endCall) m_onComplete();

            NTweenerUpdater.Instance.UnRegisterTweener(this);

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

            switch (m_mode)
            {
                case SequenceMode.Sequence:
                    m_currentTweenID = 0;
                    AssignCurrentTween();
                    break;
                case SequenceMode.Parallel:
                    m_Tweens = new ITweenable[m_sequence.Count];
                    for (var i = 0; i < m_Tweens.Length; i++)
                    {
                        m_Tweens[i] = m_sequence[i];
                        m_sequence[i].StartTween(false);
                    }
                    break;
            }

            NTweenerUpdater.Instance.RegisterTweener(this);

            m_onStart();

            return this;
        }

        #endregion

        protected override void Update(float deltaTime)
        {
            if (!m_running) return;

            if (m_dead) Stop();

            switch (m_mode)
            {
                case SequenceMode.Sequence:
                    UpdateSequenceMode(deltaTime);
                    break;
                case SequenceMode.Parallel:
                    UpdateParallelMode(deltaTime);
                    break;
            }
        }

        private void UpdateSequenceMode(float deltaTime)
        {
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

        private void UpdateParallelMode(float deltaTime)
        {
            bool allDead = true;
            foreach (ITweenable tween in m_Tweens)
            {
                if (!tween.Dead) allDead = false;

                tween.Update(deltaTime);
            }

            if (allDead) Stop(true);
        }

        private void Next()
        {
            m_currentTweenID++;

            if (m_currentTweenID >= m_sequence.Count)
            {
                NTweenerUpdater.Instance.UnRegisterTweener(this);
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