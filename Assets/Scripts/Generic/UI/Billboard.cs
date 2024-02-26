using UnityEngine;

namespace Generic.UI
{
    public class Billboard : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}