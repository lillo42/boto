namespace Boto.Widget;

public static class ChartExtensions
{
    public static Chart Block(this Chart chart, Block block)
    {
        chart.Block = block;
        return chart;
    }
    
    public static Chart XAxis(this Chart chart, Axis axis)
    {
        chart.XAxis = axis;
        return chart;
    }
    
    public static Chart YAxis(this Chart chart, Axis axis)
    {
        chart.YAxis = axis;
        return chart;
    }
    
    public static Chart Dataset(this Chart chart, List<Dataset> dataset)
    {
        chart.Datasets = dataset;
        return chart;
    }
    
    
}
