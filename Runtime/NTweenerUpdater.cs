using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.NTween.Internal
{
    public sealed class NTweenerUpdater : Singleton<NTweenerUpdater>
    {
        private List<NTweener> tweenersToUpdate = new List<NTweener>();

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

        private void FixedUpdate()
        {
            TryUpdate(ref tweenersToUpdate, Time.fixedDeltaTime);
        }

        private void TryUpdate(ref List<NTweener> _tweenersToUpdate, float _deltaTime)
        {
            if(_tweenersToUpdate == null || _tweenersToUpdate.Count <= 0) return;
            foreach (var _tweener in _tweenersToUpdate)
            {
                if(_tweener == null) continue;
                _tweener.Update(_deltaTime);
            }
        }

        public void RegisterTweener(NTweener _tweener) => tweenersToUpdate.Add(_tweener);
        public void UnRegisterTweener(NTweener _tweener) => tweenersToUpdate.Remove(_tweener);
    }
}