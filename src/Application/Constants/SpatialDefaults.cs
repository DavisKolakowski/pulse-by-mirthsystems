using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants;

public static class SpatialDefaults
{
    /// <summary>
    /// WGS 84 - World Geodetic System 1984 (used by GPS)
    /// </summary>
    public const int Srid = 4326;

    /// <summary>
    /// Conversion factor from miles to meters
    /// </summary>
    public const double MilesToMeters = 1609.344;

    /// <summary>
    /// Conversion factor from meters to miles
    /// </summary>
    public const double MetersToMiles = 0.000621371;

    /// <summary>
    /// Earth's radius in miles
    /// </summary>
    public const double EarthRadiusMiles = 3958.8;

    /// <summary>
    /// Maximum search radius in miles
    /// </summary>
    public const double MaxSearchRadiusMiles = 50.0;

    /// <summary>
    /// Minimum search radius in miles
    /// </summary>
    public const double MinSearchRadiusMiles = 0.1;
}