using System.Collections.Generic;

namespace Nazio_LT.Tools.NTween
{
    public class NTweenSequence : NTweenBase<NTweenSequence>
    {
        private List<NTweener> m_sequence = new();

        private ITweenable m_currentTween = null;
        private int m_currentTweenID = 0;

        public void Add(NTweener tweener)
        {
            m_sequence.Add(tweener);
        }

        public NTweenSequence StartTween()
        {
            m_currentTweenID = 0;
            AssignCurrentTween();

            NTweenerUpdater.instance.RegisterTweener(this);

            m_onStart();

            return this;
        }

        protected override void Update(float deltaTime)
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