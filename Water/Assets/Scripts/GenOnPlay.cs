using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenOnPlay : MonoBehaviour
{
    public struct TerrainType{
    public string name;
    public float height;
    public Color color; }
    // Start is called before the first frame update
    void Start()
    {   
        int mapWidth = 100;
        int mapHeight = 100;
        
        MapDisplay display = FindObjectOfType<MapDisplay>();
        MapGen map = FindObjectOfType<MapGen>();

        Vector2 offset = new Vector2(0, 0);
        int seed = Random.Range(-1000, 1000);
        float[,] noiseMap = Noise.GenerateNoiseMap(100, 100, map.noiseScale, 3, 0.2f, 3, seed, offset);

        Color[] colorMap = new Color[mapWidth*mapHeight];
        for(int y = 0; y < mapHeight; y++){
            for(int x = 0; x < mapWidth; x++){
                float currentHeight = noiseMap[x, y];
                for(int i = 0 ; i < map.regions.Length; i++){
                    if(currentHeight < map.regions[i].height){
                        colorMap[(y*mapWidth) + x] = map.regions[i].color;
                        break;
                    }
                }
            }
        }
        

        display.DrawMesh(MeshGen.TerrainMeshGen(noiseMap, map.heightMultiplier, map.meshHeightCurve), 
            ColorTexture.TextureColorMap(colorMap, mapWidth, mapHeight));

    }

    // Update is called once per frame
    void Update()
    {
        return;
    }
}
