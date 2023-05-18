using UnityEngine;

namespace Nazio_LT.Tools.NTween
{
    public enum NTweenType
    {
        NTMoveTo = 0,
        NTMove = 1,
        NTRotateTo = 2,
        NTRotate = 3,
        NTScaleTo = 4
    }

    [System.Serializable]
    public class NTweenerHandler
    {
        //Editor variables
        [SerializeField] private bool m_open = false;
        [SerializeField] private bool m_otherPropsOpen = false;

        [SerializeField] private NTweenType m_type = NTweenType.NTMove;
        [SerializeField] private Transform m_transform = null;
        [SerializeField] private Vector3 m_target = Vector3.zero;
        [SerializeField] private Vector3 m_delta = Vector3.zero;
        [SerializeField] private float m_duration = 0f;

        [SerializeField] private bool m_loop = false;
        [SerializeField] private bool m_pingpong = false;

        private NTweener m_tweener = null;

        public NTweener StartTween()
        {
            m_tweener = GetNTweener();

            if (m_loop) m_tweener.Loop();
            if (m_pingpong) m_tweener.PingPong();

            return m_tweener.StartTween();
        }

        public NTweener Stop() => m_tweener.Stop();

        public NTweener Pause(bool value = true) => m_tweener.Pause(value);

        private NTweener GetNTweener()
        {
            switch (m_type)
            {
                case NTweenType.NTMoveTo:
                    return m_transform.NTMoveTo(m_target, m_duration);

                case NTweenType.NTMove:
                    return m_transform.NTMove(m_delta, m_duration);

                case NTweenType.NTRotateTo:
                    return m_transform.NTRotateTo(m_target, m_duration);

                case NTweenType.NTRotate:
                    return m_transform.NTRotate(m_delta, m_duration);

                case NTweenType.NTScaleTo:
                    return m_transform.NTScaleTo(m_target, m_duration);
            }

            return null;
        }
    }
}
