Got following results for the performance test when just searched for red shirts. I would assume that based on requirements this is the correct result. First number is quantity of shirts returned. Then quantities of matched sizes based on size/colour condition, then quantities of matched colours based on the same condition. Assert fails saying that blue shirts quantity should also be given.

![image](https://user-images.githubusercontent.com/24981173/111824037-95f5dc80-88dd-11eb-8b1b-68337ecebb71.png)

# My solution and thoughts

This is my attempt for this search engine. I believe code could be better organised, so if I had an opportunity to spend more time on it (was quite busy today) I would ensure quality would be better.

One observation is that for some reason performance test fails due to expecting quantity of blue shirts. Although in the performance test blue shirts are not searched for. This was confusing me a bit and caused me to spend some more time on the challenge. I did test it with quantity of all shirts being given (even the ones that were not searched), performance test succedded then. However the normal simple test failed because it expected quantity to be 0 for shirts that were not searched. 

I left the program to not include quantities of shirts that weren't searched as this was the requirement spec. Getting rid of "if (options.Colors.Contains(color))" in the search engine class ensures that quantities of all shirts are provided, even ones that were not searched.

# Construction Line code challenge

The code challenge consists in the implementation of a simple search engine for shirts.

## What to do?
Shirts are in different sizes and colors. As described in the Size.cs class, there are three sizes: small, medium and large, and five different colors listed in Color.cs class.

The search specifies a range of sizes and colors in SearchOptions.cs. For example, for small, medium and red the search engine should return shirts that are either small or medium in size and are red in color. In this case, the SearchOptions should look like:

```
{
    Sizes = List<Size> {Size.Small, Size.Medium},
    Colors = List<Color> {Color.Red}
}
```

The results should include, as well as the shirts matching the search options, the total count for each search option taking into account the options that have been selected. For example, if there are two shirts, one small and red and another medium and blue, if the search options are small size and red color, the results (captured in SearchResults.cs) with total count for each option should be:
```
{
    Shirts = List<Shirt> { SmallRedShirt },
    SizeCounts = List<SizeCount> { Small(1), Medium(0), Large(0)},
    ColorCounts = List<ColorCount> { Red(1), Blue(0), Yellow(0), White(0), Black(0)}
}
```

The search engine logic sits in SearchEngine.cs and should be implemented by the candidate. Feel free to use any additional data structures, classes or libraries to prepare the data before the actual search. The initalisation of these should sit in the constructor of the search engine.

There are two tests in the test project; one simple search for red shirts out of a total of three, and another one which tests the performance of the search algorithm through 50.000 random shirts of all sizes and colors which measures how long it takes to perform the search algorithm. A reasonable implementation should not take more than 100 ms to return the results.

## Procedure
We would like you to send us a link to a git repository that we can access with your implementation.

The whole exercise should not take more than an hour to implement.
