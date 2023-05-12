using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.NTween
{
    public sealed class NTweenerUpdater : Singleton<NTweenerUpdater>
    {
        private List<NTweener> m_tweenersToUpdate = new List<NTweener>();

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying) return;
#endif
            if (instance == null)
            {
                instance = (new GameObject("NTweener")).AddComponent<NTweenerUpdater>();
                DontDestroyOnLoad(instance.gameObject);
            }
        }

        private void Update()
        {
            TryUpdate(ref m_tweenersToUpdate, Time.unscaledDeltaTime);
        }

        private void TryUpdate(ref List<NTweener> tweenersToUpdate, float unscaledDeltaTime)
        {
            if (tweenersToUpdate == null || tweenersToUpdate.Count <= 0) return;

            for (int i = 0; i < tweenersToUpdate.Count; i++)
            {
                NTweener tweener = tweenersToUpdate[i];

                if (tweener == null || tweener.Dead)
                {
                    tweenersToUpdate.RemoveAt(i);
                    continue;
                }

                float deltaTime = unscaledDeltaTime * (tweener.UnscaledTime ? 1f : Time.timeScale);

                tweener.Update(deltaTime);
            }
        }

        public void RegisterTweener(NTweener tweener) => m_tweenersToUpdate.Add(tweener);
        public void UnRegisterTweener(NTweener tweener) => m_tweenersToUpdate.Remove(tweener);
    }
}