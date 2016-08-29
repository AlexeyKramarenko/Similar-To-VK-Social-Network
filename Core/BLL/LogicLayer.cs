
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace  Core.BLL
{
    public abstract class LogicLayer
    {
        protected const int MAXROWS = int.MaxValue;

        protected static Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }
        

        /// <summary>
        /// удаление элементов из кеша, чьи ключи начинаются с префикса
        /// </summary>
        protected static void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> itemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = LogicLayer.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                    itemsToRemove.Add(enumerator.Key.ToString());
            }

            foreach (string itemToRemove in itemsToRemove)
                LogicLayer.Cache.Remove(itemToRemove);
        }

      
    }
}
