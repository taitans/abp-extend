using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteSecurityOption : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string ReRouteName { get; protected set; }
        public virtual List<ReRouteSecurityOptionIPAllowed> IPAllowedList { get; protected set; }
        public virtual List<ReRouteSecurityOptionIPBlocked> IPBlockedList { get; protected set; }

        public ReRouteSecurityOption()
        {
            IPAllowedList = new List<ReRouteSecurityOptionIPAllowed>();
            IPBlockedList = new List<ReRouteSecurityOptionIPBlocked>();
        }

        public virtual void AddIPAllowed(string ip)
        {
            IPAllowedList.Add(new ReRouteSecurityOptionIPAllowed(ip));
        }

        public virtual void RemoveAllIPAlloweds()
        {
            IPAllowedList.Clear();
        }

        public void RemoveIPAlloweds(string ip)
        {
            IPAllowedList.RemoveAll(c => c.IP == ip);
        }

        public void FindIPAllowed(string ip)
        {
            IPAllowedList.FirstOrDefault(c => c.IP == ip);
        }

        public virtual void AddIPBlocked(string ip)
        {
            IPBlockedList.Add(new ReRouteSecurityOptionIPBlocked(ip));
        }

        public virtual void RemoveAllIPBlockeds()
        {
            IPBlockedList.Clear();
        }

        public void RemoveIPBlockeds(string ip)
        {
            IPBlockedList.RemoveAll(c => c.IP == ip);
        }

        public void FindIPBlocked(string ip)
        {
            IPBlockedList.FirstOrDefault(c => c.IP == ip);
        }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId, ReRouteName };
        }
    }
}
