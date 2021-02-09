using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepairShopRP.Pages.Users
{
    [Authorize(Roles = "Mechanic,Admin")]
    public class EditUserModel : PageModel
    {

        private readonly UserManager<RepairShopUser> _userManager;
        private readonly SignInManager<RepairShopUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly RoleManager<ApplicationRole> _roleManger;

        public EditUserModel(
            UserManager<RepairShopUser> userManager,
            SignInManager<RepairShopUser> signInManager,
            ILogger<IndexModel> logger,
            RoleManager<ApplicationRole> roleManger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManger = roleManger;
        }


        public string UserID { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            public string Id;

            [Display(Name = "Username")]
            [DataType(DataType.Text)]
            public string Username { get; set; }

            [Display(Name = "First Name")]
            [DataType(DataType.Text)]
            [StringLength(100, MinimumLength = 3)]
            [Required]
            public string FirstName { get; set; }


            [Display(Name = "Last Name")]
            [StringLength(100, MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Required]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [StringLength(100, MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Role")]
            public string Role { get; set; }

        }

        public SelectList roles { get; set; }
        private async Task LoadAsync(RepairShopUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var role = await _userManager.GetRolesAsync(user);

            var list = await _roleManger.Roles.ToListAsync();

            roles = new SelectList(list, "Name", "Name", role);
            Input = new InputModel
            {
                Id = user.Id,
                PhoneNumber = phoneNumber,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Role = role.FirstOrDefault()
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
                return NotFound();

    
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{id}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{Input.Id}'.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Model invalid");
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    
                    return RedirectToPage();
                }
            }

            if (user.LastName != Input.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (user.FirstName != Input.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (user.Address != Input.Address)
            {
                user.Address = Input.Address;
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.AddToRoleAsync(user, Input.Role);

            var updateInfo = await _userManager.UpdateAsync(user);
            if (!updateInfo.Succeeded)
            {
                
                return RedirectToPage();
            }


            
            return RedirectToPage("/Users/Index");
        }
    }
}

