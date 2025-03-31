using Microsoft.AspNetCore.Mvc;

namespace MvcLab.Controllers
{
    public class LabController : Controller
    {
        public IActionResult Info()
        {
            ViewBag.LabNumber = "1";
            ViewBag.Topic = "Створення проєктів";
            ViewBag.Purpose = "ознайомитися з основними принципами роботи .NET, навчитися налаштовувати середовище розробки та встановлювати необхідні компоненти, набути навичок створення рішень та проектів різних типів, набути навичок обробки запитів з використанням middleware.";
            ViewBag.Author = "Ліна Бубон";
            return View();
        }

    }
}