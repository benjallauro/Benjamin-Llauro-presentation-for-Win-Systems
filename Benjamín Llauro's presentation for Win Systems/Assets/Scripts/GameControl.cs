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

        [SerializeField] private Row[] rows;
        [SerializeField] private string prePrizeText;
        [SerializeField] private float spinTime;
        [SerializeField] private float startDelay;
        [SerializeField] private float minStopDelayRange;
        [SerializeField] private float maxStopDelayRange;

        private int _prizeValue;

        private bool _resultsChecked = true;


        void Update()
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

            
            if (CheckRowsStopped() == true && !_resultsChecked)
            {
                CheckResults();
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
            WaitForSeconds wait = new WaitForSeconds(UnityEngine.Random.Range(minStopDelayRange, maxStopDelayRange));
            while (true)
            {
                foreach (Row current in rows)
                {
                    yield return wait;
                    current.PrepareToStop();
                }
                yield break;
            }
        }
        private IEnumerator CheckTimer()
        {
            WaitForSeconds wait = new WaitForSeconds(spinTime);
            while (true)
            {
                yield return wait;
                /*foreach (Row current in rows)
                    current.PrepareToStop();*/
                StartCoroutine(StopRotationWithDelay());
                yield break;
            }
        }
        private void CheckResults()
        {
            _resultsChecked = true;
            Debug.Log("CHECKED RESULTS");
        }
    }
}