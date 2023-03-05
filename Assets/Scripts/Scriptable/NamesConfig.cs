using UnityEngine;

[CreateAssetMenu(fileName = "Names", menuName = "Bussiness names", order = 51)]
public class NamesConfig : ScriptableObject {
    [SerializeField] string bussinessName;
    [SerializeField] string firstUpgradeName;
    [SerializeField] string secondUpgradeName;

    public string BussinessName {
        get {
            return bussinessName;
        }
    }

    public string FirstUpgradeName {
        get {
            return firstUpgradeName;
        }
    }

    public string SecondUpgradeName {
        get {
            return secondUpgradeName;
        }
    }
}
