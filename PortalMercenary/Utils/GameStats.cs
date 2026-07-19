namespace PortalMercenary.Utils;

public struct GameStats
{
    public float Time;

    public bool? IsWin;
    public int Kills;
    
    public string[] ToStringArray()
    {
        return
        [
            $"{(IsWin.HasValue ? (IsWin.Value ? "WIN!" : "DEFEAT!") : "N/A")}",
            $"time: {Time:F2}",
            $"kills: {Kills}"
        ];
    }
}