
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using  Core.BLL.DTO;
using  Core.BLL.Interfaces;
using System.Collections.Generic;

namespace  Core.BLL
{
    public class LuceneService : ILuceneService
    {
        public List<UserDTO> UsersFromLuceneIndex(List<UserDTO> users, string name)
        {
            var directory = CreateIndex(users);
            var resultUsers = SearchInIndex(directory, name);
            return resultUsers;
        }
        private RAMDirectory CreateIndex(List<UserDTO> _users)
        {
            RAMDirectory ramDirectory = new RAMDirectory();

            using (Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))


            using (var indexWriter = new IndexWriter(ramDirectory, analyzer, new IndexWriter.MaxFieldLength(1000)))
            {
                foreach (var user in _users)
                {
                    var document = new Document();

                    document.Add(new Field("UserID", user.UserID, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("ImageUrl", user.ImageUrl, Field.Store.YES, Field.Index.NOT_ANALYZED));

                    document.Add(new Field("FirstName", user.FirstName, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("LastName", user.LastName, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("UserName", user.UserName, Field.Store.YES, Field.Index.ANALYZED));

                    indexWriter.AddDocument(document);
                }
                indexWriter.Optimize();
                indexWriter.Flush(true, true, true);
            }

            return ramDirectory;
        }
        private List<UserDTO> SearchInIndex(RAMDirectory ramDirectory, string textSearch)
        {
            List<UserDTO> resultUsers = new List<UserDTO>();
            
            using (var indexReader = IndexReader.Open(ramDirectory, true))//true - если предполагается только чтение документов, т.е. без записи или удаления
            using (var indexSearcher = new IndexSearcher(indexReader))
            using (Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            {
                MultiFieldQueryParser queryParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] { "FirstName", "LastName", "UserName" }, analyzer);
                         
                queryParser.AllowLeadingWildcard = true;

                Query query = queryParser.Parse(textSearch);
              
                TopScoreDocCollector collector = TopScoreDocCollector.Create(1000, true);

                indexSearcher.Search(query, collector);
                
                TopDocs topDocs = collector.TopDocs();
                
                ScoreDoc[] scoreDocs = topDocs.ScoreDocs;
                
                foreach (ScoreDoc s in scoreDocs)
                {
                    int numberOfDocument = s.Doc; 
                    
                    Document doc = indexSearcher.Doc(numberOfDocument);
                    
                    #region Final filter

                    string userId = doc.GetField("UserID").StringValue;
                    string imageUrl = doc.GetField("ImageUrl").StringValue;
                    string firstName = doc.GetField("FirstName").StringValue;
                    string lastName = doc.GetField("LastName").StringValue;
                    string userName = doc.GetField("UserName").StringValue;

                    #endregion

                    if (textSearch.Contains(" "))//если два слова в поиске
                    {
                        string pattern = textSearch.Replace(" ", "");

                        string combination1 = (firstName + lastName).ToLower();
                        string combination2 = (lastName + firstName).ToLower();

                        if (pattern == combination1 || pattern == combination2)

                            resultUsers.Add(new UserDTO
                            {
                                UserID = userId,
                                ImageUrl = imageUrl,
                                FirstName = firstName,
                                LastName = lastName,
                                UserName = userName
                            });
                    }
                    else //если задано только имя или фамилия
                    {
                        resultUsers.Add(new UserDTO
                        {
                            //ЧТЕНИЕ ДАННЫХ ИЗ ДОКУМЕНТА
                            UserID = userId,
                            ImageUrl = imageUrl,
                            FirstName = firstName,
                            LastName = lastName,
                            UserName = userName
                        });
                    }
                }
            }

            return resultUsers;
        }
     


       
    }
}
