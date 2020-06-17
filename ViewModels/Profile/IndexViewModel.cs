using PinkUmbrella.Models;

namespace PinkUmbrella.ViewModels.Profile
{
    public class IndexViewModel: ProfileViewModel
    {
        public FeedModel Feed { get; set; }

        public PaginatedModel<ArchivedMediaModel> Media { get; set; }
    }
}