using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Income", menuName = "Income config", order = 51)]
public class IncomeConfig : ScriptableObject { 
    [SerializeField] int initialIncome;
    [SerializeField] int incomeDelay;

    public int InitialIncome {
        get {
            return initialIncome;
        }
    }

    public int IncomeDelay {
        get {
            return incomeDelay;
        }
    }
}
