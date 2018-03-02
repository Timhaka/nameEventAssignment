using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nameEventAssignmentWeb.Helpers
{
    public class AppEventHelper
    {
        public static void Install(ClientContext ctx)
        {


            //add remove event to our list
            List list = ctx.Web.GetListByTitle("Name List");
            string url = GetWebServiceUrl();

            list.AddRemoteEventReceiver("added4", url, EventReceiverType.ItemAdded, EventReceiverSynchronization.Synchronous, true);
            list.AddRemoteEventReceiver("updated4", url, EventReceiverType.ItemUpdated, EventReceiverSynchronization.Synchronous, true);

        }
        public static void UnInstall(ClientContext ctx)
        {
            List list = ctx.Web.GetListByTitle("Name List");
            list.GetEventReceiverByName("added4").DeleteObject();
            list.GetEventReceiverByName("updated4").DeleteObject();
            ctx.ExecuteQuery();

        }

        private static string GetWebServiceUrl()
        {
            System.ServiceModel.OperationContext op = System.ServiceModel.OperationContext.Current;
            System.ServiceModel.Channels.Message msg = op.RequestContext.RequestMessage;
            Uri url = msg.Headers.To;
            return url.ToString();
        }

    }


}
