using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Application.Contracts.Services;

using Azure;
using Azure.Core.GeoJson;
using Azure.Maps.Geolocation;
using Azure.Maps.Search;
using Azure.Maps.Search.Models;
using Azure.Maps.TimeZones;

namespace Application.Services;


public class AzureMapsService : IAzureMapsService
{
    private readonly MapsSearchClient _searchClient;
    private readonly MapsGeolocationClient _geolocationClient;
    private readonly MapsTimeZoneClient _timeZoneClient;

    private readonly string _subscriptionKey;

    public AzureMapsService(AzureKeyCredential azureMapsKeyCredential)
    {
        _searchClient = new MapsSearchClient(azureMapsKeyCredential);
        _geolocationClient = new MapsGeolocationClient(azureMapsKeyCredential);
        _timeZoneClient = new MapsTimeZoneClient(azureMapsKeyCredential);

        _subscriptionKey = azureMapsKeyCredential.Key;
    }
}
