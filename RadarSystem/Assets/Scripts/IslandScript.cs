using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    DTL.Shape.PerlinIsland elevation;
    DTL.Shape.PerlinIsland temperature;
    DTL.Shape.PerlinIsland rainfall;

    [MinMaxRange(-20f, 70f)]
    public MinMax mm_temperature = new MinMax(10, 30);

    [MinMaxRange(0f, 4500)]
    public MinMax mm_rainfall = new MinMax(0, 4500);

    [MinMaxRange(-5000, 8000)]
    public MinMax mm_elevation = new MinMax(0, 500);

    int[,] mat_elevation;
    int[,] mat_temperature;
    int[,] mat_rainfall;
    int[,] mat_biome;

    Color32[] biome_color =
    {
        new Color32(41, 40, 159, 255),      // sea
        new Color32(218, 217, 225, 255),    // tundra
        new Color32(223, 203, 140, 255),    // desert
        new Color32(188, 205, 146, 255),    // step
        new Color32(164, 143, 50, 255),     // savannah
        new Color32(97, 154, 96, 255),      // conifer
        new Color32(101, 163, 56, 255),     // temperate deciduous forest
        new Color32(9, 100, 5, 255),        // laurel forest
        new Color32(43, 84, 41, 255),       // rain green forest
        new Color32(43, 84, 41, 255),       // subtropical rainforest
        new Color32(43, 84, 41, 255)        // tropical rainforest
    };

    public int img_width;
    public int img_height;

    Texture2D image;

    int[,] AllocateMatrix()
    {
        return new int[img_height, img_width];
    }

    void SetupBiome()
    {
        for (int y = 0; y < img_height; ++y)
        {
            for (int x = 0; x < img_width; ++x)
            {
                if (mat_elevation[y, x] < 90) mat_biome[y, x] = 0;          // sea
                else if (mat_temperature[y, x] < -5) mat_biome[y, x] = 1;   // tundra
                else if (mat_rainfall[y, x] < 500) mat_biome[y, x] = 2;     // desert
                else if (mat_rainfall[y, x] < 1500)
                {
                    if (mat_temperature[y, x] < 20) mat_biome[y, x] = 3;    // step
                    else mat_biome[y, x] = 4;   // savannah
                }
                else if (mat_temperature[y, x] < 3) mat_biome[y, x] = 5;    // conifer
                else if (mat_temperature[y, x] < 12) mat_biome[y, x] = 6;   // temperate deciduous forest
                else if (mat_temperature[y, x] < 20) mat_biome[y, x] = 7;   // laurel forest
                else if (mat_rainfall[y, x] < 2500) mat_biome[y, x] = 8;    // rain green forest
                else if (mat_temperature[y, x] < 24) mat_biome[y, x] = 9;   // subtropical rainforest
                else mat_biome[y, x] = 10;  // tropical rainforest
            }
        }
    }

    void SetupImage()
    {
        for (int y = 0; y < img_height; ++y)
        {
            for (int x = 0; x < img_width; ++x)
            {
                Color c = biome_color[mat_biome[y, x]];
                image.SetPixel(x, y, c);
            }
        }
    }

    void InitializeParameters()
    {
        elevation = new DTL.Shape.PerlinIsland(8.0, 8, (int)mm_elevation.max, (int)mm_elevation.min);
        temperature = new DTL.Shape.PerlinIsland(8.0, 8, (int)mm_temperature.max, (int)mm_temperature.min);
        rainfall = new DTL.Shape.PerlinIsland(8.0, 8, (int)mm_rainfall.max, (int)mm_rainfall.min);

        mat_elevation = AllocateMatrix();
        mat_rainfall = AllocateMatrix();
        mat_temperature = AllocateMatrix();
        mat_biome = AllocateMatrix();

        image = new Texture2D(img_width, img_height);

        elevation.Draw(mat_elevation);
        temperature.Draw(mat_temperature);
        rainfall.Draw(mat_rainfall);

        SetupBiome();
        SetupImage();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeParameters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
