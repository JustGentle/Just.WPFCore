namespace Just.Setting
{
    public interface ISetting
    {
        bool DelaySave { get; set; }
        void LoadSettings();
        void SaveSettings();
    }
}
