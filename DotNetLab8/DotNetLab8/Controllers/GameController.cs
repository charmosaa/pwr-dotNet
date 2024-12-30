using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DotNetLab8.Controllers
{
    public class GameController : Controller
    {
        private const string RangeKey = "Range";
        private const string RandomKey = "Random";
        private const string GuessCounterKey = "GuessCounter";
        private const string PreviousGuessesKey = "PreviousGuesses";

        public IActionResult Set(int newRange)
        {
            string cssClass;
            string message;
            string message2;

            if (newRange <= 0)
            {
                cssClass = "error";
                message = "Range must be greater than 0";
                message2 = "try again";
            }
            else
            {
                HttpContext.Session.SetInt32(RangeKey, newRange);
                HttpContext.Session.SetInt32(RandomKey, new Random().Next(newRange));
                HttpContext.Session.SetInt32(GuessCounterKey, 0);
                HttpContext.Session.SetString(PreviousGuessesKey, "");

                cssClass = "success";
                message = $"New range: <0,{newRange})";
                message2 = "start guessing";
            }
            ViewData["Message"] = message;
            ViewData["Message2"] = message2;
            ViewData["CssClass"] = cssClass;
            ViewData["Counter"] = "guesses: 0";

            return View("Game");
        }

        public IActionResult Draw()
        {
            int range = HttpContext.Session.GetInt32(RangeKey) ?? 100;
            HttpContext.Session.SetInt32(RandomKey, new Random().Next(range));
            HttpContext.Session.SetInt32(GuessCounterKey, 0);
            HttpContext.Session.SetString(PreviousGuessesKey, "");

            ViewData["Message"] = $"New number has been set from range <0,{range})";
            ViewData["Message2"] = "start guessing";
            ViewData["CssClass"] = "newRandom";
            ViewData["Counter"] = "guesses: 0";

            return View("Game");
        }

        public IActionResult Guess(int guessedNumber)
        {
            string cssClass = "guess";
            int range = HttpContext.Session.GetInt32(RangeKey) ?? 100;
            int random = HttpContext.Session.GetInt32(RandomKey) ?? new Random().Next(range);
            int guessCounter = HttpContext.Session.GetInt32(GuessCounterKey) ?? 0;
            guessCounter++;

            string previousGuesses = HttpContext.Session.GetString(PreviousGuessesKey) ?? "";
            previousGuesses += $"{guessedNumber}, ";

            string compared;
            if (guessedNumber < random)
            {
                compared = "too low";
            }
            else if (guessedNumber > random)
            {
                compared = "too high";
            }
            else
            {
                cssClass = "correctGuess";
                compared = "correct";
                HttpContext.Session.SetInt32(RandomKey, new Random().Next(range));
                HttpContext.Session.SetInt32(GuessCounterKey, 0);
                HttpContext.Session.SetString(PreviousGuessesKey, "");
                ViewData["Restart"] = "new number has been set - start guessing again";
                guessCounter = 0;
                previousGuesses = "";
            }

            HttpContext.Session.SetInt32(GuessCounterKey, guessCounter);
            HttpContext.Session.SetString(PreviousGuessesKey, previousGuesses);

            ViewData["Message"] = $"Number from range <0,{range})";
            ViewData["Message2"] = $"YOUR GUESS: {guessedNumber} is {compared}";
            ViewData["Counter"] = $"guesses: {guessCounter}";
            ViewData["CssClass"] = cssClass;
            ViewData["PreviousGuesses"] = $"previous guesses: {previousGuesses}";

            return View("Game");
        }
    }
}
