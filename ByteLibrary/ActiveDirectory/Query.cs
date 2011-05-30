using System.Collections.Generic;
using System.DirectoryServices;

namespace ByteLibrary.ActiveDirectory
{
    public class Query
    {
        public static IEnumerable<string> RunQuery(DirectoryEntry root, string filter, SearchScope scope)
        {
            var paths = new List<string>();

            using (var searcher = new DirectorySearcher())
            {
                searcher.SearchRoot = root;
                searcher.Filter = filter;
                searcher.SearchScope = scope;
                searcher.PageSize = 999;

                using (SearchResultCollection results = searcher.FindAll())
                {
                    foreach (SearchResult result in results)
                    {
                        paths.Add(result.Path);
                    }
                }
            }

            return paths;
        }

        public static string PathToDN(string path)
        {
            return path.Remove(0, 7);
        }

        public static string DNToPath(string DN)
        {
            return string.Format("LDAP://{0}", DN);
        }
    }
}
