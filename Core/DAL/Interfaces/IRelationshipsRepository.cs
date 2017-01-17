using System.Collections.Generic;
using  Core.POCO;
using System.Linq;

namespace  Core.DAL.Interfaces
{
    public interface IRelationshipsRepository
    {
        string GetRelationshipDefinitionOfTheSenderToReceiver(string SenderId, string ReceiverId);
        bool CheckIfTheSameInvitationAlreadyExistsInDB(Message msg);
        void AddToFriendsMessage(Message message);
        List<string> GetFriendsIDsOfUser(string UserID);
        int GetRelationshipDefinitionIdOfPageVisitor(string visitorId, string pageOfUserId);
        Relationship GetRelationship(string FirstUserID, string SecondUserID);
        void RemoveFromFriends(Relationship friendship);
        void SaveRelationshipDefinition(string senderUserId, string receiverUserId, int relationshipId);
    }
}