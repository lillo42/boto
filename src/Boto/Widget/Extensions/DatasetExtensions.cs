using Boto.Styles;

namespace Boto.Widget;

public static class DatasetExtensions
{
    public static Dataset Name(this Dataset dataset, string name)
    {
        dataset.Name = name;
        return dataset;
    }
    
    public static Dataset GraphType(this Dataset dataset, GraphType graphType)
    {
        dataset.GraphType = graphType;
        return dataset;
    }
    
    public static Dataset Style(this Dataset dataset, Style style)
    {
        dataset.Style = style;
        return dataset;
    }
    
    public static Dataset Marker(this Dataset dataset, Symbols.Marker marker)
    {
        dataset.Marker = marker;
        return dataset;
    }
    
    public static Dataset Data(this Dataset dataset, (double, double)[] data)
    {
        dataset.Data = data;
        return dataset;
    }
     public static Dataset Data(this Dataset dataset, List<(double, double)> data)
        {
            dataset.Data = data.ToArray();
            return dataset;
        }
}
