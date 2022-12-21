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
        public async Task<IActionResult> Player(AddPlayerViewModel addPlayerViewModel)
        {
            var player = new Player()
            {
                Name = addPlayerViewModel.Name,
                Team = addPlayerViewModel.Team,
                JersyNumber = addPlayerViewModel.JersyNumber
            };
            await dbContext.Players.AddAsync(player);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("PlayerList");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var player = await dbContext.Players.FirstOrDefaultAsync(a => a.Id == id);

            if (player != null)
            {
                var viewModel = new UpdateDeletePlayerViewModel
                {
                    Id = player.Id,
                    Name = player.Name,
                    Team = player.Team,
                    JersyNumber = player.JersyNumber
                };
                return await Task.Run(()=> View("View",viewModel));
            }
            return RedirectToAction("PlayerList");
        }

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
