using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ArborRx.Examples
{
    public class Onkey : ObservableStateBehaviour
    {
        public KeyCode keycode;

        Subject<Unit> onKeyStream = new Subject<Unit>();
        Subject<Unit> onKeyDownStream = new Subject<Unit>();
        Subject<Unit> onKeyUpStream = new Subject<Unit>();

        public IObservable<Unit> OnKeyAsObservable()
        {
            return onKeyStream.AsObservable();
        }

        public IObservable<Unit> OnKeyDownAsObservable()
        {
            return onKeyDownStream.AsObservable();
        }

        public IObservable<Unit> OnKeyUpObservable()
        {
            return onKeyUpStream.AsObservable();
        }

        void Awake()
        {
            updateAsObservable.Where(_ => Input.GetKey(keycode)).Subscribe(_ => onKeyStream.OnNext(Unit.Default));
            updateAsObservable.Where(_ => Input.GetKeyDown(keycode)).Subscribe(_ => onKeyDownStream.OnNext(Unit.Default));
            updateAsObservable.Where(_ => Input.GetKeyUp(keycode)).Subscribe(_ => onKeyUpStream.OnNext(Unit.Default));
        }
    }
}