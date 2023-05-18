using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.NTween
{
    public sealed class NTweenerUpdater : Singleton<NTweenerUpdater>
    {
        private List<ITweenable> m_tweenersToUpdate = new List<ITweenable>();
        private List<ITweenable> m_tweenersToFixedUpdate = new List<ITweenable>();

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying) return;
#endif
            if (Instance == null)
            {
                NTweenerUpdater instance = (new GameObject("NTweener")).AddComponent<NTweenerUpdater>();
                DontDestroyOnLoad(Instance.gameObject);
                instance.SetInstance(instance);
            }
        }

        private void Update()
        {
            if (Time.frameCount > 1) TryUpdate(ref m_tweenersToUpdate, Time.unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            if (Time.frameCount > 1) TryUpdate(ref m_tweenersToFixedUpdate, Time.fixedUnscaledDeltaTime);
        }

        private void TryUpdate(ref List<ITweenable> tweenersToUpdate, float unscaledDeltaTime)
        {
            if (tweenersToUpdate == null || tweenersToUpdate.Count <= 0) return;

            for (int i = 0; i < tweenersToUpdate.Count; i++)
            {
                ITweenable tweener = tweenersToUpdate[i];

                if (tweener == null || tweener.Dead)
                {
                    tweenersToUpdate.RemoveAt(i);
                    continue;
                }

                float deltaTime = unscaledDeltaTime * (tweener.UnscaledTime ? 1f : Time.timeScale);

                tweener.Update(deltaTime);
            }
        }

        internal void RegisterTweener(ITweenable tweener) => (tweener.IsInFixedUpdate ? m_tweenersToFixedUpdate : m_tweenersToUpdate).Add(tweener);
        internal void UnRegisterTweener(ITweenable tweener) => (tweener.IsInFixedUpdate ? m_tweenersToFixedUpdate : m_tweenersToUpdate).Remove(tweener);
    }
}