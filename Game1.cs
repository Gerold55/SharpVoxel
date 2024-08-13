using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyMinecraftClone
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicEffect _effect;
        Chunk _chunk;

        Matrix _worldMatrix;
        Matrix _viewMatrix;
        Matrix _projectionMatrix;

        float _cameraDistance = 15f; // Initial camera distance
        float _zoomSpeed = 5f; // Speed at which zooming occurs
        float _rotationSpeed = 0.5f; // Speed of rotation
        float _rotationX = 0f; // Rotation around X-axis
        float _rotationY = 0f; // Rotation around Y-axis

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _worldMatrix = Matrix.CreateRotationX(_rotationX) * Matrix.CreateRotationY(_rotationY) * Matrix.CreateTranslation(0f, 0f, 0f);
            _viewMatrix = Matrix.CreateLookAt(new Vector3(_cameraDistance, _cameraDistance, _cameraDistance), Vector3.Zero, Vector3.Up);
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _effect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                TextureEnabled = false
            };

            _chunk = new Chunk(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Handle zoom in and out
            if (keyboardState.IsKeyDown(Keys.W)) // Zoom in
                _cameraDistance -= _zoomSpeed * deltaTime;

            if (keyboardState.IsKeyDown(Keys.S)) // Zoom out
                _cameraDistance += _zoomSpeed * deltaTime;

            // Ensure the camera distance does not become negative or too small
            _cameraDistance = MathHelper.Clamp(_cameraDistance, 1f, 50f);

            // Handle rotation
            if (keyboardState.IsKeyDown(Keys.A)) // Rotate left
                _rotationY -= _rotationSpeed * deltaTime;

            if (keyboardState.IsKeyDown(Keys.D)) // Rotate right
                _rotationY += _rotationSpeed * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Q)) // Rotate up
                _rotationX -= _rotationSpeed * deltaTime;

            if (keyboardState.IsKeyDown(Keys.E)) // Rotate down
                _rotationX += _rotationSpeed * deltaTime;

            // Update view matrix
            _viewMatrix = Matrix.CreateLookAt(new Vector3(_cameraDistance, _cameraDistance, _cameraDistance), Vector3.Zero, Vector3.Up);

            // Update world matrix with rotation
            _worldMatrix = Matrix.CreateRotationX(_rotationX) * Matrix.CreateRotationY(_rotationY) * Matrix.CreateTranslation(0f, 0f, 0f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _effect.World = _worldMatrix;
            _effect.View = _viewMatrix;
            _effect.Projection = _projectionMatrix;

            _chunk.Render(GraphicsDevice, _effect);

            base.Draw(gameTime);
        }
    }
}
