using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao;

[Table("measure")]
public class Measure
{
    public int MeasureId { get; set;}
    public string MeasureName { get; set; } = null!;
}