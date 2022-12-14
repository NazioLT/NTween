using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.NTween.Internal
{
    public sealed class NTweenerUpdater : Singleton<NTweenerUpdater>
    {
        private List<NTweenBase> tweenersToUpdate = new List<NTweenBase>();

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

        protected override void Awake() { }

        private void Update()
        {
            TryUpdate(ref tweenersToUpdate, Time.unscaledDeltaTime);
        }

        private void TryUpdate(ref List<NTweenBase> _tweenersToUpdate, float _unscaledDeltaTime)
        {
            if (_tweenersToUpdate == null || _tweenersToUpdate.Count <= 0) return;

            for (int i = 0; i < _tweenersToUpdate.Count; i++)
            {
                NTweenBase _tweener = _tweenersToUpdate[i];

                if (_tweener == null)
                {
                    _tweenersToUpdate.RemoveAt(i);
                    continue;
                }

                float _deltaTime = _unscaledDeltaTime * (_tweener.unscaledTime ? 1f : Time.timeScale);

                _tweener.Update(_deltaTime);
            }
        }

        public void RegisterTweener(NTweenBase _tweener) => tweenersToUpdate.Add(_tweener);
        public void UnRegisterTweener(NTweenBase _tweener) => tweenersToUpdate.Remove(_tweener);
    }
}