using System;
using System.Collections.Generic;

namespace WeatherApp.Models;
public class Forecast
{
    // Root class represents the entire JSON response
    public record Root
    (
        string Type,
        Geometry Geometry,
        Properties Properties
    );

    public record Properties(
        Meta Meta,
        IReadOnlyList<Timeseries> Timeseries
    );

    public record Timeseries
    (
        DateTime Time,
        Data Data
    );

    public record Data
    (
        Instant Instant,
        Next1Hours Next_1_hours,
        Next6Hours Next_6_hours,
        Next12Hours Next_12_hours
    );

    public record Instant(Details Details);
    public record Next1Hours(Summary Summary, Details Details);
    public record Next6Hours(Summary Summary, Details Details);
    public record Next12Hours(Summary Summary, Details Details);

    public record Summary(string Symbol_code);

    public record Details
    (
        double Air_pressure_at_sea_level,
        double Air_temperature,
        double Cloud_area_fraction,
        double Relative_humidity,
        double Wind_from_direction,
        double Wind_speed,
        double Precipitation_amount
    );

    public record Units
    (
        string Air_pressure_at_sea_level,
        string Air_temperature,
        string Cloud_area_fraction,
        string Precipitation_amount,
        string Relative_humidity,
        string Wind_from_direction,
        string Wind_speed
    );
    public record Geometry
    (
        string Type,
        IReadOnlyList<double> Coordinates
    );

    public record Meta
    (
        DateTime Updated_at,
        Units Units
    );

}

// Usage for deserialization
// var forecast = JsonConvert.DeserializeObject<Forecast.Root>(jsonResponse);
