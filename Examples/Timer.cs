using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ArborRx.Examples
{
    public class Timer : ObservableStateBehaviour
    {
        public float time;
        private Subject<Unit> timeCountStream = new Subject<Unit>();

        void Awake()
        {
            stateBeginAsObservable.Subscribe(_ => {
                Observable.Timer(System.TimeSpan.FromSeconds(time))
                    .TakeUntil(stateEndAsObservable)
                    .Subscribe(__ => timeCountStream.OnNext(Unit.Default));
            });
        }

        public IObservable<Unit> timeCountAsObservable()
        {
            return timeCountStream.AsObservable();
        }
    }
}