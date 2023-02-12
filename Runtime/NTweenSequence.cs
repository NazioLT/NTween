using UnityEngine;
using System;
using System.Collections.Generic;

namespace Nazio_LT.Tools.NTween
{
    public class NTweenSequence : NTweenBase
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
            if (onStartCallBack != null) onStartCallBack();

            if (tweeners.Count == 0) return this;

            currentTweenID = 0;
            LaunchCurrent();
            return this;
        }

        /// <summary>Pass to the next tween, finish if no tweens remains.</summary>
        private void Next()
        {
            currentTweenID++;
            if (currentTweenID >= tweeners.Count)
            {
                Finish();
                return;//Finish
            }

            LaunchCurrent();
        }

        private void Finish()
        {
            if (callback != null) callback();
        }

        private void LaunchCurrent() => current = tweeners[currentTweenID].OnComplete(Next).StartTween();

        #region Orders

        public void Pause() => current.Pause();
        public void Resume() => current.Resume();

        public void Stop(bool _onCompleteCallback) => current.Stop(_onCompleteCallback);


        public NTweenSequence OnStart(Action _callback)
        {
            onStartCallBack += _callback;
            return this;
        }

        public NTweenSequence OnComplete(Action _callback)
        {
            callback += _callback;
            return this;
        }

        #endregion
    }
}