using UnityEngine;
using TMPro;
namespace Artemito { 
    public class CursorTarget : MonoBehaviour
    {
        public TextMeshProUGUI text;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.text = "";
        }

    }
}
