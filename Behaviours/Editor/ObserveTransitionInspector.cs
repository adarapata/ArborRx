using UnityEditor;
using System.Linq;
using UniRx;
using ArborRx;

namespace ArborRxEditor
{
    [CustomEditor(typeof(ObserveTransition))]
    public class ObserveTransitionInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var transition = serializedObject.targetObject as ObserveTransition;
            var behaviours = transition.state.behaviours
                                .Where(b => !b.GetType().Equals(typeof(ObserveTransition))).ToList();
            if (behaviours.Count > 0)
            {
                if (transition.target == null) { transition.target = behaviours.First(); }
                int index = EditorGUILayout.Popup("BehaviourName", behaviours.FindIndex(b => transition.target.Equals(b)), behaviours.Select(o => o.GetType().Name).ToArray());
                transition.target = behaviours[index];
                var methods = transition.target.GetType().GetMethods();
                var obs = methods.Where(m => m.ReturnType == typeof(IObservable<Unit>)).ToList();
                if (obs.Count > 0)
                {
                    int methodIndex = EditorGUILayout.Popup("ObservableMethod", obs.FindIndex(o => o.Name == transition.methodName), obs.Select(o => o.Name).ToArray());
                    if (methodIndex < 0) { methodIndex = 0; }
                    transition.methodName = obs[methodIndex].Name;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}