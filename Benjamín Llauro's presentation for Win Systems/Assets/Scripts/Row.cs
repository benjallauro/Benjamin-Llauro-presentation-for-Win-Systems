using System.Collections;
using UnityEngine;

namespace SlotMachine
{
    public class Row : MonoBehaviour
    {
        private float _timeInterval;

        public bool rowStopped;
        public string stoppedSlot;
        [SerializeField] private float timeToStop;

        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform comparisonToReset;
        float startPositionY;

        private void Start()
        {
            rowStopped = true;
            startPositionY = transform.position.y;
            GameControl.startButtonPressed += StartRotating;
        }
        private void StartRotating()
        {
            stoppedSlot = "";
            rowStopped = false;

            StartCoroutine(CheckTimer()); // Temporal call

            //StopAllCoroutines();
            //StartCoroutine(Rotate());
        }

        private void FixedUpdate()
        {
            if(!rowStopped)
            {
                if (transform.position.y < comparisonToReset.position.y) //11.5f is the position to the first real piece.
                {
                    transform./*Local*/position = new Vector3(transform.position.x, startPositionY, transform.position.z);
                }
                else
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + scrollSpeed * Time.deltaTime, transform.position.z);
            }
        }
        IEnumerator CheckTimer()
        {
            WaitForSeconds wait = new WaitForSeconds(timeToStop);
            while (true)
            {
                yield return wait;
                rowStopped = true;
                StopAllCoroutines();
            }
        }

        private IEnumerator Rotate() //The tutorial had A LOT of hardcoding here, Check and fix before sending!!.
        {
            rowStopped = false;
            _timeInterval = 0.025f;

            for(int i = 0; i < 1000; i++)
            {
                if (transform.position.y <= -2.5f) //11.5f is the position to the first real piece.
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, /*transform.position.y -*/ 11.5f /*- 0.25f*/, transform.localPosition.z);
                    yield return new WaitForSeconds(_timeInterval);
                }
               else
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f /*- 0.25f*/, transform.position.z);
                
                yield return new WaitForSeconds(_timeInterval);
            }


            //We adjust the slot so it doesnt stop mid icons.
            /*
            _randomValue = Random.Range(60, 100);
            
            switch(_randomValue % 3)
            {
                case 1:
                    _randomValue += 2;
                    break;
                case 2:
                    _randomValue += 1;
                    break;
            }

            for(int i = 0; i < _randomValue; i++)
            {
                if(transform.position.y = new Vector2.)
            }
            */
        }






    }
}