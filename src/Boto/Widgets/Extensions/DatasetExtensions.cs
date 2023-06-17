using Boto.Styles;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Dataset"/> extensions method.
/// </summary>
public static class DatasetExtensions
{
    /// <summary>
    /// Change the <see cref="Dataset.Name"/>.
    /// </summary>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <param name="name">The name.</param>
    /// <returns>The <paramref name="dataset"/> with the <see cref="Dataset.Name"/> as <paramref name="name"/>.</returns>
    public static Dataset SetName(this Dataset dataset, string name)
    {
        dataset.Name = name;
        return dataset;
    }

    /// <summary>
    /// Change the <see cref="Dataset.GraphType"/>.
    /// </summary>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <param name="graphType">The <see cref="GraphType"/>.</param>
    /// <returns>The <paramref name="dataset"/> with the <see cref="Dataset.GraphType"/> as <paramref name="graphType"/>.</returns>
    public static Dataset SetGraphType(this Dataset dataset, GraphType graphType)
    {
        dataset.GraphType = graphType;
        return dataset;
    }

    /// <summary>
    /// Change the <see cref="Dataset.Style"/>.
    /// </summary>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <param name="style">The <see cref="GraphType"/>.</param>
    /// <returns>The <paramref name="dataset"/> with the <see cref="Dataset.Style"/> as <paramref name="style"/>.</returns>
    public static Dataset SetStyle(this Dataset dataset, Style style)
    {
        dataset.Style = style;
        return dataset;
    }

    /// <summary>
    /// Change the <see cref="Dataset.Marker"/>.
    /// </summary>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <param name="marker">The <see cref="Symbols.Marker"/>.</param>
    /// <returns>The <paramref name="dataset"/> with the <see cref="Dataset.Marker"/> as <paramref name="marker"/>.</returns>
    public static Dataset SetMarker(this Dataset dataset, Symbols.Marker marker)
    {
        dataset.Marker = marker;
        return dataset;
    }

    /// <summary>
    /// Change the <see cref="Dataset.Data"/>.
    /// </summary>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <param name="data">The data.</param>
    /// <returns>The <paramref name="dataset"/> with the <see cref="Dataset.Data"/> as <paramref name="data"/>.</returns>
    public static Dataset SetData(this Dataset dataset, IEnumerable<(double, double)> data)
    {
        dataset.Data = data.ToArray();
        return dataset;
    }

    /// <summary>
    /// Change the <see cref="Dataset.Data"/>.
    /// </summary>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <param name="data">The data.</param>
    /// <returns>The <paramref name="dataset"/> with the <see cref="Dataset.Data"/> <paramref name="data"/>.</returns>
    public static Dataset SetData(this Dataset dataset, params (double, double)[] data)
    {
        dataset.Data = data;
        return dataset;
    }
}
