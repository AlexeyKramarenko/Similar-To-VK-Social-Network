using System.Collections.Generic;
using  Core.POCO;
using System.Linq;
using  Core.BLL.DTO;

namespace  Core.BLL.Interfaces
{
    public interface IRelationshipsService
    {
        List<FriendsDTO> CreateLinks(int firstFriendsToShow, string UserID,out int usersCount);
        Relationship GetRelationship(string FirstUserID, string SecondUserID);
        void AddToFriendsMessage(Message message);
        bool CheckIfTheSameInvitationAlreadyExistsInDB(Message msg);
        List<string> GetFriendsIDsOfUser(string UserID);
        int GetRelationshipDefinitionIdOfPageVisitor(string visitorId, string pageOfUserId);
        string GetRelationshipDefinitionOfTheSenderToReceiver(string SenderId, string ReceiverId);
        void RemoveFromFriends(Relationship relationship, string FirstUserID, string SecondUserID);
        void SaveRelationshipDefinition(string senderUserId, string receiverUserId, int relationshipId);
        PrivacyRestrictions GetPrivacyRestrictionsOfCurrentPage(string pageOfUserId, string CurrentUserId, int RelationshipDefinitionIDOfCurrentVisitor);
    }
}