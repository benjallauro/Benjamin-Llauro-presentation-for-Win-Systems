using Prizes;
using UnityEngine;

namespace SlotMachine
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] PrizesManager prizesManager;

        //Middle Line
        private int _sameIconCountMiddle;
        private int _firstIconCategoryMiddle;

        //Up Line
        private int _sameIconCountUp;
        private int _firstIconCategoryUp;

        //Down Line
        private int _sameIconCountDown;
        private int _firstIconCategoryDown;


        public int CheckResult(Row[] rows)
        {
            SlotIcon[] upIcons = new SlotIcon[rows.Length];
            SlotIcon[] middleIcons = new SlotIcon[rows.Length];
            SlotIcon[] downIcons = new SlotIcon[rows.Length];

            for (int i = 0; i < middleIcons.Length; i++)
            {
                upIcons[i] = rows[i].GetUpIconResult();
                middleIcons[i] = rows[i].GetMiddleIconResult();
                downIcons[i] = rows[i].GetDownIconResult();
            }

            #region Middle slot combo check

            _sameIconCountMiddle = 0;
            _firstIconCategoryMiddle = (int)middleIcons[0].iconType;

            bool stopComboMiddle = false;

            for(int i  = 0; i < middleIcons.Length; i++)
            {
                if (_firstIconCategoryMiddle == (int)middleIcons[i].iconType && !stopComboMiddle)
                    _sameIconCountMiddle++;
                else
                    stopComboMiddle = true;
            }

            #endregion

            #region Up slot combo check

            _sameIconCountUp = 0;
            _firstIconCategoryUp = (int)upIcons[0].iconType;

            bool stopComboUp = false;

            for (int i = 0; i < upIcons.Length; i++)
            {
                if (_firstIconCategoryUp == (int)upIcons[i].iconType && !stopComboUp)
                    _sameIconCountUp++;
                else
                    stopComboUp = true;
            }

            #endregion

            #region Down slot combo check

            _sameIconCountDown = 0;
            _firstIconCategoryDown = (int)downIcons[0].iconType;

            bool stopComboDown = false;

            for (int i = 0; i < downIcons.Length; i++)
            {
                if (_firstIconCategoryDown == (int)downIcons[i].iconType && !stopComboDown)
                    _sameIconCountDown++;
                else
                    stopComboDown = true;
            }

            #endregion

            return (CheckPrize(middleIcons[0].iconType, _sameIconCountMiddle) + CheckPrize(upIcons[0].iconType, _sameIconCountUp) + CheckPrize(downIcons[0].iconType, _sameIconCountDown));
        }

        private int CheckPrize(SlotIcon.Category category, int _sameIconCount)
        {
            Prize[] allPrizes = prizesManager.GetAllPrizes();

            foreach(Prize currentPrize in allPrizes)
            {
                if (currentPrize.category == category && currentPrize.amountNeeded == _sameIconCount)
                    return currentPrize.prize;
            }
            return 0;
        }
    }
}