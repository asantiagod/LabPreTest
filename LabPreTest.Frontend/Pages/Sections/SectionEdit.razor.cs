using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Authorization;

namespace LabPreTest.Frontend.Pages.Sections
{
    [Authorize(Roles = FrontendStrings.AdminString)]
    public partial class SectionEdit
    {
    }
}
