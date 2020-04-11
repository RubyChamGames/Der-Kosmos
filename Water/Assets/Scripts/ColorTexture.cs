using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorTexture
{
    public static Texture2D TextureColorMap(Color[] colorMap, int width, int height){
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureHeightMap(float[,] noiseMap){
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Color[] colorMap = new Color[height*width];
        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                colorMap[(y * width) + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        return TextureColorMap(colorMap, width, height);
    }
}
