using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level config", order = 51)]
public class LevelConfig : ScriptableObject { 
    [SerializeField] int initialLevel;
    [SerializeField] int initialLevelCost;

    public int InitialLevel {
        get {
            return initialLevel;
        }
    }

    public int InitialLevelPrice {
        get {
            return initialLevelCost;
        }
    }
}
