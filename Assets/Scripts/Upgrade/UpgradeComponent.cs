namespace BusinessECS
{
    [System.Serializable]
    public struct UpgradeComponent {
        public UnityEngine.UI.Button upgradeButton1;
        public UnityEngine.UI.Button upgradeButton2;
        public TMPro.TextMeshProUGUI price1TMP;
        public TMPro.TextMeshProUGUI multiplier1TMP;
        public TMPro.TextMeshProUGUI price2TMP;
        public TMPro.TextMeshProUGUI multiplier2TMP;
        public UpgradeConfig upgradeData1;
        public UpgradeConfig upgradeData2;
    }
}
