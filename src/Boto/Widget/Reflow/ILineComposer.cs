using Boto.Texts;

namespace Boto.Widget.Reflow;

internal interface ILineComposer
{
    (List<StyledGrapheme>, int)? NextLine();
}
