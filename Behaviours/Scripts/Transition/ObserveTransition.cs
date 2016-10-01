using Arbor;
using UniRx;

namespace ArborRx
{
    [BuiltInBehaviour]
    public class ObserveTransition : ObservableStateBehaviour
    {
        public StateBehaviour target;
        public string methodName = "";
        public StateLink next;

        void Awake()
        {
            stateBeginAsObservable.Subscribe(_ =>
            {
                IObservable<Unit> obs = target.GetType().GetMethod(methodName).Invoke(target, null) as IObservable<Unit>;
                var subscribe = obs.Subscribe(__ => Transition(next));
                stateEndAsObservable.Take(1).Subscribe(__ => subscribe.Dispose());
            });
        }
    }
}