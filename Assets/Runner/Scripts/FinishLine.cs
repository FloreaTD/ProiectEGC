using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class FinishLine : Spawnable
    {
        const string k_PlayerTag = "Player";

        private Inventory inventory;
        private int nrGold;
        public int crediteCastig;

        void OnTriggerEnter(Collider col)
        {
            inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                nrGold = inventory.returnTemGold();
            }
            if (col.CompareTag(k_PlayerTag) && nrGold > crediteCastig)
            {
                GameManager.Instance.Win();
                Debug.Log(nrGold);
            }
            else
            {
                GameManager.Instance.Lose();
            }
        }
    }
}
