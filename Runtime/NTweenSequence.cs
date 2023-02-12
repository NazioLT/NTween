using UnityEngine;
using System;
using System.Collections.Generic;
using Nazio_LT.Tools.NTween.Internal;

namespace Nazio_LT.Tools.NTween
{
    public class NTweenSequence
    {
        private List<NTweener> tweeners = new List<NTweener>();
        private int currentTweenID = 0;

        private NTweener current;

        public NTweenSequence AddTween(NTweener _tween)
        {
            tweeners.Add(_tween);
            return this;
        }

        public NTweenSequence StartTween()
        {
            if(tweeners.Count == 0) return this;

            currentTweenID = 0;
            current = tweeners[0].OnComplete(Next).StartTween();
            return this;
        }

        private void Next()
        {
            currentTweenID++;
            if(currentTweenID >= tweeners.Count) return;//Finish

            current = tweeners[currentTweenID];
            current.OnComplete(Next).StartTween();
        }
    }
}