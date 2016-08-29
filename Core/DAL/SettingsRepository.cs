using  Core.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using  Core.POCO;

namespace  Core.DAL
{
    public class SettingsRepository :ISettingsRepository
    {
        DBContext db;
        public SettingsRepository(DBContext db)
        {
            this.db = db;
        }
        public string[] GetVisibilityLevelNameForEachPrivacySection(int Id)
        {
            //db.PrivacyFlags содержит список разделов (settings.aspx), для которых можно изменять видимость
            List<PrivacyFlag> privacyFlags = db.PrivacyFlags.Where(p => p.ProfileID == Id).ToList();
            List<VisibilityLevel> visLevels = db.VisibilityLevels.ToList();
            
            var visibilityLevelsIDs = new List<int>();//1,2,3

            foreach (PrivacyFlag i in privacyFlags)
            {
                visibilityLevelsIDs.Add(i.VisibilityLevelID);
            }

            string[] visibilityLevelsNames = new string[visibilityLevelsIDs.Count];

            for (int i = 0; i < visibilityLevelsIDs.Count; i++)
            {
                int value = visibilityLevelsIDs[i];

                string visibName = visLevels.FirstOrDefault(o => o.ID == value).Name;

                visibilityLevelsNames[i] = visibName;
            }

            return visibilityLevelsNames;
        }

        public void AttachPrivacyToUserProfile(int profileId)
        {
            var flags = new List<PrivacyFlag>
            {
                new PrivacyFlag  { PrivacyFlagTypeID = 1 },
                new PrivacyFlag  { PrivacyFlagTypeID = 2 },
                new PrivacyFlag  { PrivacyFlagTypeID = 3 },
                new PrivacyFlag  { PrivacyFlagTypeID = 4 },
                new PrivacyFlag  { PrivacyFlagTypeID = 5 },
                new PrivacyFlag  { PrivacyFlagTypeID = 6 },
                new PrivacyFlag  { PrivacyFlagTypeID = 7 }
            };

            foreach (var pf in flags)
            {
                pf.ProfileID = profileId;
                pf.VisibilityLevelID = 1;
            }

            db.PrivacyFlags.AddRange(flags);

        }
        public void UpdatePrivacyFlagForChoosenSection(PrivacyFlag pf)
        {
            PrivacyFlag prFlag = db.PrivacyFlags.FirstOrDefault(a => a.PrivacyFlagTypeID == pf.PrivacyFlagTypeID && a.ProfileID == pf.ProfileID);

            if (prFlag != null)           
                prFlag.VisibilityLevelID = pf.VisibilityLevelID;
            
        }

      
    }
}
