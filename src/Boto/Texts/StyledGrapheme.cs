using Boto.Styles;

namespace Boto.Texts;

/// <summary>
/// A grapheme associated to a style.
/// </summary>
public record StyledGrapheme(string Symbol, Style Style);
