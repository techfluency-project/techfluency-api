using TechFluency.Repository;
using TechFluency.Enums;
using TechFluency.Constants;
using TechFluency.Models;

namespace TechFluency.Services
{
    public class BadgeService
    {
        private readonly BadgeRepository _badgeRepository; 
        private readonly UserProgresRepository _userProgresRepository;

        public BadgeService(BadgeRepository badgeRepository, UserProgresRepository userProgresRepository)
        {
            _badgeRepository = badgeRepository;
            _userProgresRepository = userProgresRepository;
        }

        public void CheckBadgeAchievement(UserProgress userProgress)
        {
            var badgeTitles = new BadgesTitles();
            var listActivities = userProgress.Activities;
            var badgesId = new List<string>();

            foreach (var activity in listActivities)
            {
                switch(activity.Topic)
                {
                    case EnumTopic.Vocabulary:
                        if(activity.TotalCorrect == 10)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.VOCABULARY_EXPLORER));
                        }
                        else if(activity.TotalCorrect == 25)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.VOCABULARY_CHALLENGER));
                        }
                        else if (activity.TotalCorrect == 50)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.VOCABULARY_MASTER));
                        }
                        break;

                    case EnumTopic.Daily:
                        if (activity.TotalCorrect == 5)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.DAILY_CHALLENGER));
                        }
                        else if (activity.TotalCorrect == 15)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.DAILY_EXPLORER));
                        }
                        else if (activity.TotalCorrect == 30)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.DAILY_MASTER));
                        }
                        break;

                    case EnumTopic.TextInterpretation:
                        if (activity.TotalCorrect == 10)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.TEXT_INTERPRETATION_EXPLORER));
                        }
                        else if (activity.TotalCorrect == 20)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_CHALLENGER));
                        }
                        else if (activity.TotalCorrect == 40)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.TEXT_INTERPRETATION_MASTER));
                        }
                        break;

                    case EnumTopic.CodeDocumentation:
                        if (activity.TotalCorrect == 10)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_EXPLORER));
                        }
                        else if (activity.TotalCorrect == 25)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_CHALLENGER));
                        }
                        else if (activity.TotalCorrect == 50)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_MASTER));
                        }
                        break;

                    case EnumTopic.Interview:
                        if (activity.TotalCorrect == 5)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_EXPLORER));
                        }
                        else if (activity.TotalCorrect == 15)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_CHALLENGER));
                        }
                        else if (activity.TotalCorrect == 30)
                        {
                            badgesId.Add(_badgeRepository.GetBagdeByTitle(badgeTitles.CODE_DOCUMENTATION_MASTER));
                        }
                        break;
                }
                userProgress.Badges = badgesId;
            }

            _userProgresRepository.Update(userProgress.Id, userProgress);

        }
    }
}
