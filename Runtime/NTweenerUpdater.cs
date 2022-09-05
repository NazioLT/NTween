using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.NTween;

namespace Nazio_LT.NTween.Internal
{
    public class NTweenerUpdater : MonoBehaviour
    {
        public static NTweenerUpdater instance { private set; get; }

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