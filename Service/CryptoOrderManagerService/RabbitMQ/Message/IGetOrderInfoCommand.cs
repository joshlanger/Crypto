using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Message
{
    /// <summary>
    /// Command to initiate pulling of order info from crypto trading platforms
    /// If PlatformInterfaceID is null, order info will be pulled from ALL configured platforms
    /// </summary>
    public interface IGetOrderInfoCommand
    {
        int ? PlatformInterfaceID { get; set; }
    }
}
