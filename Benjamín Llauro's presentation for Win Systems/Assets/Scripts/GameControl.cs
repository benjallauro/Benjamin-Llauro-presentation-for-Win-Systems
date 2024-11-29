using System;
using System.Collections;
using System.Collections.Generic;
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

        private int _prizeValue;

        private bool _resultsChecked = false;


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
                startButtonPressed();
            }
        }
        private void CheckResults()
        {
            _resultsChecked = true;
            Debug.Log("CHECKED RESULTS");
        }
    }
}