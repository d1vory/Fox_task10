using Microsoft.AspNetCore.Mvc;

namespace Task10.Controllers;

public class CourseController: Controller
{
    private readonly ILogger<CourseController> _logger;

    public CourseController(ILogger<CourseController> logger)
    {
        _logger = logger;
    }

    
}