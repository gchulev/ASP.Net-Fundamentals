using Contacts.Data.Models;
using Contacts.Interfaces;
using Contacts.Models;
using Contacts.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
	public class ContactsController : Controller
	{
		private readonly IDataService _dataService;
		private readonly IModelMappingService _modelMappingService;
		private readonly IUserService _userService;
		public ContactsController(IDataService dataService, IModelMappingService modelMappingService, IUserService userService)
		{
			this._dataService = dataService;
			this._modelMappingService = modelMappingService;
			this._userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			List<Contact> allContacts = await this._dataService.GetAllContactsAsync();

			List<Task<ContactViewModel>> allContactsViewModelTasks = allContacts.Select(async c => await this._modelMappingService.MapContactToContactViewModelAsync(c)).ToList();

			ContactViewModel[] contacts = await Task.WhenAll(allContactsViewModelTasks.Select(async t => await t));

			return View(new ContactsViewModel() { Contacts = contacts.ToList() });
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(ContactViewModel contact)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("InvalidContact", "Invalid Contact!");
				return View(contact);
			}
			try
			{
				Contact contactEntry = await this._modelMappingService.MapContactViewModelToContactAsync(contact).ConfigureAwait(false);

				Contact contactToSave = await this._dataService.AddContactAsync(contactEntry);

				if (contactToSave == null)
				{
					throw new Exception("Unable to add the contact. It might already esist in the dtabase.");
				}

				return RedirectToAction("All");

			}
			catch (Exception)
			{
				ModelState.AddModelError("", "Error while adding the contact!");
				return View(contact);
			}

		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? contactId)
		{
			if (contactId is null)
			{
				var error = new ErrorViewModel() { RequestId = contactId.ToString() };
				return RedirectToAction("Error", error);
			}

			Contact contact = await this._dataService.GetContactByIdAsync(contactId).ConfigureAwait(false);
			ContactViewModel contactView = await this._modelMappingService.MapContactToContactViewModelAsync(contact);

			return View(contactView);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ContactViewModel contactView)
		{
			if (ModelState.IsValid)
			{
				Contact contact = await this._modelMappingService.MapContactViewModelToContactAsync(contactView).ConfigureAwait(false);
				Contact editedContact = await this._dataService.EditContactAsync(contact); // Not sure if I have to use the contact to render on screen 

				return RedirectToAction("All");
			}

			var error = new ErrorViewModel() { RequestId = contactView.ContactId.ToString() };

			return View("Error", error);
		}

		[HttpGet]
		public async Task<IActionResult> Team()
		{
			ApplicationUser currentUser = await this._dataService.FindUserByUsername(User.Identity!.Name!);

			List<ContactViewModel> contactsList = (await Task.WhenAll(currentUser.ApplicationUsersContacts.Select(async auc => await this._modelMappingService.MapContactToContactViewModelAsync(auc.Contact)))).ToList();

			var contacts = new ContactsViewModel() { Contacts = contactsList };

			return View(contacts);
		}

		[HttpPost]
		public async Task<IActionResult> AddToTeam(int contactId)
		{
			ApplicationUser currentUser = await this._userService.FindUserByClaimsPrincipalAsync(User);

			await this._dataService.AddContactToTeamAsync(contactId, currentUser);

			return RedirectToAction("All");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromTeam(int contactId)
		{
			ApplicationUser? user = await this._userService.FindUserByNameAsync(User.Identity.Name);

			try
			{
				await this._dataService.RemoveFromTeam(contactId, user);
			}
			catch (Exception)
			{
				ModelState.AddModelError("UserRemoveError", $"Unable to remove {contactId} from user, because the user is not found!");
				return View();
			}

			return RedirectToAction("Team");
		}
	}
}
