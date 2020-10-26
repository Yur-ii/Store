using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Components
{
    public class LoginImg : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public LoginImg(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View("LoginImg",user);
        }
    }
}
