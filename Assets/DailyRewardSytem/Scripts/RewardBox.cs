using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DailyRewardSytem
{
    public class RewardBox : MonoBehaviour
    {
        public TextMeshProUGUI AmountText;
        public TextMeshProUGUI DayText;
        public Image RewardImage;

        public GameObject inactiveCover;
        public GameObject readyToClaimCover;

        public Button Button;

        private RewardBoxState rewardBoxState;

        public RewardBoxState RewardBoxState
        {
            get { return rewardBoxState; }
            set { rewardBoxState = value; }
        }

        private const string DAY_PREFIX = "Day ";

        private void Awake()
        {
            Button = gameObject.GetComponent<Button>();
        }

        public void SetAmount(int amount)
        {
            AmountText.text = amount.ToString();
        }

        public void SetDay(int day)
        {
            DayText.text = DAY_PREFIX + day;
        }

        public void SetRewardSprite(Sprite sprite)
        {
            RewardImage.sprite = sprite;
        }

        public void ChangeState(RewardBoxState rewardBoxState)
        {
            resetLookOfBoxToDefault();
            if (rewardBoxState == RewardBoxState.Inactive)
            {
                Button.interactable = false;
                inactiveCover.SetActive(true);
            }
            else if (rewardBoxState == RewardBoxState.RewardAvailable)
            {
                Button.interactable = true;
                readyToClaimCover.SetActive(true);
                Button.onClick.AddListener(ClaimSelf);
            }
            else if (rewardBoxState == RewardBoxState.Claimed) { }

            this.rewardBoxState = rewardBoxState;
        }

        private void resetLookOfBoxToDefault()
        {
            inactiveCover.SetActive(false);
            readyToClaimCover.SetActive(false);
        }

        private void ClaimSelf()
        {
            ChangeState(RewardBoxState.Claimed);
        }
    }
}