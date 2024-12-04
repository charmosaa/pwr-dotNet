using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotNetLab8.Models;

namespace DotNetLab8.Controllers
{
    public class ToolController : Controller
    {
        
        static (int, double?, double?) QuadtraticEquation(double a = 0, double b = 0, double c=0)
        {

            //non-quadratic
            if (a == 0)
            {
                if (b == 0)
                    return c == 0 ? (int.MaxValue, null, null) : (0, null, null);
                return (1, -c / b, null);
            }

            //quadratic
            else
            {
                double delta = b * b - 4 * a * c;
                if (delta < 0)
                    return (0, null, null);
                else if (delta == 0)
                    return (1, -b / (2 * a), null);
                else
                {
                    double sqrtDelta = Math.Sqrt(delta);
                    return (2, (-b - sqrtDelta) / (2 * a), (-b + sqrtDelta) / (2 * a));
                }
            }
        }

       // [Route("Tool/Solve/{a}/{b}/{c}")]
        public IActionResult Solve(double a, double b, double c)
        {
            string cssClass;
            string message;
            string equation = $"{a}x^2 + {b}x + {c} = 0";

            (int solutions, double? x1, double? x2) = QuadtraticEquation(a, b, c);

            switch(solutions)
            {
                case 0:
                    cssClass = "no-solution";
                    message = "no solutions";
                    break;
                case 1:
                    cssClass = "one-solution";
                    message = $"1 solution x = {x1:F3}";
                    break;
                case 2:
                    cssClass = "two-solutions";
                    message = $"2 solutions x = {x1:F3} or x = {x2:F3}";
                    break;
                case > 2:
                    cssClass = "identity";
                    message = "infinitely many solutions";
                    break;
                default:
                    cssClass = "no-equation";
                    message = "no equation to solve";
                    break;

            }

            ViewData["CssClass"] = cssClass;
            ViewData["Equation"] = equation;
            ViewData["Message"] = message;

            return View();
        }


    }
}
