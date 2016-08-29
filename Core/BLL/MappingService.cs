
using AutoMapper;
using  Core.BLL.Interfaces;

namespace  Core.BLL
{

    public class MappingService : IMappingService
    {
        public TDest Map<TSrc, TDest>(TSrc source) where TDest : class
        {
            return Mapper.Map<TSrc, TDest>(source);
        }
    }

}
