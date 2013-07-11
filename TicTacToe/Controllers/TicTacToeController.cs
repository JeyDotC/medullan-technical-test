using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class TicTacToeController : Controller
    {
        private readonly Persistence _persistence = new Persistence();

        //
        // GET: /TicTacToe/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JoinOrCreate()
        {
            Game game;
            if (_persistence.Exists(Request.Form["GameName"]))
            {
                game = _persistence.Get(Request.Form["GameName"]);
                game.Player2 = new Player
                    {
                        Name = Request.Form["PlayerName"],
                        Symbol = Symbols.O
                    };
                Session["MyPlayer"] = game.Player2;
            }
            else
            {
                game = new Game
                    {
                        Name = Request.Form["GameName"],
                        Player1 = new Player
                            {
                                Name = Request.Form["PlayerName"],
                                Symbol = Symbols.X
                            },
                        Board = Game.CreateDashboard()
                    };
                Session["MyPlayer"] = game.Player1;
            }
            _persistence.Save(game);
            Session["Game"] = game.Name;

            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            if (Session["Game"] == null || !_persistence.Exists(Session["Game"].ToString()))
            {
                return RedirectToAction("Index");
            }

            var game = _persistence.Get(Session["Game"].ToString());
            ViewBag.Player = Session["MyPlayer"];

            var winner = game.GetWinner();
            if (winner != Symbols.None)
            {
                ViewBag.Info = string.Format("YOU {0}!", (ViewBag.Player as Player).Symbol == winner ? "WIN" : "LOSE");
            }
            else
            {
                ViewBag.Info = Session["Info"];
                Session["Info"] = "";
            }

            return View(game);
        }

        public ActionResult Move()
        {
            var game = _persistence.Get(Session["Game"].ToString());
            var player = Session["MyPlayer"] as Player;

            var winner = game.GetWinner();
            if (winner == Symbols.None)
            {
                if (game.CurrentPlayer == player.Symbol)
                {
                    DoMovement(game, player);
                }
                else
                {
                    Session["Info"] = "Wait! it's not your turn.";
                }
            }

            return RedirectToAction("Dashboard");
        }

        private void DoMovement(Game game, Player player)
        {
            var x = int.Parse(Request.Form["X"]);
            var y = int.Parse(Request.Form["Y"]);

            if (game.Board[x][y] == null)
            {
                game.Board[x][y] = player;
                game.CurrentPlayer = game.CurrentPlayer == Symbols.X ? Symbols.O : Symbols.X;
                _persistence.Save(game);
            }
            else
            {
                Session["Info"] = "The other player already took that position :(";
            }
        }
    }
}
