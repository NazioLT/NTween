using System;

namespace Nazio_LT.Tools.NTween
{
    public abstract class NTweenBase
    {
        public bool unscaledTime { protected set; get; }

        public Action callback { protected set; get; }
        public Action onStartCallBack { protected set; get; }
    }
}