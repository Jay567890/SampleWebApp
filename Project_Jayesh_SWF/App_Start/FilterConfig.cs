using System.Web;
using System.Web.Mvc;

namespace Project_Jayesh_SWF
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
