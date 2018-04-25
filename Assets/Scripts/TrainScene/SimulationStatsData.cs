using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SimulationStatsData {
    public Vector2[] depth;
    public Vector2[] force;

    public SimulationStatsData(List<Vector2> depth, List<Vector2> force)
    {
        this.depth = depth.ToArray();
        this.force = force.ToArray();
    }
}

