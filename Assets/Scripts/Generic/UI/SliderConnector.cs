using UnityEngine;
using UnityEngine.UI;

namespace Generic.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderConnector : MonoBehaviour
    {
        [SerializeField]
        private FloatReference _NormalisedValue;

        private Slider _Slider;
        public Slider Slider => _Slider;

        void Awake()
        {
            _Slider = GetComponent<Slider>();
            _NormalisedValue.OnVariableChanged += ValueChangeHandler;
        }

        private void ValueChangeHandler(float value)
        {
            _Slider.value = value;
        }
    }
}