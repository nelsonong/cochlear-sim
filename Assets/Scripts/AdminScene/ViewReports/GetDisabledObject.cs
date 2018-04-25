using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDisabledObject : MonoBehaviour {

	public GameObject statisticsPopup;
	public Dropdown timestampDropdown;
	public Dropdown chartTypeDropdown;

	public GameObject GetStatisticsPopup() {
		return statisticsPopup;
	}

	public Dropdown GetTimestampDropdown() {
		return timestampDropdown;
	}

	public Dropdown GetChartTypeDropdown() {
		return chartTypeDropdown;
	}

	public void GetSmartChart() {
		
	}
}
