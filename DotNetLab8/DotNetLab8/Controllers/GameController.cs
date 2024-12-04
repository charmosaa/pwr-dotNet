using Microsoft.AspNetCore.Mvc;

namespace DotNetLab8.Controllers
{
    public class GameController : Controller
    {
        private static int range = 100;
        private static int random = new Random().Next(range);
        private static int guessCounter = 0;
        public IActionResult Set(int newRange)
        {
            string cssClass;
            string message;
            string message2;

            if (newRange <= 0)
            {
                cssClass = "error";
                message = $"Range must be greater than 0";
                message2 = "try again";
            }
            else
            {
                range = newRange;
                random = new Random().Next(range);
                guessCounter = 0;
                cssClass = "success";
                message = $"New range: <0,{range})";
                message2 = "start guessing";
            }
            ViewData["Message"] = message;
            ViewData["Message2"] = message2;
            ViewData["CssClass"] = cssClass;
            ViewData["Counter"] = $"guesses: {guessCounter}";

            return View("Game");
        }

        public IActionResult Draw()
        {
            random = new Random().Next(range);
            guessCounter = 0;

            ViewData["Message"] = $"New number has been set from range <0,{range})";
            ViewData["Message2"] = $"start guessing";
            ViewData["CssClass"] = "newRandom"; 
            ViewData["Counter"] = $"guesses: {guessCounter}";

            return View("Game");
        }

        public IActionResult Guess(int guessedNumber)
        {
            string cssClass;
            string message;
            string compared;
            string message2;
            string counter;

            cssClass = "guess";
            guessCounter++;
            counter = $"guesses: {guessCounter}";

            if (guessedNumber < random)
                compared = "too low";
            
            else if (guessedNumber > random)
                compared = "too high";
            
            else
            {
                cssClass = "correctGuess";
                compared = "correct";
                ViewData["Restart"] = "new number has been set - start guessing again";
                guessCounter = 0;
                random = new Random().Next(range); 
            }

            message = $"Number from range <0,{range})";
            message2 = $"YOUR GUESS: {guessedNumber} is {compared}";

            ViewData["Message"] = message;
            ViewData["Message2"] = message2;
            ViewData["Counter"] = counter;
            ViewData["CssClass"] = cssClass;

            return View("Game");
        }


    }
}
