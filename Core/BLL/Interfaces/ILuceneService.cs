

using  Core.BLL.DTO;
using System.Collections.Generic;



namespace  Core.BLL.Interfaces
{
    public interface ILuceneService
    {
        List<UserDTO> UsersFromLuceneIndex(List<UserDTO> users, string name);
    }
}