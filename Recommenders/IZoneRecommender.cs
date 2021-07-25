using System.Collections.Generic;
using xZoneAPI.Models.Zones;

namespace xZoneAPI.Recommenders
{
    public interface IZoneRecommender
    {
        List<Zone> getRecommendedZones(int userId);
    }
}