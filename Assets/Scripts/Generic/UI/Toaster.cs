using DG.Tweening;
using UnityEngine;

namespace Generic.UI
{
    public class Toaster : MonoBehaviour
    {
        [SerializeField]
        private bool m_IsShow;
        public bool IsShow { get { return m_IsShow; } }

        [SerializeField]
        private float m_AnimationDuration;

        [SerializeField]
        private Transform m_PopPosition;

        [SerializeField]
        private Transform m_RetractPosition;

        public void Toggle()
        {
            Debug.Log("1");
            if(m_IsShow) Hide();
            else
            {
                Debug.Log("2");
                Show();
            }
        }

        public void Show()
        {
            Debug.Log($"3");
            m_IsShow = true;
            Pop();
        }

        public void Hide()
        {
            m_IsShow = false;
            Retract();
        }

        protected virtual void Pop()
        {
            Debug.Log($"4 {m_IsShow}");
            transform.DOMove(m_PopPosition.position, m_AnimationDuration);
        }

        protected virtual void Retract()
        {
            transform.DOMove(m_RetractPosition.position, m_AnimationDuration);
        }
    }
}