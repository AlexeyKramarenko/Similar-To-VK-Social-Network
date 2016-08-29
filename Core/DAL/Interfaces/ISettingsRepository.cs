using  Core.POCO;

namespace  Core.DAL.Interfaces
{
    public interface ISettingsRepository
    {
        void AttachPrivacyToUserProfile(int profileId);
        string[] GetVisibilityLevelNameForEachPrivacySection(int Id);
        void UpdatePrivacyFlagForChoosenSection(PrivacyFlag pf);
    }
}