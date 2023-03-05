using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Business upgrade", order = 51)]
public class UpgradeConfig : ScriptableObject { 
    [SerializeField] int price;
    [SerializeField] int incomeMultiplierInPercents;

    public int Price {
        get {
            return price;
        }
    }

    public int IncomeMultiplier {
        get {
            return incomeMultiplierInPercents;
        }
    }
}
