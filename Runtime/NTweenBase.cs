namespace Nazio_LT.Tools.NTween
{
    public abstract class NTweenBase
    {
        public bool unscaledTime { protected set; get; }

        public abstract bool Update(float _deltaTime);
    }
}