# Boto

[//]: # (<img src="./assets/demo.gif" alt="Demo cast under Linux Termite with Inconsolata font 12pt">)

`Boto` is a [C#](https://learn.microsoft.com/en-us/dotnet/csharp/) library to build rich terminal
user interfaces and dashboards. It is heavily inspired by the `Javascript`
library [blessed-contrib](https://github.com/yaronn/blessed-contrib), the
`Go` library [termui](https://github.com/gizak/termui) and the `Rust` library [tui](https://github.com/fdehau/tui-rs) .

The library supports multiple backends:
  - [Tutu](https://github.com/lillo42/tutu/) 

The library is based on the principle of immediate rendering with intermediate
buffers. This means that at each new frame you should build all widgets that are
supposed to be part of the UI. While providing a great flexibility for rich and
interactive UI, this may introduce overhead for highly dynamic content. So, the
implementation try to minimize the number of ansi escapes sequences generated to
draw the updated UI.

Moreover, the library does not provide any input handling nor any event system and
you may rely on the previously cited libraries to achieve such features.

### Demo

The demo shown in the gif can be run with all available backends.

```bash
# tutu
dotnet run --project ./sample/Demo --release -- --tick-rate 200
```

where `tick-rate` is the UI refresh rate in ms.

The UI code is in [samples/demo/ui.rs](https://github.com/fdehau/tui-rs/blob/v0.19.0/examples/demo/ui.rs) while the
application state is in [examples/demo/app.rs](https://github.com/fdehau/tui-rs/blob/v0.19.0/examples/demo/app.rs).

If the user interface contains glyphs that are not displayed correctly by your terminal, you may want to run
the demo without those symbols:

### Widgets

The library comes with the following list of widgets:

  * [Block](./samples/BlockSample)
  * [Gauge](./samples/GaugeSample)
  * [Sparkline](./samples/SparklineSample)
  * [Chart](./samples/ChartSample)
  * [BarChart](./samples/BarchartSample)
  * [List](./samples/ListSample)
  * [Table](./samples/TableSample)
  * [Paragraph](./samples/ParagraphSample)
  * [Canvas (with line, point cloud, map)](./samples/CanvasSample)
  * [Tabs](./samples/TabSample)

Click on each item to see the source of the example. Run the examples with with 
cargo (e.g. to run the gauge example `dotnet run --project ./samples/GaugeSample`), and quit by pressing `q`.

## License

[MIT](LICENSE)
