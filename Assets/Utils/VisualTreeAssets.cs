using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 
    

[CreateAssetMenu(fileName = "VisualTreeAssets", menuName = "ScriptableObjects/VisualTreeAssets")]
public class VisualTreeAssets : ScriptableObject
{
    static VisualTreeAssets _instance;
    public static VisualTreeAssets Instance { 
    get { if (_instance == null)
                _instance = Resources.Load("VisualTreeAssets") as VisualTreeAssets;
            return _instance;
        }
    }

    [SerializeField]public VisualTreeAsset customListView;
    [SerializeField]public VisualTreeAsset interactionItem;
    [SerializeField]public VisualTreeAsset inventoryView;
    [SerializeField]public VisualTreeAsset characterInteraction;
    [SerializeField]public VisualTreeAsset propertyInteraction;
    [SerializeField] public VisualTreeAsset dialogPropertyField;
    [SerializeField] public VisualTreeAsset inventoryPropertyField;
    [SerializeField] public VisualTreeAsset propertyItem;
        [Header("Interactions")]
    [SerializeField] public VisualTreeAsset changeProperty;
    [SerializeField] public VisualTreeAsset compareProperty;
}
}
