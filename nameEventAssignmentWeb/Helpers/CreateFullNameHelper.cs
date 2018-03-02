using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nameEventAssignmentWeb.Helpers
{
    public class CreateFullNameHelper
    {
        public static void CombineName(int itemId, ClientContext ctx)
        {

            List list = ctx.Web.GetListByTitle("Name List");
            ListItem item = list.GetItemById(itemId);
            ctx.Load(item);
            ctx.ExecuteQuery();

            string fname = item["FName"].ToString();
            string lname = item["LName"].ToString();
            string fullname = fname + " " + lname;

            if (item["FullName"] == null || fullname != item["FullName"].ToString())
            {

                item["FullName"] = fullname;
                item.Update();
                ctx.ExecuteQuery();

                //item.SystemUpdate(); new csom thing that came out 4 months ago

            }

        }

    }
}