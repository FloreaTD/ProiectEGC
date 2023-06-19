using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, it will trigger a fail
    /// state with the GameManager.
    /// </summary>
    public class Gate : Spawnable
    {
        const string k_PlayerTag = "Player";

        [SerializeField]
        GateType m_GateType;
        [SerializeField]
        float m_Value;
        [SerializeField]
        RectTransform m_Text;

        bool m_Applied;
        Vector3 m_TextInitialScale;

        enum GateType
        {
            ChangeSpeed,
            ChangeSize,
            GAC,
            PCLP3,
            MN,
            DEEA2,
            PL,
            Sport,
            EGC,
        }

        Inventory m_Inventory;

        /// <summary>
        /// Sets the local scale of this spawnable object
        /// and ensures the Text attached to this gate
        /// does not scale.
        /// </summary>
        /// <param name="scale">
        /// The scale to apply to this spawnable object.
        /// </param>
        public override void SetScale(Vector3 scale)
        {
            // Ensure the text does not get scaled
            if (m_Text != null)
            {
                float xFactor = Mathf.Min(scale.y / scale.x, 1.0f);
                float yFactor = Mathf.Min(scale.x / scale.y, 1.0f);
                m_Text.localScale = Vector3.Scale(m_TextInitialScale, new Vector3(xFactor, yFactor, 1.0f));

                m_Transform.localScale = scale;
            }
        }

        /// <summary>
        /// Reset the gate to its initial state. Called when a level
        /// is restarted by the GameManager.
        /// </summary>
        public override void ResetSpawnable()
        {
            m_Applied = false;
        }

        protected override void Awake()
        {
            base.Awake();

            if (m_Text != null)
            {
                m_TextInitialScale = m_Text.localScale;
            }
        }

        void Start()
        {
            m_Inventory = Inventory.Instance;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag) && !m_Applied)
            {
                ActivateGate();
            }
        }

        void ActivateGate()
        {
            switch (m_GateType)
            {
                case GateType.ChangeSpeed:
                    PlayerController.Instance.AdjustSpeed(m_Value);
                    break;

                case GateType.ChangeSize:
                    PlayerController.Instance.AdjustScale(m_Value);
                    break;

                case GateType.GAC:
                    m_Inventory.GAC();
                    break;

                case GateType.PCLP3:
                    m_Inventory.PCLP3();
                    break;

                case GateType.MN:
                    m_Inventory.MN();
                    break;

                case GateType.DEEA2:
                    m_Inventory.DEEA2();
                    break;

                case GateType.PL:
                    m_Inventory.PL();
                    break;

                case GateType.Sport:
                    m_Inventory.Sport();
                    break;

                case GateType.EGC:
                    m_Inventory.EGC();
                    break;

                default:
                    Debug.LogWarning("GateType not recognized: " + m_GateType);
                    break;
            }

            m_Applied = true;
        }
    }
}
