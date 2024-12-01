using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SlotMachine
{
    public class GameControl : MonoBehaviour
    {
        public static event Action startButtonPressed = delegate { };

        [SerializeField] private TextMeshProUGUI prizeText;
        [SerializeField] private ResultManager resultManager;


        [SerializeField] private Row[] rows;
        [SerializeField] private string prePrizeText;
        [SerializeField] private float minSpinTime;
        [SerializeField] private float maxSpinTime;
        [SerializeField] private float startDelay;
        [SerializeField] private float minStopDelayRange;
        [SerializeField] private float maxStopDelayRange;

        private int _prizeValue;

        private bool _resultsChecked = true;

        private bool _machineRunning = false;


        private void Update()
        {
            foreach(Row current in rows)
            {
                if(current.rowStopped)
                {
                    _prizeValue = 0;
                    prizeText.enabled = false;
                    break;
                }
            }

            
            if (CheckRowsStopped() == true && !_resultsChecked && !_machineRunning)
            {
                resultManager.CheckResult(rows);
                _resultsChecked = true;
                prizeText.enabled = true;
                prizeText.text = prePrizeText + " " + _prizeValue;
            }
        }
        private bool CheckRowsStopped()
        {
            foreach (Row current in rows)
            {
                if (!current.rowStopped)
                    return false;
            }
            return true;
        }
        public void PressStartButton()
        {
            if(CheckRowsStopped())
            {
                //startButtonPressed();
                //foreach (Row current in rows)
                //current.StartRotating();
                _machineRunning = true;
                StopAllCoroutines();
                StartCoroutine(StartRotationsWithDelay());
                _resultsChecked = false;
            }
        }
        private IEnumerator StartRotationsWithDelay()
        {
            WaitForSeconds wait = new WaitForSeconds(startDelay);
            while(true)
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
    }
}