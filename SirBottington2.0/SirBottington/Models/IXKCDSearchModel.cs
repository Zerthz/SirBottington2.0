namespace SirBottington.Models
{
    public interface IXKCDSearchModel
    {
        IXKCDModel Comic { get; set; }
        string Id { get; set; }
        string SearchTerm { get; set; }
    }
}