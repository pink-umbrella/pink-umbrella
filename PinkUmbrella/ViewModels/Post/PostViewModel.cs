using System.Collections.Generic;
using Tides.Core;

namespace PinkUmbrella.ViewModels.Post
{
    public class PostViewModel: BaseViewModel
    {
        public BaseObject Post { get; set; }
        public List<BaseObject> Chain { get; set; }
    }
}