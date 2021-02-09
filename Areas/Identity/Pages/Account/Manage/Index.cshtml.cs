using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarRepairShopRP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarRepairShopRP.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<RepairShopUser> _userManager;
        private readonly SignInManager<RepairShopUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<RepairShopUser> userManager,
            SignInManager<RepairShopUser> signInManager,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Display(Name = "First Name")]
            [DataType(DataType.Text)]
            [StringLength(100, MinimumLength = 3)]
            [Required]
            public string FirstName { get; set; }


            [Display(Name = "Last Name")]
            [DataType(DataType.Text)]
            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string LastName { get; set; }
    
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [StringLength(100, MinimumLength = 3)]
            [Display(Name = "Address")]
            public string Address { get; set; }
 
            [Display(Name = "Role")]
            public string RoleName { get; set; }
        }

        private async Task LoadAsync(RepairShopUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var role = await _userManager.GetRolesAsync(user);
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleName = role.FirstOrDefault(),
                Address = user.Address
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if(user.LastName != Input.LastName)
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

            // _logger.LogInformation(Input.LastName);

            var updateInfo = await _userManager.UpdateAsync(user);
            if(!updateInfo.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update first and last name";
                return RedirectToPage();
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
