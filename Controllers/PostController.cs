﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PinkUmbrella.Models;
using PinkUmbrella.Services;
using PinkUmbrella.ViewModels.Post;
using PinkUmbrella.ViewModels.Home;

namespace PinkUmbrella.Controllers
{
    [AllowAnonymous]
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;

        public PostController(IWebHostEnvironment environment, ILogger<PostController> logger, SignInManager<UserProfileModel> signInManager,
            UserManager<UserProfileModel> userManager, IPostService posts, IUserProfileService userProfiles):
            base(environment, signInManager, userManager, posts, userProfiles)
        {
            _logger = logger;
        }

        [Route("/Post/{id}")]
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["Controller"] = "Post";
            ViewData["Action"] = nameof(Index);
            var user = await GetCurrentUserAsync();
            if (id != null)
            {
                return View(new PostViewModel() {
                    Post = await _posts.GetPost(id.Value, user?.Id ?? -1),
                    MyProfile = user
                });
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var result = await _posts.TryCreateTextPosts(user.Id, model.Content, model.Visibility);
            if (!result.Error)
            {
                return RedirectToAction(nameof(Index), new { Id = result.Posts.First().Id });
            }
            else
            {
                ViewData["errorMsg"] = result.Message;
                return View();
            }
        }
    }
}
