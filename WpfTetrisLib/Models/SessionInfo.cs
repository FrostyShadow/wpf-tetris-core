using Prism.Events;

namespace WpfTetrisLib.Models
{
    public class SessionInfo
    {
        private static SessionInfo _instance;

        public IEventAggregator EventAggregator;

        private SessionInfo()
        {

        }

        public static SessionInfo Session
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new SessionInfo
                {
                    EventAggregator = new EventAggregator()
                };
                return _instance;
            }
        }
    }
}