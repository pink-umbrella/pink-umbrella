using Tides.Models.Peer;

namespace PinkUmbrella.ViewModels.Peer
{
    public class PeerViewModel : BaseViewModel
    {
        public Tides.Actors.Peer Peer { get; set; }
        
        public object ProxiedViewModel { get; set; }

        public string ViewName { get; set; }
    }
}