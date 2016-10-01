# ArborRx

This is expansion plug in of [Arbor2](https://www.assetstore.unity3d.com/jp/#!/content/47081).
I simplify cooperation with [UniRx](https://github.com/neuecc/UniRx).

## getting started

- this repository clone or Download
- Import UniRx from AssetStore https://www.assetstore.unity3d.com/jp/#!/content/17276
- Import Arbor2 from AssetStore https://www.assetstore.unity3d.com/jp/#!/content/47081

## Example

Please define StateBehaviour with a method to return `IObservable<Unit>`

```csharp
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
```

Subscribe to a stream from an editor screen of Arbor when you use ObservaTransition and change.

![arborrx_right](https://cloud.githubusercontent.com/assets/1734002/19012414/52fd653a-87f0-11e6-8b73-a0ce3206779c.gif)
