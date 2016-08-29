using  Core.BLL.Interfaces;
using  Core.DAL.Interfaces;
using  Core.POCO;


namespace  Core.BLL
{
    public class SettingsService : LogicLayer, ISettingsService
    {
        IUnitOfWork Database;
        public SettingsService(IUnitOfWork db)
        {
            Database = db;
        }


        public string[] GetVisibilityLevelNameForEachPrivacySection(int Id)
        {
            string[] visibilityLevelsNames = Database.Settings.GetVisibilityLevelNameForEachPrivacySection(Id);
            return visibilityLevelsNames;
        }

        public void AttachPrivacyToUserProfile(int profileId)
        {
            Database.Settings.AttachPrivacyToUserProfile(profileId);
            Database.Save();
        }

        public void UpdatePrivacyFlagForChoosenSection(PrivacyFlag pf)
        {
            Database.Settings.UpdatePrivacyFlagForChoosenSection(pf);
            Database.Save();
        }
    }
}
