using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.Enums
{
    public enum SearchOptions
    {
        Preset = -1,
        Default = 0,
        SearchAll = 1,
        PONumber = 2,
        TrackingNo = 3,
        IMEI = 4,
        ShiptoName = 5,
        ShiptoAddress = 6,
        RMANo = 7
    }
}