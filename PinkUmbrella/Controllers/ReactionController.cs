using System;
using System.Threading.Tasks;
using Estuary.Core;
using Estuary.Objects;
using Estuary.Services;
using Estuary.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using PinkUmbrella.Models;
using PinkUmbrella.Models.Settings;
using PinkUmbrella.Services;
using PinkUmbrella.Services.Local;
using PinkUmbrella.ViewModels.Shared;
using Tides.Models;
using Tides.Models.Public;

namespace PinkUmbrella.Controllers
{
    [FeatureGate(FeatureFlags.ControllerReaction)]
    [Authorize]
    public class ReactionController: BaseController
    {
        private readonly ILogger<ReactionController> _logger;
        private readonly IReactionService _reactions;
        private readonly IActivityStreamRepository _activityStream;

        public ReactionController(
            ILogger<ReactionController> logger,
            SignInManager<UserProfileModel> signInManager,
            UserManager<UserProfileModel> userManager,
            IUserProfileService localProfiles,
            IPublicProfileService publicProfiles,
            IReactionService reactions,
            IAuthService auth,
            ISettingsService settings,
            IActivityStreamRepository activityStream):
            base(signInManager, userManager, localProfiles, publicProfiles, auth, settings)
        {
            _logger = logger;
            _reactions = reactions;
            _activityStream = activityStream;
        }

        public async Task<IActionResult> Like(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Like);

        public async Task<IActionResult> Dislike(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Dislike);

        public async Task<IActionResult> Upvote(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Upvote);

        public async Task<IActionResult> Downvote(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Downvote);

        public async Task<IActionResult> Ignore(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Ignore);

        public async Task<IActionResult> Report(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Report);

        public async Task<IActionResult> Block(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Block);

        public async Task<IActionResult> Follow(string id) => await React(new PublicId(Uri.UnescapeDataString(id)), ReactionType.Follow);

        private async Task<IActionResult> React(PublicId toId, ReactionType t)
        {
            var user = await GetCurrentUserAsync();
            var reactionId = await _reactions.React(user.PublicId, toId, t);
            ViewData["PartialName"] = "Button/ReactButton";
            return View("_NoLayout", new ReactViewModel()
            {
                HasReacted = true,
                Enabled = true,
                ToId = toId,
                Type = t,
                ViewerId = user.UserId,
                Count = await _reactions.GetCount(toId, t),
            });
        }

        public async Task<IActionResult> UnLike(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Like);

        public async Task<IActionResult> UnDislike(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Dislike);

        public async Task<IActionResult> UnUpvote(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Upvote);

        public async Task<IActionResult> UnDownvote(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Downvote);

        public async Task<IActionResult> UnReport(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Report);

        public async Task<IActionResult> UnBlock(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Block);

        public async Task<IActionResult> UnIgnore(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Ignore);

        public async Task<IActionResult> UnFollow(string id, string undo) => await UnReact(new PublicId(Uri.UnescapeDataString(id)), undo, ReactionType.Follow);

        private async Task<IActionResult> UnReact(PublicId toId, string undo, ReactionType t)
        {
            var user = await GetCurrentUserAsync();
            BaseObject toActivity = null;
            if (string.IsNullOrWhiteSpace(undo))
            {
                toActivity = await _activityStream.Get(new ActivityStreamFilter("outbox") { id = user.PublicId, includeReplies = false, targetId = toId }.FixType(t.ToString()));
            }
            else
            {
                toActivity = await _activityStream.Get(new ActivityStreamFilter("outbox") { id = user.PublicId, includeReplies = false, activityId = new PublicId(undo) });
            }
                
            if (toActivity == null)
            {
                throw new ArgumentNullException(nameof(toActivity));
            }
            else if (toActivity is Error error)
            {
                return BadRequest(error.summary ?? error.content);
            }
            else
            {
                await _activityStream.Undo(user, toActivity);
            }

            ViewData["PartialName"] = "Button/ReactButton";
            return View("_NoLayout", new ReactViewModel()
            {
                HasReacted = false,
                Enabled = true,
                ToId = toId,
                Type = t,
                ViewerId = user.UserId,
                Count = await _reactions.GetCount(toId, t),
            });
        }
    }
}