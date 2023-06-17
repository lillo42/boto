using Boto.Texts;

namespace Boto.Widgets.Reflow;

internal interface ILineComposer
{
    (List<StyledGrapheme>, int)? NextLine();
}
