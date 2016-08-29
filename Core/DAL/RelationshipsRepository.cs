using  Core.DAL.Interfaces;
using  Core.POCO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace  Core.DAL
{
    public class RelationshipsRepository : IRelationshipsRepository
    {
        DBContext db;

        PhotoRepository photoRepository;
        ProfileRepository profileRepository; 

        public RelationshipsRepository(DBContext db, IUserRepository userRepository)
        {
            this.db = db;
            photoRepository = new PhotoRepository(db);
            profileRepository = new ProfileRepository(db, userRepository); 
        }

        public void SaveRelationshipDefinition(string senderUserId, string receiverUserId, int relationshipId)
        {
            var relationship = db.Relationships.FirstOrDefault(a => a.SenderAccountID == senderUserId && a.ReceiverAccountID == receiverUserId);

            if (relationship == null)
                relationship = db.Relationships.FirstOrDefault(a => a.SenderAccountID == receiverUserId && a.ReceiverAccountID == senderUserId);

            //INSERT
            if (relationship == null)
                db.Relationships.Add(new Relationship { RelationshipDefinitionID = relationshipId, SenderAccountID = senderUserId, ReceiverAccountID = receiverUserId });

            //UPDATE
            else
                relationship.RelationshipDefinitionID = relationshipId;

        }
        public bool CheckIfTheSameInvitationAlreadyExistsInDB(Message msg)
        {
            var invititionMsg = db.Messages.FirstOrDefault(a => a.Invitation == msg.Invitation && a.SendersUserID == msg.SendersUserID && a.ReceiversUserID == msg.ReceiversUserID);

            //Проверка того, что приглашение в друзья не отправил ранее тот пользователь, которому собственно и посылается этот запрос.
            //Цель - избежать дублирования приглашения.
            if (invititionMsg == null)
                invititionMsg = db.Messages.FirstOrDefault(a => a.Invitation == msg.Invitation && a.SendersUserID == msg.ReceiversUserID && a.ReceiversUserID == msg.SendersUserID);

            if (invititionMsg != null)
                return true;

            return false;
        }
        public List<string> GetFriendsIDsOfUser(string UserID)
        {
            List<string> friends = new List<string>();

            var senders = db.Relationships.Where(a => a.SenderAccountID == UserID && a.RelationshipDefinitionID == 1).ToArray();
            var receivers = db.Relationships.Where(a => a.ReceiverAccountID == UserID && a.RelationshipDefinitionID == 1).ToArray();

            for (int i = 0; i < senders.Length; i++)
            {
                friends.Add(senders[i].ReceiverAccountID);
            }
            for (int i = 0; i < receivers.Length; i++)
            {
                friends.Add(receivers[i].SenderAccountID);
            }

            return friends;
        }

        public int GetRelationshipDefinitionIdOfPageVisitor(string visitorId, string pageOfUserId)
        {
            Relationship relationship = db.Relationships.FirstOrDefault(a => a.SenderAccountID == visitorId && a.ReceiverAccountID == pageOfUserId);

            if (relationship == null)
                relationship = db.Relationships.FirstOrDefault(a => a.SenderAccountID == pageOfUserId && a.ReceiverAccountID == visitorId);

            if (relationship == null)
                return 3; //3 - unknown user

            return relationship.RelationshipDefinitionID;
        }
        public string GetRelationshipDefinitionOfTheSenderToReceiver(string SenderId, string ReceiverId)
        {
            var relationship = db.Relationships.FirstOrDefault(a => a.SenderAccountID == SenderId && a.ReceiverAccountID == ReceiverId);

            if (relationship != null)
            {
                var id = relationship.RelationshipDefinitionID;
                var def = db.RelationshipDefinitions.FirstOrDefault(a => a.RelationshipDefinitionID == id);

                if (def != null)
                {
                    return def.Definition;
                }
            }

            return null;
        }

        public void AddToFriendsMessage(Message message)
        {
            db.Entry(message).State = System.Data.Entity.EntityState.Added;
        }

        public void RemoveFromFriends(IQueryable<Relationship> friendship)
        {
            db.Entry(friendship).State = System.Data.Entity.EntityState.Deleted;
        }

        public IQueryable<Relationship> GetRelationship(string FirstUserID, string SecondUserID)
        {
            IQueryable<Relationship> friendship = db.Relationships.Where(a => a.ReceiverAccountID == FirstUserID && a.SenderAccountID == SecondUserID);

            if (friendship == null)
                friendship = db.Relationships.Where(a => a.ReceiverAccountID == SecondUserID && a.SenderAccountID == FirstUserID);

            return friendship;
        }



    }
}
