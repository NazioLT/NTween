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

    public enum ObjectType
    {
        Transform = 0,
        Rigidbody = 1,
        Rigidbody2D = 2
    }

    [System.Serializable]
    public class NTweenerHandler
    {
        //Editor variables
        [SerializeField] private bool m_open = false;
        [SerializeField] private bool m_otherPropsOpen = false;

        [SerializeField] private NTweenType m_type = NTweenType.NTMove;
        [SerializeField] private ObjectType m_objectType = ObjectType.Transform;
        [SerializeField] private Transform m_transform = null;
        [SerializeField] private Rigidbody m_rigidbody = null;
        [SerializeField] private Rigidbody2D m_rigidbody2D = null;
        [SerializeField] private Vector3 m_target = Vector3.zero;
        [SerializeField] private Vector3 m_delta = Vector3.zero;
        [SerializeField] private float m_duration = 0f;


        [SerializeField] private bool m_loop = false;
        [SerializeField] private bool m_pingpong = false;
        [SerializeField] private bool m_hasAnimationCurve = false;
        [SerializeField] private AnimationCurve m_curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private NTweener m_tweener = null;

        public NTweener StartTween()
        {
            m_tweener = GetNTweener();

            if (m_loop) m_tweener.Loop();
            if (m_pingpong) m_tweener.PingPong();
            if (m_hasAnimationCurve) m_tweener.AddAnimationCurve(m_curve);

            return m_tweener.StartTween();
        }

        public NTweener Stop() => m_tweener.Stop();

        public NTweener Pause(bool value = true) => m_tweener.Pause(value);

        private NTweener GetNTweener()
        {
            switch (m_type)
            {
                case NTweenType.NTMoveTo:
                    return NTMoveTo();

                case NTweenType.NTMove:
                    return NTMove();

                case NTweenType.NTRotateTo:
                    return m_transform.NTRotateTo(m_target, m_duration);

                case NTweenType.NTRotate:
                    return m_transform.NTRotate(m_delta, m_duration);

                case NTweenType.NTScaleTo:
                    return m_transform.NTScaleTo(m_target, m_duration);
            }

            return null;
        }

        private NTweener NTMoveTo()
        {
            switch (m_objectType)
            {
                case ObjectType.Transform:
                    return m_transform.NTMoveTo(m_target, m_duration);

                case ObjectType.Rigidbody:
                    return m_rigidbody.NTMoveTo(m_target, m_duration).FixedUpdate();

                case ObjectType.Rigidbody2D:
                    return m_rigidbody2D.NTMoveTo(m_target, m_duration).FixedUpdate();
            }

            return null;
        }

        private NTweener NTMove()
        {
            switch (m_objectType)
            {
                case ObjectType.Transform:
                    return m_transform.NTMove(m_delta, m_duration);

                case ObjectType.Rigidbody:
                    return m_rigidbody.NTMove(m_delta, m_duration).FixedUpdate();

                case ObjectType.Rigidbody2D:
                    return m_rigidbody2D.NTMove(m_delta, m_duration).FixedUpdate();
            }

            return null;
        }
    }
}
