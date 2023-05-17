using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.NTween
{
    public sealed class NTweenerUpdater : Singleton<NTweenerUpdater>
    {
        private List<ITweenable> m_tweenersToUpdate = new List<ITweenable>();

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying) return;
#endif
            if (Instance == null)
            {
                (new GameObject("NTweener")).AddComponent<NTweenerUpdater>();
                DontDestroyOnLoad(Instance.gameObject);
            }
        }

        private void Update()
        {
            TryUpdate(ref m_tweenersToUpdate, Time.unscaledDeltaTime);
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

        internal void RegisterTweener(ITweenable tweener) => m_tweenersToUpdate.Add(tweener);
        internal void UnRegisterTweener(ITweenable tweener) => m_tweenersToUpdate.Remove(tweener);
    }
}