using System.Windows.Controls;
using Prism.Events;

namespace WpfTetrisLib.Models
{
    public class FieldSetMessage
    {
        public Grid Grid { get; set; }
    }

    public class FieldSetModem : PubSubEvent<FieldSetMessage> { }
}