using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace nameEventAssignmentWeb.Services
{
    public class AppEventReceiver : IRemoteEventService
    {
        /// <summary>
        /// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
        /// </summary>
        /// <param name="properties">Holds information about the app event.</param>
        /// <returns>Holds information returned from the app event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();

            if (properties.EventType == SPRemoteEventType.ItemAdded || properties.EventType == SPRemoteEventType.ItemUpdated)
            {
                using (ClientContext ctx = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
                {

                    Helpers.CreateFullNameHelper.CombineName(properties.ItemEventProperties.ListItemId, ctx);


                }


            }

            using (ClientContext ctx = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: false))
            {
                if (ctx != null)
                {
                    if (properties.EventType == SPRemoteEventType.AppInstalled)
                    {
                        Helpers.AppEventHelper.Install(ctx);
                    }
                    if (properties.EventType == SPRemoteEventType.AppUninstalling)
                    {
                        Helpers.AppEventHelper.UnInstall(ctx);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method is a required placeholder, but is not used by app events.
        /// </summary>
        /// <param name="properties">Unused.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }

    }
}
