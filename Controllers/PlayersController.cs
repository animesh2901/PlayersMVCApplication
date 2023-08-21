using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlayersMVCApplication.Data;
using PlayersMVCApplication.Models;
using PlayersMVCApplication.Models.Player;

namespace PlayersMVCApplication.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayerMVCDbContext dbContext;

        //Constructor
        public PlayersController(PlayerMVCDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All Data From DB
        [HttpGet]
        public async Task<IActionResult> PlayerList()
        {
            var players = await dbContext.Players.ToListAsync();
            return View(players);
        }

        //Go to addPlayer.cshtml from home page after clicking add player link button
        [HttpGet]
        public IActionResult AddPlayer()
        {
            return View();
        }

        //Add New Player in DB
        [HttpPost]
        public async Task<IActionResult> AddPlayer(Player player)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }
                //TempData["Country"] = GetCountryList();
                var newPlayer = new Player()
                {
                    Name = player.Name,
                    PhoneNumber = player.PhoneNumber,
                    Team = player.Team,
                    JersyNumber = player.JersyNumber,
                    //Country = player.Country,
                    //State = player.State,
                    //City = player.City
                };
                await dbContext.Players.AddAsync(newPlayer);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("PlayerList");
            }
            catch (Exception ex)
            {
                TempData["Exception"] = ex.Message;
                return View();

            }
        }

        //Return to PlayerList.cshtml page after successfully add new player
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            //return View();
            var validatePlayer = await dbContext.Players.FirstOrDefaultAsync(a => a.Id == id);

            if (validatePlayer != null)
            {
                var viewModel = new Player
                {
                    Id = validatePlayer.Id,
                    Name = validatePlayer.Name,
                    PhoneNumber = validatePlayer.PhoneNumber,
                    Team = validatePlayer.Team,
                    JersyNumber = validatePlayer.JersyNumber,
                    //Country = validatePlayer.Country,
                    //State = validatePlayer.State,
                    //City = validatePlayer.City
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("PlayerList");
        }

        //For updation of existing player in DB; Go to the view.cshtml page
        //After click on view link button from PlayerList.cshtml page
        [HttpPost]
        public async Task<IActionResult> Update(Player player)
        {
            var updatePlayer = await dbContext.Players.FindAsync(player.Id);

            if (updatePlayer != null)
            {
                updatePlayer.Name = player.Name;
                updatePlayer.PhoneNumber = player.PhoneNumber;
                updatePlayer.Team = player.Team;
                updatePlayer.JersyNumber = player.JersyNumber;
                //updatePlayer.Country = player.Country;
                //updatePlayer.State = player.State;
                //updatePlayer.City = player.City;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("PlayerList");
            }

            return RedirectToAction("PlayerList");
        }

        //Finally Delete Existing player in DB on click of Delete button in View.cshtml page
        [HttpPost]
        public async Task<IActionResult> Delete(Player player)
        {
            var deletePlayer = await dbContext.Players.FindAsync(player.Id);

            if (deletePlayer != null)
            {
                dbContext.Players.Remove(deletePlayer);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("PlayerList");
            }
            return RedirectToAction("PlayerList");
        }

        public async Task<Object> GetCountryList()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://alpha-vantage.p.rapidapi.com/query?interval=5min&function=TIME_SERIES_INTRADAY&symbol=MSFT&datatype=json&output_size=compact"),
                Headers =
                        {
                            { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJhbmltZXNoYW5hbmQyOTAxQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6InB6ZVlicE5kRm1fMV82OG1tV2JxRXcwZ0xPbU1semxTYVlFVGtlUi1OWHhpMDllR0l6ODJUX1J0Skp0aktRV0ZIVU0ifSwiZXhwIjoxNjg1MTM0NzMyfQ.tI1dkfXJcunN9hrRIN4M6F255GVr_EZfkVGVXmd3bhE" },
                            { "Accept", "application/json" }
                        }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                //  
                //TempData["CountryList"]= body;
                return body;
            }

        }
    }
}
