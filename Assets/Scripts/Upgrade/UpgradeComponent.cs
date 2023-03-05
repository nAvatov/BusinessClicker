namespace BusinessECS
{
    [System.Serializable]
    public struct UpgradeComponent {
        public UnityEngine.UI.Button UpgradeButton1;
        public UnityEngine.UI.Button UpgradeButton2;
        public TMPro.TextMeshProUGUI Price1TMP;
        public TMPro.TextMeshProUGUI Multiplier1TMP;
        public TMPro.TextMeshProUGUI Price2TMP;
        public TMPro.TextMeshProUGUI Multiplier2TMP;
        public UpgradeConfig Upgrade1Data;
        public UpgradeConfig Upgrade2Data;
    }
}
