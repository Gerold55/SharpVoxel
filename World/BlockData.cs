using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{
    public enum Faces
    {
        FRONT,
        BACK,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    public struct FaceData
    {
        public List<Vector3> Vertices;
        public List<Vector2> UV;
    }

    public static class FaceDataRaw
    {
        public static readonly Dictionary<Faces, List<Vector3>> RawVertexData = new Dictionary<Faces, List<Vector3>>
        {
            { Faces.FRONT, new List<Vector3>
            {
                new Vector3(-0.5f, 0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, 0.5f, 0.5f), // topright vert
                new Vector3(0.5f, -0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, 0.5f), // bottomleft vert
            }},
            { Faces.BACK, new List<Vector3>
            {
                new Vector3(0.5f, 0.5f, -0.5f), // topleft vert
                new Vector3(-0.5f, 0.5f, -0.5f), // topright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
            }},
            { Faces.LEFT, new List<Vector3>
            {
                new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
                new Vector3(-0.5f, 0.5f, 0.5f), // topright vert
                new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            }},
            { Faces.RIGHT, new List<Vector3>
            {
                new Vector3(0.5f, 0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, 0.5f, -0.5f), // topright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
            }},
            { Faces.TOP, new List<Vector3>
            {
                new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
                new Vector3(0.5f, 0.5f, -0.5f), // topright vert
                new Vector3(0.5f, 0.5f, 0.5f), // bottomright vert
                new Vector3(-0.5f, 0.5f, 0.5f), // bottomleft vert
            }},
            { Faces.BOTTOM, new List<Vector3>
            {
                new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
                new Vector3(0.5f, -0.5f, 0.5f), // topright vert
                new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
                new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            }},
        };
    }
}
