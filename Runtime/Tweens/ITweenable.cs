namespace Nazio_LT.Tools.NTween
{
    internal interface ITweenable
    {
        internal void Update(float deltaTime);

        bool UnscaledTime { get; }
        bool Dead { get; }
    }
}