using System.ComponentModel;

namespace Stocknize.Domain.Enums
{
    public enum MovimentationType
    {
        [Description("Compra")]
        Buy = 0,
        [Description("Venda")]
        Sell
    }
}
