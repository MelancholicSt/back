﻿namespace WebApplication1.Data.Dto;

public class GeolocationDto
{
    public string Status { get; set; } = null!;
    public string? City { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}