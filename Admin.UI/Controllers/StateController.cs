using Admin.UI.Models;
using Admin.UI.Service;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Admin.UI.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
           _stateService = stateService;
        }

        public async Task<ActionResult> StateIndex()
        {
            try
            {
                var result = await _stateService.GetStateDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<StateDto>());
            }
            catch (HttpRequestException ex)
            {
                // Log the exception details
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw; // rethrow the exception to propagate it up the call stack
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<ActionResult> StateCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StateCreate(StateDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _stateService.AddStateAsync(model);

                if (result != null)
                {
                    return RedirectToAction(nameof(StateIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> StateDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _stateService.DeleteStateAsync(stateid);

                if (result != null)
                {
                    return RedirectToAction(nameof(StateIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    return RedirectToAction(nameof(StateIndex));
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> StateToUpdate(int stateid)
        {
            try
            {
                var existingState = await _stateService.GetStateByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("StateUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(StateIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(StateIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> StateUpdate([FromRoute] int stateid, [FromForm] StateDto updatedStateDto)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    stateid = updatedStateDto.StateId;
                    var updatedState = await _stateService.UpdateStateAsync(stateid, updatedStateDto);

                    if (updatedState != null)
                    {
                        return RedirectToAction(nameof(StateIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                        return View("StateUpdate", updatedStateDto);
                    }
                }

                return View("StateUpdate", updatedStateDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("StateUpdate", updatedStateDto);
            }
        }


    }
}
