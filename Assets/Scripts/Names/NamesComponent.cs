namespace BusinessECS
{
    [System.Serializable]
    public struct NamesComponent {
        public TMPro.TextMeshProUGUI businessName;
        public TMPro.TextMeshProUGUI firstUpgradeName;
        public TMPro.TextMeshProUGUI secondUpgradeName;
        public NamesConfig namesData;
    }
}
