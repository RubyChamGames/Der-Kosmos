using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public int heightMultiplier;
    public AnimationCurve meshHeightCurve;
    public DrawMode drawMode;
    public int mapHeight;
    public int mapWidth;
    public float noiseScale;
    public float lacunarity;
    public float persistence;
    public int octaves;
    public int seed;
    public Vector2 offset;
    public bool autoupdate;
    
    public TerrainType[] regions;

    public void GenerateMap(){
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, 
        persistence, lacunarity, seed, offset);

        Color[] colorMap = new Color[mapWidth*mapHeight];
        for(int y = 0; y < mapHeight; y++){
            for(int x = 0; x < mapWidth; x++){
                float currentHeight = noiseMap[x, y];
                for(int i = 0 ; i < regions.Length; i++){
                    if(currentHeight < regions[i].height){
                        colorMap[(y*mapWidth) + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap){
            display.DrawTexture(ColorTexture.TextureHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap){
            display.DrawTexture(ColorTexture.TextureColorMap(colorMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.Mesh){
            display.DrawMesh(MeshGen.TerrainMeshGen(noiseMap, heightMultiplier, meshHeightCurve), 
            ColorTexture.TextureColorMap(colorMap, mapWidth, mapHeight));
        }
    }
}

[System.Serializable]
public struct TerrainType{
    public string name;
    public float height;
    public Color color;
}