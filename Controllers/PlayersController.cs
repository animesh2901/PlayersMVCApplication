using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Player(Player player)
        {
            var newPlayer = new Player()
            {                
                Name = player.Name,
                Team = player.Team,
                JersyNumber = player.JersyNumber
            };
            await dbContext.Players.AddAsync(newPlayer);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("PlayerList");
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
                    Team = validatePlayer.Team,
                    JersyNumber = validatePlayer.JersyNumber
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("PlayerList");
        }

        //For updation of existing player in DB; Go to the view.cshtml page
        //After click on view link button from PlayerList.cshtml page
        [HttpPost]
        public async Task<IActionResult> Update (UpdateDeletePlayerViewModel playerModel)
        {
            var player = await dbContext.Players.FindAsync(playerModel.Id);

            if (player != null)
            {
                player.Name = playerModel.Name;
                player.Team= playerModel.Team;
                player.JersyNumber = playerModel.JersyNumber;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("PlayerList");
            }

            return RedirectToAction("PlayerList");
        }

        //Finally Delete Existing player in DB on click of Delete button in View.cshtml page
        [HttpPost]
        public async Task<IActionResult> Delete (UpdateDeletePlayerViewModel playerModel)
        {
            var player = await dbContext.Players.FindAsync(playerModel.Id);

            if (player != null)
            {
                dbContext.Players.Remove(player);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("PlayerList");
            }
            return RedirectToAction("PlayerList");
        }
    }
}
