﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGen
{
    public static MeshData TerrainMeshGen(float[,] heightMap, float heightMultiplier, AnimationCurve meshHeightCurve){
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float TopLeftX = (width-1)/(-2f);
        float TopLeftZ = (height-1)/(2f);


        MeshData meshdata = new MeshData(width, height);
        int vertexIndex = 0;

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                
                meshdata.vertices[vertexIndex] = new Vector3(TopLeftX + x, 
                    meshHeightCurve.Evaluate(heightMap[x, y]) * heightMultiplier,TopLeftZ - y);
                meshdata.uvs[vertexIndex] = new Vector2(x/(float)width, y/(float)height);
                if(x < width - 1 && y < height -1){
                    meshdata.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshdata.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex+=1;
            }
        }

        return meshdata;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    int triangleIndex;
    public Vector2[] uvs;
    public MeshData(int meshWidth, int meshHeight){
        vertices = new Vector3[meshWidth*meshHeight];
        uvs = new Vector2[meshHeight* meshWidth];
        triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
    }

    public void AddTriangle(int a, int b, int c){
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;

        triangleIndex += 3;
    }

    public Mesh CreateMesh(){
        Mesh mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        return mesh;
    }
}
