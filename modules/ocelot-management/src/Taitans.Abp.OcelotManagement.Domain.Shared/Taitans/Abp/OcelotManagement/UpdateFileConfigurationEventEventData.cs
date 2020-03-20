using System;
using Volo.Abp.EventBus;

namespace Taitans.Abp.OcelotManagement
{
    [EventName("OcelotManagement.UpdateFileConfigurationEventEventData")]
    public class UpdateFileConfigurationEventEventData
    {
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }


        public UpdateFileConfigurationEventEventData(string name, DateTime? updateTime)
        {
            Name = name;
            UpdateTime = updateTime ?? DateTime.Now;
        }
    }
}
