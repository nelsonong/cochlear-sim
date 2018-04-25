using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ToucanSystems;
using UnityEngine;

public class GraphManager : MonoBehaviour {
    public Color32 color;
    public float lineWidth;
    public SmartChart chart;

    private Dictionary<string,string> timeStampDict;

    private SimulationStatsData dataHolder;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<string> GetDataTimeStamps(string username)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dirInfo.GetFiles("*.json");

        Debug.Log(info[0]);

        timeStampDict = new Dictionary<string, string>();
        List<string> simulationList = new List<string>();

        foreach (FileInfo f in info)
        {
            Debug.Log(f.ToString());
            if (Path.GetFileName(f.ToString()).Split('_')[0].Equals(username))
            {
                Debug.Log(f.ToString().Split('_')[0]);
                Debug.Log(f.ToString());
                if (f.ToString().Split('_').Length > 1){
                    string timeStr = new System.DateTime(Convert.ToInt64(f.ToString().Split('_')[1])).ToString("G");
                    timeStampDict[timeStr] = f.ToString();
                    simulationList.Add(timeStr);
                }
                
            }
        }
        return simulationList;
    }

    public void ChangeSimulation(string timeStr, string type="depth")
    {
        LoadData(timeStr);
        PrepareChartData(GetChartData(type), color, lineWidth);
        PrepareChart(type);
    }

    public void ChangeChart(string type)
    {
        PrepareChartData(GetChartData(type), color, lineWidth);
        PrepareChart(type);
    }

    private void LoadData (string timeStr)
    {
        if (timeStampDict.ContainsKey(timeStr))
        {
            Debug.Log(timeStr);
            Debug.Log(timeStampDict[timeStr]);
            dataHolder = StatsManager.instance.LoadTimeAndForceStats(timeStampDict[timeStr]);
        }
           
    }

    public List<Vector2> GetChartData(string type)
    {
        if (dataHolder == null || dataHolder.depth == null || dataHolder.force == null)
            return null;

        if (type == "depth")
            return new List<Vector2>(dataHolder.depth);

        else
            return new List<Vector2>(dataHolder.force);
    }

    private void PrepareChart(string type)
    {
        SmartChartData[] chartData = { PrepareChartData(GetChartData(type), color, lineWidth) };

        Debug.Log(GetChartData("depth"));

        Vector2 maxVals = GetMaxVals(GetChartData(type));
        chart.maxXValue = (maxVals.x > 10) ? maxVals.x : 10;
        chart.maxYValue = (maxVals.y > 10) ? maxVals.y : 10;

        DisplayDataOnChart(chartData);
        //DisplayDataOnChart(chartData); // Sorry.
    }

    private Vector2 GetMaxVals(List<Vector2> dataList)
    {
        Vector2 maxVals = Vector2.zero;
        foreach (Vector2 point in dataList)
        {
            maxVals.x = point.x > maxVals.x ? point.x : maxVals.x;
            maxVals.y = point.y > maxVals.y ? point.y : maxVals.y;
        }
        return maxVals;
    }

    private SmartChartData PrepareChartData(List<Vector2> dataList, Color32 color, float lineWidth)
    {
        if (dataList != null && dataList.Count > 0)
        {
            SmartChartData chartData = new SmartChartData();
            chartData.data = dataList.ToArray();
            chartData.dataLineColor = color;
            chartData.dataLineWidth = lineWidth;
            return chartData;
        }
        return null;
    }

    private void DisplayDataOnChart(SmartChartData[] chartData)
    {
        chart.fillAreaUnderLine = false;
        //chart.DeleteMarkers(chartData[0].data.Length);
        DeleteMarkers(chartData[0]);
        chart.showMarkers = true;
        
        chart.SetupValues(true);
        chart.chartData[0].data = chartData[0].data;
        chart.UpdateChart();
    }

    private void DeleteMarkers(SmartChartData chartData)
    {
        GameObject markersContainer = GameObject.Find("MarkersContainer");
        ChartMarkerController[] markers = markersContainer.GetComponentsInChildren<ChartMarkerController>();
        if (chartData.data.Length < markers.Length)
            for (int i = chartData.data.Length; i < markers.Length; i++)
            {
                if (gameObject.activeInHierarchy)
                {
                        if (markers[i] != null)
                        {
                            Destroy(markers[i].gameObject);
                            markers[i] = null;
                        }
                }
            }
    }
}
