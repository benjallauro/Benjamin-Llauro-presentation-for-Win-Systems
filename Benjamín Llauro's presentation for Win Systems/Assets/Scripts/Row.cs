using System.Collections;
using UnityEngine;

namespace SlotMachine
{
    public class Row : MonoBehaviour
    {
        private float _timeInterval;
        private bool _goingToStop = false;


        public bool rowStopped;
        public string stoppedSlot;
        private SlotIcon _finalTarget;

        [SerializeField] private SlotIcon[] realIcons;
        [SerializeField] private SlotIcon ghostIconToLoop;
        [SerializeField] private float timeToStop;

        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform comparisonToReset;
        [SerializeField] private Transform middlePoint;
        Vector3 startPosition;

        private void Start()
        {
            rowStopped = true;
            startPosition = transform.position;
            GameControl.startButtonPressed += StartRotating;
        }
        private void StartRotating()
        {
            stoppedSlot = "";
            rowStopped = false;
            StopAllCoroutines();
            StartCoroutine(CheckTimer()); // Temporal call

            //StopAllCoroutines();
            //StartCoroutine(Rotate());
        }

        private void FixedUpdate()
        {
            if(!rowStopped)
            {
                if (ghostIconToLoop.transform.position.y <= comparisonToReset.position.y)
                {
                    transform.position = startPosition;
                }
                else
                {
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + scrollSpeed * Time.deltaTime, transform.position.z);
                    if(_goingToStop)
                    {
                        if (_finalTarget.transform.position.y <= middlePoint.position.y)
                        {
                            rowStopped = true;
                            _goingToStop = false;
                        }
                    }
                }
            }
        }
        IEnumerator CheckTimer()
        {
            WaitForSeconds wait = new WaitForSeconds(timeToStop);
            while (true)
            {
                yield return wait;
                _goingToStop = true;
                _finalTarget = GetClosestHigherIconFromCenter();
                StopAllCoroutines();
            }
        }

        private SlotIcon GetClosestHigherIconFromCenter()
        {
            SlotIcon closestIcon = realIcons[0];
            float closestDistance = Vector3.Distance(closestIcon.transform.position, middlePoint.position); //  closestIcon.transform.position.y - middlePoint.position.y;
            float currentDistance;
            for(int i = 0; i < realIcons.Length; i++)
            {
                currentDistance = Vector3.Distance(realIcons[i].transform.position, middlePoint.position);
                if (currentDistance < closestDistance && realIcons[i].transform.position.y > middlePoint.position.y)
                {
                    Debug.Log("ICONO MAS CERCANO: " + realIcons[i].gameObject.name + ". Posicion en Y:" + realIcons[i].transform.position.y);
                    Debug.Log("Middlepoint Y: " + middlePoint.position.y);
                    closestDistance = currentDistance;
                    closestIcon = realIcons[i];
                }
            }
            return closestIcon;
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