namespace BusinessECS
{
    [System.Serializable]
    public struct NamesComponent {
        public TMPro.TextMeshProUGUI BusinessName;
        public TMPro.TextMeshProUGUI FirstUpgradeName;
        public TMPro.TextMeshProUGUI SecondUpgradeName;
        public NamesConfig NamesData;
    }
}
