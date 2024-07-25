using UnityEngine;
using UnityEngine.Rendering;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(UIConfig), menuName = "Evgenoid/UIConfig")]
    public class UIConfig : ScriptableObject
    {
        public SerializedDictionary<UIViewNames.VewNames, GameObject> viewsMap;
    }
}
