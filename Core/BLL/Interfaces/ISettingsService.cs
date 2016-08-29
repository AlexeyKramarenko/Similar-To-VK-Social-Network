using  Core.POCO;

namespace  Core.BLL.Interfaces
{
    public interface ISettingsService
    {
        void AttachPrivacyToUserProfile(int profileId);
        string[] GetVisibilityLevelNameForEachPrivacySection(int Id);
        void UpdatePrivacyFlagForChoosenSection(PrivacyFlag pf);
    }
}