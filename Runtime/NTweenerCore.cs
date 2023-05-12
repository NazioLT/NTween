using UnityEngine;

namespace Nazio_LT.Tools.NTween
{
    internal static class NTweenerCore
    {
        internal const string NTWEENER_NAME = "NTweener";

        internal static void Error(string error) => Debug.Log($"[{NTWEENER_NAME}] : {error}");
        internal static void UnRegisterError(NTweener tweener, string error)
        {
            NTweenerUpdater.instance.UnRegisterTweener(tweener);
            Error($"Cannot call tween.");
        }
    }
}