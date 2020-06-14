using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class RouteSecurityOption : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string RouteName { get; protected set; }
        public virtual List<RouteSecurityOptionIPAllowed> IPAllowedList { get; protected set; }
        public virtual List<RouteSecurityOptionIPBlocked> IPBlockedList { get; protected set; }

        public RouteSecurityOption(Guid globalConfigurationId, string routeName)
        {
            GlobalConfigurationId = globalConfigurationId;
            RouteName = routeName;

            IPAllowedList = new List<RouteSecurityOptionIPAllowed>();
            IPBlockedList = new List<RouteSecurityOptionIPBlocked>();
        }

        public virtual void AddIPAllowed(string ip)
        {
            IPAllowedList.Add(new RouteSecurityOptionIPAllowed(GlobalConfigurationId, RouteName, ip));
        }

        public virtual void RemoveAllIPAlloweds()
        {
            IPAllowedList.Clear();
        }

        public void RemoveIPAllowed(string ip)
        {
            IPAllowedList.RemoveAll(c => c.IP == ip);
        }

        public RouteSecurityOptionIPAllowed FindIPAllowed(string ip)
        {
            return IPAllowedList.FirstOrDefault(c => c.IP == ip);
        }

        public virtual void AddIPBlocked(string ip)
        {
            IPBlockedList.Add(new RouteSecurityOptionIPBlocked(GlobalConfigurationId, RouteName, ip));
        }

        public virtual void RemoveAllIPBlocked()
        {
            IPBlockedList.Clear();
        }

        public void RemoveIPBlocked(string ip)
        {
            IPBlockedList.RemoveAll(c => c.IP == ip);
        }

        public RouteSecurityOptionIPBlocked FindIPBlocked(string ip)
        {
            return IPBlockedList.FirstOrDefault(c => c.IP == ip);
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, RouteName };
        }
    }
}
