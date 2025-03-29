using InternIntellegence_Portfolio.Models;
using InternIntellegence_Portfolio.Dto;
using InternIntellegence_Portfolio.DbHelper.Repos;

namespace InternIntellegence_Portfolio.Services{
    public class ProtfolioManagementService{

        private readonly IGenericRepo<Achivements> _achievementsRepo;
        private readonly IGenericRepo<Projects> _projectsRepo;
        private readonly IGenericRepo<Contact> _contactRepo;
        private readonly IGenericRepo<Skills> _skillsRepo;

        public ProtfolioManagementService(
            IGenericRepo<Achivements> achievementsRepo,
            IGenericRepo<Projects> projectsRepo,
            IGenericRepo<Contact> contactRepo,
            IGenericRepo<Skills> skillsRepo)
        {
            _achievementsRepo = achievementsRepo;
            _projectsRepo = projectsRepo;
            _contactRepo = contactRepo;
            _skillsRepo = skillsRepo;
        }

        private void AddAchievements(ICollection<AchivementDto> achievementDtos, string userId)
        {
            foreach (var dto in achievementDtos)
            {
                var achievement = new Achivements
                {
                    AchivementName = dto.AchivementName,
                    AchivementDescription = dto.AchivementDescription,
                    ApplicationUserId = userId
                };
                _achievementsRepo.Add(achievement);
                _achievementsRepo.Save();
            }
        }

        private void AddProjects(ICollection<ProjectsAddDto> projectDtos, string userId)
        {
            foreach (var dto in projectDtos)
            {
                var project = new Projects
                {
                    ProjectName = dto.ProjectName,
                    ProjectDiscription = dto.ProjectDiscription,
                    ProjectLink = dto.ProjectLink,
                    ApplicationUserId = userId
                };
                _projectsRepo.Add(project);
                _projectsRepo.Save();
            }
        }

        private void AddContacts(ICollection<ContactAddDto> contactDtos, string userId)
        {
            foreach (var dto in contactDtos)
            {
                var contact = new Contact
                {
                    EmailAddress = dto.EmailAddress,
                    PhoneNumebr = dto.PhoneNumebr,
                    ApplicationUserId = userId
                };
                _contactRepo.Add(contact);
                _contactRepo.Save();
            }
        }

        private void AddSkills(ICollection<SkillsAddDto> skillDtos, string userId)
        {
            foreach (var dto in skillDtos)
            {
                var skill = new Skills
                {
                    SkillName = dto.SkillName,
                    SkillDescription = dto.SkillDescription,
                    ApplicationUserId = userId
                };
                _skillsRepo.Add(skill);
                _skillsRepo.Save();
            }
        }

        public void AddPortfolio(ProtfolioAddDto portfolioData)
        {
            // Add all components
            AddAchievements(portfolioData.Achivements, portfolioData.UserId);
            AddProjects(portfolioData.Projects, portfolioData.UserId);
            AddContacts(portfolioData.Contacts, portfolioData.UserId);
            AddSkills(portfolioData.Skills, portfolioData.UserId);
        }

        private List<AchivementReadDto> GetAchivements(string userId) { 
            var achievements = _achievementsRepo.GetAll()
            .Where(a => a.ApplicationUserId == userId)
            .Select(a => new AchivementReadDto
            {
                AchivementId = a.Id,
                AchivementName = a.AchivementName,
                AchivementDescription = a.AchivementDescription
            })
            .ToList();

            return achievements;
        }

        private List<ContactReadDto> GetContacts(string userId) { 
             var contacts = _contactRepo.GetAll()
            .Where(c => c.ApplicationUserId == userId)
            .Select(c => new ContactReadDto
            {
                ContactId = c.Id,
                EmailAddress = c.EmailAddress,
                PhoneNumebr = c.PhoneNumebr
            })
            .ToList();

            return contacts;
        }

        private List<ProjectsReadDto> GetProjects(string userId) { 
            var projects = _projectsRepo.GetAll()
            .Where(p => p.ApplicationUserId == userId)
            .Select(p => new ProjectsReadDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                ProjectDiscription = p.ProjectDiscription,
                ProjectLink = p.ProjectLink
            })
            .ToList();

            return projects;
        }

        private List<SkillsReadDto> GetSkills(string userId) { 
            var skills = _skillsRepo.GetAll()
            .Where(s => s.ApplicationUserId == userId)
            .Select(s => new SkillsReadDto
            {
                SkillId = s.SkillId,
                SkillName = s.SkillName,
                SkillDescription = s.SkillDescription
            })
            .ToList();

            return skills;
        }

        public PortfolioReadDto GetPortfolio(string userId) { 
            var portfolio = new PortfolioReadDto
            {
                Achivements = GetAchivements(userId),
                Contacts = GetContacts(userId),
                Projects = GetProjects(userId),
                Skills = GetSkills(userId)
            };

            return portfolio;
        }

        
    }
}