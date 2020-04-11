using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{   
    // Creating the generator function
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale,
     int octaves, float persistence, float lacunarity, int seed, Vector2 offset){
        // Creating a 2-d Array called noise map
        float[,] noiseMap = new float[mapWidth, mapHeight];
        float halfWidth = mapWidth/2f;
        float halfHeight = mapHeight/2f;
        
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for(int i = 0; i < octaves; i++){
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // To avoid division - by - zero error
        if(scale <= 0){
            scale = 0.0001f;
        }

        float MaxNoise = float.MinValue;
        float MinNoise = float.MaxValue;
        // Iterating through the indices, calling Perlin Noise function for each index
        for(int y = 0; y < mapHeight; y++){
            for(int x = 0; x < mapWidth; x++){
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for(int i = 0; i < octaves; i++){
                    float SampleX = (x- halfWidth)/scale * frequency + octaveOffsets[i].x;
                    float SampleY = (y - halfHeight)/scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(SampleX, SampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                if(MaxNoise <= noiseHeight){
                    MaxNoise = noiseHeight;
                }
                if(MinNoise >= noiseHeight){
                    MinNoise = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }
        // For normalizing between 0 , 1
        for(int y = 0; y < mapHeight; y++){
            for(int x = 0; x < mapWidth; x++){
                noiseMap[x, y] = Mathf.InverseLerp(MinNoise, MaxNoise, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
