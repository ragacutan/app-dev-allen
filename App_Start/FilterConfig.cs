using System.Web;
using System.Web.Mvc;

namespace SEE_TURTLES_WEB_APP_FINAL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
