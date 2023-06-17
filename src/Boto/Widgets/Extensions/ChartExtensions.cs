using Boto.Layouts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Chart"/> extensions method.
/// </summary>
public static class ChartExtensions
{
    /// <summary>
    /// Change <see cref="Chart.Block"/>.
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.Block"/> as <paramref name="block"/>.</returns>
    public static Chart SetBlock(this Chart chart, Block block)
    {
        chart.Block = block;
        return chart;
    }

    /// <summary>
    /// Change <see cref="Chart.XAxis"/>.
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.XAxis"/> as <paramref name="axis"/>.</returns>
    public static Chart SetXAxis(this Chart chart, Axis axis)
    {
        chart.XAxis = axis;
        return chart;
    }

    /// <summary>
    /// Change <see cref="Chart.YAxis"/>.
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.YAxis"/> as <paramref name="axis"/>.</returns>
    public static Chart YAxis(this Chart chart, Axis axis)
    {
        chart.YAxis = axis;
        return chart;
    }

    /// <summary>
    /// Change <see cref="Chart.Datasets"/>.
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="dataset">The <see cref="Dataset"/>.</param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.Datasets"/> as <paramref name="dataset"/>.</returns>
    public static Chart SetDataset(this Chart chart, List<Dataset> dataset)
    {
        chart.Datasets = dataset;
        return chart;
    }

    /// <summary>
    /// Add <see cref="Dataset"/> to <see cref="Chart.Datasets"/>
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="dataset">The <see cref="Dataset"/></param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.Datasets"/> plu <paramref name="dataset"/>.</returns>
    public static Chart AddDataset(this Chart chart, Dataset dataset)
    {
        chart.Datasets.Add(dataset);
        return chart;
    }

    /// <summary>
    /// Add <see cref="Dataset"/> to <see cref="Chart.Datasets"/>
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="dataset">The collection of <see cref="Dataset"/></param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.Datasets"/> plu <paramref name="dataset"/>.</returns>
    public static Chart AddDatasets(this Chart chart, IEnumerable<Dataset> dataset)
    {
        chart.Datasets.AddRange(dataset);
        return chart;
    }

    /// <summary>
    /// Add <see cref="Dataset"/> to <see cref="Chart.Datasets"/>
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="dataset">The collection of <see cref="Dataset"/></param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.Datasets"/> plu <paramref name="dataset"/>.</returns>
    public static Chart AddDatasets(this Chart chart, params Dataset[] dataset)
    {
        chart.Datasets.AddRange(dataset);
        return chart;
    }

    /// <summary>
    /// Change <see cref="Chart.YAxis"/>.
    /// </summary>
    /// <param name="chart">The <see cref="Chart"/>.</param>
    /// <param name="hiddenLegend">The hidden legend <see cref="IConstraint"/>.</param>
    /// <returns>The <paramref name="chart"/> with <see cref="Chart.HiddenLegendConstraint"/> as <paramref name="hiddenLegend"/>.</returns>
    public static Chart SetHiddenLegendConstraint(this Chart chart, (IConstraint, IConstraint) hiddenLegend)
    {
        chart.HiddenLegendConstraint = hiddenLegend;
        return chart;
    }
}
