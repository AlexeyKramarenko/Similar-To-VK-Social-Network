
using Core.BLL.DTO;
using Core.BLL.Interfaces;
using Core.DAL.Interfaces;
using Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BLL
{
    public class RelationshipsService : LogicLayer, IRelationshipsService
    {
        IUnitOfWork Database;
        public RelationshipsService(IUnitOfWork db)
        {
            Database = db;
        }

        public List<string> GetFriendsIDsOfUser(string UserID)
        {
            string key = "FriendsIDs_" + UserID;

            List<string> ids = null;

            if (Cache[key] != null)
                ids = (List<string>)Cache[key];
            else
            {
                ids = Database.Relationships.GetFriendsIDsOfUser(UserID);
                Cache[key] = ids;
            }
            return ids;
        }
        public void AddToFriendsMessage(Message message)
        {
            Database.Relationships.AddToFriendsMessage(message);
            Database.Save();

            PurgeCacheItems("FriendsIDs_" + message.ReceiversUserID);
            PurgeCacheItems("FriendsIDs_" + message.SendersUserID);
        }

        public void RemoveFromFriends(IQueryable<Relationship> relationship, string FirstUserID, string SecondUserID)
        {
            Database.Relationships.RemoveFromFriends(relationship);
            Database.Save();

            PurgeCacheItems("FriendsIDs_" + FirstUserID);
            PurgeCacheItems("FriendsIDs_" + SecondUserID);
        }
        public void SaveRelationshipDefinition(string senderUserId, string receiverUserId, int relationshipId)
        {
            Database.Relationships.SaveRelationshipDefinition(senderUserId, receiverUserId, relationshipId);
            Database.Save();
        }
        public int GetRelationshipDefinitionIdOfPageVisitor(string visitorId, string pageOfUserId)
        {
            string key = "RelationshipDefinitionIdOfPageVisitor_" + visitorId + "_" + pageOfUserId;

            int id;

            if (Cache[key] != null)
                id = (int)Cache[key];
            else
            {
                id = Database.Relationships.GetRelationshipDefinitionIdOfPageVisitor(visitorId, pageOfUserId);
                Cache[key] = id;
            }

            return id;
        }
        public PrivacyRestrictions GetPrivacyRestrictionsOfCurrentPage(string pageOfUserId, string CurrentUserId, int RelationshipDefinitionIDOfCurrentVisitor)
        {
            string key = "PrivacyRestrictionsOfCurrentPage_" + pageOfUserId + "_" + CurrentUserId;

            PrivacyRestrictions privacy;

            if (Cache[key] != null)
                privacy = (PrivacyRestrictions)Cache[key];
            else
            {
                privacy = __GetPrivacyRestrictionsOfCurrentPage(pageOfUserId, CurrentUserId, RelationshipDefinitionIDOfCurrentVisitor);
                Cache[key] = privacy;
            }

            return privacy;
        }

        private PrivacyRestrictions __GetPrivacyRestrictionsOfCurrentPage(string pageOfUserId, string CurrentUserId, int RelationshipDefinitionIDOfCurrentVisitor)
        {
            if (pageOfUserId != null && pageOfUserId != CurrentUserId)
            {
                var profile = Database.Profiles.GetProfileByUserId(pageOfUserId);

                if (profile != null)
                {
                    int profileId = profile.ProfileID;

                    List<PrivacyFlag> privacyFlags = Database.Privacy.GetPrivacyCollection(profileId);

                    List<UserRights> userRights = Database.UserRights.GetUserRights();

                    var restrictions = new PrivacyRestrictions();

                    var A = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[0].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (A != null)
                        restrictions.DisplayDetailsInfo = true;
                    else
                        restrictions.DisplayDetailsInfo = false;


                    var B = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[1].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (B != null)
                        restrictions.DisplayPosts = true;
                    else
                        restrictions.DisplayPosts = false;


                    var C = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[2].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (C != null)
                        restrictions.DisplayMessageForm = true;
                    else
                        restrictions.DisplayMessageForm = false;


                    var D = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[3].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (D != null)
                        restrictions.DisplayComments = true;
                    else
                        restrictions.DisplayComments = false;


                    var E = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[4].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (E != null)
                        restrictions.DisplayCommentLink = true;
                    else
                        restrictions.DisplayCommentLink = false;


                    var F = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[5].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (F != null)
                        restrictions.SendMessagePossibility = true;
                    else
                        restrictions.SendMessagePossibility = false;


                    var G = userRights.FirstOrDefault(a => a.VisibilityLevelId == privacyFlags[6].VisibilityLevelID && a.RelationshipDefinitionID == RelationshipDefinitionIDOfCurrentVisitor);

                    if (G != null)
                        restrictions.ShowInSearch = true;
                    else
                        restrictions.ShowInSearch = false;

                    return restrictions;
                }
            }

            return null;

            //level = 3 : =1
            //level = 2 : <=2
            //level = 1 : <=4

            //  if (RelationshipDefinitionIDOfCurrentVisitor== )
            //privacyFlags[0] . 
        }

        public bool CheckIfTheSameInvitationAlreadyExistsInDB(Message msg)
        {
            return Database.Relationships.CheckIfTheSameInvitationAlreadyExistsInDB(msg);
        }


        public string GetRelationshipDefinitionOfTheSenderToReceiver(string SenderId, string ReceiverId)
        {
            string id = Database.Relationships.GetRelationshipDefinitionOfTheSenderToReceiver(SenderId, ReceiverId);
            return id;
        }

        public IQueryable<Relationship> GetRelationship(string FirstUserID, string SecondUserID)
        {
            return Database.Relationships.GetRelationship(FirstUserID, SecondUserID);
        }

        public List<FriendsDTO> CreateLinks(int firstFriendsToShow, string UserID, out int usersCount)
        {
            var IDs = GetFriendsIDsOfUser(UserID);

            usersCount = IDs.Count;

            var friendsPageUrl = "/people/UserID=" + UserID + "/Online=false";

            IDs = IDs.Take(firstFriendsToShow).ToList();

            var list = new List<FriendsDTO>();

            foreach (var id in IDs)
            {
                string imageUrl = Database.Photos.GetThumbAvatarImg(id);

                list.Add(new FriendsDTO(imageUrl, friendsPageUrl));
            }

            return list;
        }
    }
}
