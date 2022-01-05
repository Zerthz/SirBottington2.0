namespace SirBottington.Models
{
    public interface IXKCDModel
    {
        string alt { get; set; }
        string day { get; set; }
        string Id { get; set; }
        string img { get; set; }
        string link { get; set; }
        string month { get; set; }
        string news { get; set; }
        int num { get; set; }
        string safe_title { get; set; }
        string title { get; set; }
        string transcript { get; set; }
        string year { get; set; }
    }
}