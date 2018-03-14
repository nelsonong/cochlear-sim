// Copyright (C) 2015, 2016 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ricimi
{
    /// <summary>
    /// Basic tooltip class used throughout the demo.
    /// </summary>

    public class TooltipCustom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject tooltip;

        public float fadeTime = 0.1f;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (tooltip != null)
            {
				UpdateTooltip();
                StartCoroutine(Utils.FadeIn(tooltip.GetComponent<CanvasGroup>(), 1.0f, fadeTime));
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (tooltip != null)
            {
                StartCoroutine(Utils.FadeOut(tooltip.GetComponent<CanvasGroup>(), 0.0f, fadeTime));
            }
        }

		private void UpdateTooltip() {
			Text[] tooltipText = tooltip.GetComponentsInChildren<Text>();
		    string username = PlayerPrefs.GetString("currentUser");
			StatsItem userData = StatsManager.instance.GetUserStats(username);

			// Icon number.
			Text n = GetComponentInChildren<Text>();

			// Update tooltip.
			switch (n.text) {
				case "1":
					tooltipText[1].text = userData.timeTraining.ToString() + "s";
                    break;
				case "2":
					tooltipText[1].text = userData.successfulInserts.ToString() + "/" + userData.numAttempts.ToString();
                    break;
				case "3":
					tooltipText[1].text = userData.failedInserts.ToString() + "/" + userData.numAttempts.ToString();
                    break;
				case "4":
					tooltipText[1].text = userData.avgInsertionDepths.ToString() + "%";
                    break;
				case "5":
					tooltipText[1].text = userData.avgInsertionTimes.ToString() + "s";
                    break;
				case "6":
					tooltipText[1].text = userData.numResets.ToString();
                    break;
			}
		}
    }
}
