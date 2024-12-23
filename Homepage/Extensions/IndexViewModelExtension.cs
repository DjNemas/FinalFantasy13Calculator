using Homepage.Models.Home;

namespace Homepage.Extensions
{
    internal static class IndexViewModelExtension
    {
        public static void  FillNavigationContent(this IndexViewModel viewModel)
        {
            viewModel.NavigationContent.AddRange(new IndexNavigationContent
            {
                Title = "Final Fantasy XIII Calculator",
                Description = "Here you can Calculate Item Exp for Modification ingame.",
                ImageUrl = "/res/image/home/FFXIII-IndexImage.png",
                ImageAltText = "Final Fantasy XIII Preview Image",
                LinkPath = "/FFXIII-Calculator"
            },
            new IndexNavigationContent
            {
                Title = "Final Fantasy XIII-2 Calculator",
                Description = "Available in Future",
                ImageUrl = "/res/image/home/FFXIII-2-IndexImage.png",
                ImageAltText = "Final Fantasy XIII-2 Preview Image",
                LinkPath = "#", //"/FFXIII-2-Calculator"
                DisableHover = true
            },
            new IndexNavigationContent
            {
                Title = "Final Fantasy XIII Lightning Returns Calculator",
                Description = "Available in Future",
                ImageUrl = "/res/image/home/FFXIII-LR-IndexImage.png",
                ImageAltText = "Final Fantasy XIII Lightning Return Preview Image",
                LinkPath = "#", //"/FFXIII-LR-Calculator"
                DisableHover = true
            });
        }
    }
}
