using System.Collections.Generic;
using System.Threading.Tasks;
using PinkUmbrella.Models;
using Tides.Actors;
using Tides.Core;
using Tides.Models.Public;

namespace PinkUmbrella.Services
{
    public interface IPublicProfileService
    {
        Task<BaseObject> GetUser(PublicId id, int? viewerId);

        Task<BaseObject> GetUser(string handle, int? viewerId);
        
        Task<ActorObject> Transform(UserProfileModel privateModel, long peerId, int? viewerId);
    }
}