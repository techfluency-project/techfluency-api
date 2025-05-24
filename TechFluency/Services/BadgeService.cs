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

        private readonly Dictionary<EnumTopic, List<(int goal, string BadgeTitle)>> _badgeCriteria = new()
        {
            { EnumTopic.Vocabulary, new List<(int, string)>
                {
                    (10, BadgesTitles.VOCABULARY_EXPLORER),
                    (25, BadgesTitles.VOCABULARY_CHALLENGER),
                    (50, BadgesTitles.VOCABULARY_MASTER),
                }
            },
            { EnumTopic.Daily, new List<(int, string)>
                {
                    (5, BadgesTitles.DAILY_EXPLORER),
                    (15, BadgesTitles.DAILY_CHALLENGER),
                    (30, BadgesTitles.DAILY_MASTER),
                }
            },
            { EnumTopic.TextInterpretation, new List<(int, string)>
                {
                    (10, BadgesTitles.TEXT_INTERPRETATION_EXPLORER),
                    (20, BadgesTitles.TEXT_INTERPRETATION_CHALLENGER),
                    (40, BadgesTitles.TEXT_INTERPRETATION_MASTER),
                }
            },
            { EnumTopic.CodeDocumentation, new List<(int, string)>
                {
                    (10, BadgesTitles.CODE_DOCUMENTATION_EXPLORER),
                    (25, BadgesTitles.CODE_DOCUMENTATION_CHALLENGER),
                    (50, BadgesTitles.CODE_DOCUMENTATION_MASTER),
                }
            },
            { EnumTopic.Interview, new List<(int, string)>
                {
                    (5, BadgesTitles.INTERVIEW_EXPLORER),
                    (15, BadgesTitles.INTERVIEW_CHALLENGER),
                    (30, BadgesTitles.INTERVIEW_MASTER),
                }
            },
        };

        public Badge GetBadgeById(string id)
        {
            try
            {
                var badge = _badgeRepository.GetBadgeById(id);
                if(badge != null)
                    return badge;

                return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CheckBadgeAchievement(UserProgress userProgress)
        {
            var needToUpdate = false;

            foreach (var activity in userProgress.Activities)
            {
                if (!_badgeCriteria.TryGetValue(activity.Topic, out var goals))
                    continue;

                foreach (var (goal, badgeTitle) in goals)
                {
                    if (activity.TotalCorrect == goal)
                    {
                        var badge = _badgeRepository.GetBagdeByTitle(badgeTitle);

                        if (userProgress.Badges == null)
                            userProgress.Badges = new List<Badge>();

                        if(userProgress.Badges.Any(x => x.Id == badge.Id))
                            break;

                        userProgress.Badges.Add(badge);
                        needToUpdate = true;
                    }
                }
            }

            if (needToUpdate)
            {
                _userProgresRepository.Update(userProgress.Id, userProgress);
            }
        }

    }
}
