using Arbor;
using UniRx;
using UniRx.Triggers;

namespace ArborRx
{
    public class ObservableStateBehaviour : StateBehaviour
    {
        private Subject<Unit> stateBeginStream = new Subject<Unit>();

        public IObservable<Unit> stateBeginAsObservable
        {
            get { return stateBeginStream.AsObservable(); }
        }

        private Subject<Unit> stateEndStream = new Subject<Unit>();

        public IObservable<Unit> stateEndAsObservable
        {
            get { return stateEndStream.AsObservable(); }
        }

        public Subject<Unit> stateAwakeStream = new Subject<Unit>();

        public IObservable<Unit> stateAwakeAsObservable
        {
            get { return stateAwakeStream.AsObservable(); }
        }

        public IObservable<Unit> updateAsObservable
        {
            get
            {
                return this.UpdateAsObservable()
                .SkipUntil(stateBeginAsObservable)
                .TakeUntil(stateEndAsObservable)
                .Repeat();
            }
        }

        public override void OnStateBegin()
        {
            stateBeginStream.OnNext(default(Unit));
        }

        public override void OnStateEnd()
        {
            stateEndStream.OnNext(default(Unit));
        }
    }
}