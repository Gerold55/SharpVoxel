using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyMinecraftClone
{
    public class Chunk
    {
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;

        public Chunk(GraphicsDevice graphicsDevice)
        {
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<ushort> indices = new List<ushort>();

            int vertexOffset = 0;

            // Example: Create a 1x1x1 chunk with 1 block
            AddBlock(vertices, indices, vertexOffset);

            _vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), vertices.Count, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(vertices.ToArray());

            _indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), indices.Count, BufferUsage.WriteOnly);
            _indexBuffer.SetData(indices.ToArray());
        }

        private void AddBlock(List<VertexPositionColor> vertices, List<ushort> indices, int vertexOffset)
        {
            vertices.AddRange(Block.Vertices);
            foreach (var index in Block.Indices)
            {
                indices.Add((ushort)(vertexOffset + index));
            }
        }

        public void Render(GraphicsDevice graphicsDevice, BasicEffect effect)
        {
            graphicsDevice.SetVertexBuffer(_vertexBuffer);
            graphicsDevice.Indices = _indexBuffer;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    _indexBuffer.IndexCount / 3
                );
            }
        }
    }
}
