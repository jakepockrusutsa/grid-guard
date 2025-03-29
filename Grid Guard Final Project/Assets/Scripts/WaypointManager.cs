using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    // Public array for waypoint objects
    public Transform[] waypoints;

    public static WaypointManager Instance { get; private set; }

    void Awake()
    {
        // pattern to make this manager easily accessible.
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
