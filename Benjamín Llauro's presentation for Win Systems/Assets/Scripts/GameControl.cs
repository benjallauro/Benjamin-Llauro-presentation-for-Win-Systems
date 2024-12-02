using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachine
{
    public class GameControl : MonoBehaviour
    {
        [Header("--- Prize text ---")]
        [SerializeField] private string prePrizeText;
        [SerializeField] private TextMeshProUGUI prizeText;

        [Header("--- Result ---")]
        [SerializeField] private ResultManager resultManager;

        [Header("--- Rows ---")]
        [SerializeField] private Row[] rows;
        

        [Header("--- Spin time ---")]
        [SerializeField] private float minSpinTime;
        [SerializeField] private float maxSpinTime;

        [Header("--- Start and stop delay ---")]
        [SerializeField] private float startDelay;
        [SerializeField] private float minStopDelayRange;
        [SerializeField] private float maxStopDelayRange;

        [Header("Spin button")]
        [SerializeField] private Button spinButton;

        private int _prizeValue;

        private bool _resultsChecked = true;

        private bool _machineRunning = false;

        #region Unity Methods
        private void Update()
        {
            foreach(Row current in rows)
            {
                if(current.GetStopped())
                {
                    _prizeValue = 0;
                    break;
                }
            }
            
            if (CheckRowsStopped() == true && !_resultsChecked && !_machineRunning)
            {
                _resultsChecked = true;
                prizeText.enabled = true;
                _prizeValue = resultManager.CheckResult(rows);
                prizeText.text = prePrizeText + " " + _prizeValue;
                spinButton.interactable = true;
            }
        }
        #endregion

        #region Private Methods
        private bool CheckRowsStopped()
        {
            foreach (Row current in rows)
            {
                if (!current.GetStopped())
                    return false;
            }
            return true;
        }
        private IEnumerator StartRotationsWithDelay()
        {
            WaitForSeconds wait = new WaitForSeconds(startDelay);
            while (true)
            {
                foreach (Row current in rows)
                {
                    yield return wait;
                    current.StartRotating();
                }
                StartCoroutine(CheckTimer());
                yield break;
            }
        }
        private IEnumerator StopRotationWithDelay()
        {
            while (true)
            {
                foreach (Row current in rows)
                {
                    WaitForSeconds wait = new WaitForSeconds(UnityEngine.Random.Range(minStopDelayRange, maxStopDelayRange));
                    yield return wait;
                    current.PrepareToStop();
                }
                _machineRunning = false;
                yield break;
            }
        }
        private IEnumerator CheckTimer()
        {
            WaitForSeconds wait = new WaitForSeconds(UnityEngine.Random.Range(minSpinTime, maxSpinTime));
            while (true)
            {
                yield return wait;
                StartCoroutine(StopRotationWithDelay());
                yield break;
            }
        }
        #endregion

        #region Public Methods
        public void PressStartButton()
        {
            if(CheckRowsStopped())
            {
                _machineRunning = true;
                StopAllCoroutines();
                StartCoroutine(StartRotationsWithDelay());
                _resultsChecked = false;
                spinButton.interactable = false;
            }
        }
        #endregion
    }
}