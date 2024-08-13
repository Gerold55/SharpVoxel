using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MyMinecraftClone
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicEffect _effect;
        VertexBuffer _vertexBuffer;
        IndexBuffer _indexBuffer;

        Matrix _worldMatrix;
        Matrix _viewMatrix;
        Matrix _projectionMatrix;

        float _rotationX;
        float _rotationY;

        int gridSize = 10; // Size of the grid
        float voxelSize = 1.0f; // Size of each voxel

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _worldMatrix = Matrix.CreateRotationX(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateTranslation(0f, 0f, 0f);
            _viewMatrix = Matrix.CreateLookAt(new Vector3(15f, 15f, 15f), Vector3.Zero, Vector3.Up);
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _effect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                TextureEnabled = false
            };

            var vertices = new List<VertexPositionColor>();
            var indices = new List<ushort>();

            int vertexOffset = 0;

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    for (int z = 0; z < gridSize; z++)
                    {
                        var color = new Color((x * 255 / gridSize), (y * 255 / gridSize), (z * 255 / gridSize));

                        // Add vertices for each face
                        AddVoxelVertices(vertices, x * voxelSize, y * voxelSize, z * voxelSize, voxelSize, color);

                        // Add indices for each face
                        AddVoxelIndices(indices, vertexOffset);

                        vertexOffset += 24; // 6 faces * 4 vertices per face
                    }
                }
            }

            _vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Count, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(vertices.ToArray());

            _indexBuffer = new IndexBuffer(GraphicsDevice, typeof(ushort), indices.Count, BufferUsage.WriteOnly);
            _indexBuffer.SetData(indices.ToArray());
        }

        private void AddVoxelVertices(List<VertexPositionColor> vertices, float x, float y, float z, float size, Color color)
        {
            float halfSize = size / 2;

            vertices.AddRange(new[]
            {
                // Front face
                new VertexPositionColor(new Vector3(x - halfSize, y - halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y - halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y + halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y + halfSize, z + halfSize), color),

                // Right face
                new VertexPositionColor(new Vector3(x + halfSize, y - halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y - halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y + halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y + halfSize, z + halfSize), color),

                // Back face
                new VertexPositionColor(new Vector3(x + halfSize, y - halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y - halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y + halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y + halfSize, z - halfSize), color),

                // Left face
                new VertexPositionColor(new Vector3(x - halfSize, y - halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y - halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y + halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y + halfSize, z - halfSize), color),

                // Top face
                new VertexPositionColor(new Vector3(x - halfSize, y + halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y + halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y + halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y + halfSize, z + halfSize), color),

                // Bottom face
                new VertexPositionColor(new Vector3(x - halfSize, y - halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y - halfSize, z + halfSize), color),
                new VertexPositionColor(new Vector3(x + halfSize, y - halfSize, z - halfSize), color),
                new VertexPositionColor(new Vector3(x - halfSize, y - halfSize, z - halfSize), color),
            });
        }

        private void AddVoxelIndices(List<ushort> indices, int offset)
        {
            ushort[] voxelIndices =
            {
                // Front face
                0, 1, 2, 2, 3, 0,

                // Right face
                4, 5, 6, 6, 7, 4,

                // Back face
                8, 9, 10, 10, 11, 8,

                // Left face
                12, 13, 14, 14, 15, 12,

                // Top face
                16, 17, 18, 18, 19, 16,

                // Bottom face
                20, 21, 22, 22, 23, 20
            };

            foreach (var index in voxelIndices)
            {
                indices.Add((ushort)(offset + index));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _rotationX += deltaTime;
            _rotationY += deltaTime;

            // Update the world matrix to include rotation
            _worldMatrix = Matrix.CreateRotationX(_rotationX) * Matrix.CreateRotationY(_rotationY) * Matrix.CreateTranslation(0f, 0f, 0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _effect.World = _worldMatrix;
            _effect.View = _viewMatrix;
            _effect.Projection = _projectionMatrix;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.SetVertexBuffer(_vertexBuffer);
                GraphicsDevice.Indices = _indexBuffer;

                // Use DrawIndexedPrimitives
                GraphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    _indexBuffer.IndexCount / 3
                );
            }

            base.Draw(gameTime);
        }
    }
}
