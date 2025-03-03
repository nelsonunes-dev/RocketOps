﻿namespace RocketOps.Core.Domain.Models;

public class HealthCheck
{
    public string Name { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Exception { get; set; } = default!;
    public string Duration { get; set; } = default!;
    public string Description { get; set; } = default!;
}
