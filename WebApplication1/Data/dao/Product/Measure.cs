using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao;


public class Measure
{
    public int MeasureId { get; set;}
    public string MeasureName { get; set; } = null!;
}