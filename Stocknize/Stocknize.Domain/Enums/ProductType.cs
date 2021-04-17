using System.ComponentModel;

namespace Stocknize.Domain.Enums
{
    public enum ProductType
    {
        [Description("Cerveja")]
        Beer = 0,
        [Description("Refrigerante")]
        Soda
    }
}
