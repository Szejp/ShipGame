using System;
using TMPro;
using UnityEngine;

namespace DailyRewardSytem {
    public class TimeToResetTextUpdater : MonoBehaviour
    {
        private TextMeshProUGUI DayText;
        private DateTime begining;

        private void Awake()
        {
            DayText = gameObject.GetComponent<TextMeshProUGUI>();
            DayText.text = "Resets in " + 30 + " days";
        }

        public void SetBegginingDate(DateTime beginingDate)
        {
            this.begining = beginingDate;
            DayText.text = "Resets in " + (DateTime.Now - beginingDate).TotalDays+ " days";
        }
    }
}
