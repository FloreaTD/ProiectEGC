using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace HyperCasual.Runner
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        [SerializeField]
        float m_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }

        void Update()
        {
            if (PlayerController.Instance == null)
            {
                return;
            }

            float horizontalInput = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();
            m_InputPosition.x = horizontalInput * Screen.width;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }

            if (m_HasInput)
            {
                float normalizedDeltaPosition = m_InputPosition.x / Screen.width * m_InputSensitivity;
                PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition);
            }
            else
            {
                PlayerController.Instance.CancelMovement();
            }
        }
    }
}
