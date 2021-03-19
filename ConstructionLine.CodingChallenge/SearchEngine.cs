using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{

    /// <summary>
    /// Grouping shirts into sizes and colour subcategory
    /// </summary>
    struct SizeGroup
    {
        public Dictionary<Color, ColorSizeGroup> Colors { get; set; }

        public SizeGroup(Dictionary<Color, ColorSizeGroup> colors)
        {
            Colors = colors;
        }
    }

    /// <summary>
    /// Grouping shirts into colours
    /// </summary>
    struct ColorSizeGroup
    {
        public List<Shirt> Shirts { get; set; }

        public ColorSizeGroup(List<Shirt> shirts)
        {
            Shirts = shirts;
        }
    }

    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private List<Color> ShirtColors;
        private List<Size> ShirtSizes;
        private Dictionary<Size, SizeGroup> _shirtSizeGroup;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
            _shirtSizeGroup = new Dictionary<Size, SizeGroup>();

            GetDistinctGroups(shirts);
            InitShirtGroups();

            foreach (KeyValuePair<Size, SizeGroup> shirtSize in _shirtSizeGroup)
            {
                List<Shirt> shirtsAtSize = GetShirtsAtSize(shirts, shirtSize.Key);
                PopulateColorGroups(shirtSize.Value, shirtsAtSize);
            }
        }

        /// <summary>
        /// Getting disting shirt colors and sizes from the dataset
        /// </summary>
        /// <param name="shirts">Shirts dataset</param>
        private void GetDistinctGroups(List<Shirt> shirts)
        {
            ShirtColors = shirts.Select(shirt => shirt.Color)
                .Distinct()
                .ToList();

            ShirtSizes = shirts.Select(shirt => shirt.Size)
                .Distinct()
                .ToList();
        }


        /// <summary>
        /// Initialising Shirt Size groups and a separate Colour group for each Size
        /// </summary>
        private void InitShirtGroups()
        {
            foreach (Size shirtSize in ShirtSizes)
            {
                Dictionary<Color, ColorSizeGroup> colorsAtSize = new Dictionary<Color, ColorSizeGroup>();

                foreach (Color shirtColor in ShirtColors)
                {
                    colorsAtSize.Add(shirtColor, new ColorSizeGroup(new List<Shirt>()));
                }

                _shirtSizeGroup.Add(shirtSize, new SizeGroup(colorsAtSize));
            }
        }

        /// <summary>
        /// Getting all shirts at a specific size from a dataset
        /// </summary>
        /// <param name="shirts">Shirts dataset</param>
        /// <param name="shirtSize">size we are trying to get shirts for</param>
        /// <returns></returns>
        private static List<Shirt> GetShirtsAtSize(List<Shirt> shirts, Size shirtSize)
        {
            List<Shirt> shirtsAtSize = new List<Shirt>();
            foreach (Shirt shirt in shirts)
            {
                if (shirt.Size == shirtSize)
                    shirtsAtSize.Add(shirt);
            }

            return shirtsAtSize;
        }

        /// <summary>
        /// Populating colour groups within size groups with matching shirt objects
        /// </summary>
        /// <param name="shirtSizeGroup">Size group for which we want to populate colour groups with matching shirts</param>
        /// <param name="shirtsAtSize">All shirts at that specific size</param>
        private void PopulateColorGroups(SizeGroup shirtSizeGroup, List<Shirt> shirtsAtSize)
        {
            foreach (KeyValuePair<Color, ColorSizeGroup> colorSize in shirtSizeGroup.Colors)
            {
                foreach (Shirt shirt in shirtsAtSize)
                {
                    if (shirt.Color == colorSize.Key)
                        colorSize.Value.Shirts.Add(shirt);
                }
            }
        }

        /// <summary>
        /// Getting quantity of shirts having colors matching search options
        /// </summary>
        /// <param name="color">Colour object of the tshirt</param>
        /// <returns>Quantity of matchign tshirtss</returns>
        private int GetColorQuantity(Color color, SearchOptions options)
        {
            int quantity = 0;

            if (options.Colors.Contains(color))
            {
                foreach (SizeGroup size in _shirtSizeGroup.Values)
                {
                    quantity += size.Colors[color].Shirts.Count;
                }
            }

            return quantity;
        }

        /// <summary>
        /// Getting quantity of shirts having sizes matching search options combined with their object
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns>List of sizes with their quantities</returns>
        private List<SizeCount> GetShirtsOfSize(SearchOptions options)
        {
            List<SizeCount> shirtsOfSize = new List<SizeCount>();

            foreach (Size size in Size.All)
            {
                SizeCount sizeCount = new SizeCount();

                sizeCount.Size = size;
                foreach (Color color in options.Colors)
                    sizeCount.Count = _shirtSizeGroup[size].Colors[color].Shirts.Count;
                shirtsOfSize.Add(sizeCount);
            }

            return shirtsOfSize;
        }

        private List<ColorCount> GetShirtsOfColour(SearchOptions options)
        {
            List<ColorCount> shirtsOfColor = new List<ColorCount>();

            foreach (Color color in Color.All)
            {
                ColorCount colorCount = new ColorCount();

                colorCount.Color = color;
                colorCount.Count = GetColorQuantity(color, options);
                shirtsOfColor.Add(colorCount);
            }

            return shirtsOfColor;
        }



        public SearchResults Search(SearchOptions options)
        {
            options.Sizes = (options.Sizes.Count > 0) ? options.Sizes : Size.All;
            options.Colors = (options.Colors.Count > 0) ? options.Colors : Color.All;

            List <Shirt> shirtsFound = new List<Shirt>();
            List<SizeCount> shirtsOfSize = GetShirtsOfSize(options);
            List<ColorCount> shirtsOfColor = GetShirtsOfColour(options);

            foreach (Size size in options.Sizes)
            {
                foreach (Color color in options.Colors)
                {
                    shirtsFound.AddRange(_shirtSizeGroup[size].Colors[color].Shirts);
                }
            }

            return new SearchResults
            {
                Shirts = shirtsFound,
                SizeCounts = shirtsOfSize,
                ColorCounts = shirtsOfColor
            };
        }
    }
}