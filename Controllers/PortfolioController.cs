using Microsoft.AspNetCore.Mvc;
using InternIntellegence_Portfolio.Services;
using InternIntellegence_Portfolio.Dto;
using InternIntellegence_Portfolio.DbHelper.Repos;
using Microsoft.AspNetCore.Authorization;

namespace InternIntellegence_Portfolio.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly ProtfolioManagementService _portfolioManagementService;
        private readonly ValidationRepo _validationRepo;

        public PortfolioController(ProtfolioManagementService portfolioManagementService,ValidationRepo validationRepo)
        {
            _validationRepo = validationRepo;
            _portfolioManagementService = portfolioManagementService;
        }

        /// <summary>
        /// Adds a complete portfolio including achievements, projects, contacts, and skills
        /// </summary>
        [HttpPost("add")]
        public IActionResult AddPortfolio([FromBody] ProtfolioAddDto portfolioData)
        {
            try
            {
                _portfolioManagementService.AddPortfolio(portfolioData);
                return Ok(new { Message = "Portfolio added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add portfolio", Error = ex.Message });
            }
        }

        /// <summary>
        /// Adds only achievements to the portfolio
        /// </summary>
        [HttpPost("achievements")]
        public IActionResult AddAchievements([FromBody] AchievementsRequestDto request)
        {
            try
            {
                var portfolioData = new ProtfolioAddDto
                {
                    UserId = request.UserId,
                    Achivements = request.Achievements
                };
                _portfolioManagementService.AddPortfolio(portfolioData);
                return Ok(new { Message = "Achievements added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add achievements", Error = ex.Message });
            }
        }

        /// <summary>
        /// Adds only projects to the portfolio
        /// </summary>
        [HttpPost("projects")]
        public IActionResult AddProjects([FromBody] ProjectsRequestDto request)
        {
            try
            {
                var portfolioData = new ProtfolioAddDto
                {
                    UserId = request.UserId,
                    Projects = request.Projects
                };
                _portfolioManagementService.AddPortfolio(portfolioData);
                return Ok(new { Message = "Projects added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add projects", Error = ex.Message });
            }
        }

        /// <summary>
        /// Adds only contacts to the portfolio
        /// </summary>
        [HttpPost("contacts")]
        public IActionResult AddContacts([FromBody] ContactsRequestDto request)
        {
            try
            {
                var portfolioData = new ProtfolioAddDto
                {
                    UserId = request.UserId,
                    Contacts = request.Contacts
                };
                _portfolioManagementService.AddPortfolio(portfolioData);
                return Ok(new { Message = "Contacts added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add contacts", Error = ex.Message });
            }
        }

        /// <summary>
        /// Adds only skills to the portfolio
        /// </summary>
        [HttpPost("skills")]
        public IActionResult AddSkills([FromBody] SkillsRequestDto request)
        {
            try
            {
                var portfolioData = new ProtfolioAddDto
                {
                    UserId = request.UserId,
                    Skills = request.Skills
                };
                _portfolioManagementService.AddPortfolio(portfolioData);
                return Ok(new { Message = "Skills added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to add skills", Error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetPortfolio(string userId)
        {
            try
            {

                if(!_validationRepo.UserExists(userId)){
                    return BadRequest(new {Message = "User Does Not Exist"});
                }

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest(new { Message = "User ID is required" });
                }

                var portfolio = _portfolioManagementService.GetPortfolio(userId);
                
                if (portfolio == null)
                {
                    return NotFound(new { Message = "Portfolio not found for this user" });
                }

                return Ok(portfolio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to retrieve portfolio", Error = ex.Message });
            }
        }

        [HttpPost("send-message")]
        public IActionResult SendMessage([FromBody] MessageSendDto messageData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _portfolioManagementService.SendMessage(messageData);
                return Ok(new { Message = "Message sent successfully!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while sending the message", Error = ex.Message });
            }
        }

        [HttpGet("messages")]
        public IActionResult GetMessages([FromQuery]string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest(new { Message = "User ID is required." });
                }

                var messages = _portfolioManagementService.ShowMessages(userId);
                return Ok(messages);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving messages", Error = ex.Message });
            }
        }
    }

  #region requestDtos
    public class AchievementsRequestDto
    {
        public string UserId { get; set; } = null!;
        public ICollection<AchivementDto> Achievements { get; set; } = new List<AchivementDto>();
    }

    public class ProjectsRequestDto
    {
        public string UserId { get; set; } = null!;
        public ICollection<ProjectsAddDto> Projects { get; set; } = new List<ProjectsAddDto>();
    }

    public class ContactsRequestDto
    {
        public string UserId { get; set; } = null!;
        public ICollection<ContactAddDto> Contacts { get; set; } = new List<ContactAddDto>();
    }

    public class SkillsRequestDto
    {
        public string UserId { get; set; } = null!;
        public ICollection<SkillsAddDto> Skills { get; set; } = new List<SkillsAddDto>();
    }
    #endregion
}