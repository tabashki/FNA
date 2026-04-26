#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System;
using System.Threading;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
	public class DepthTexture : Texture
	{
		#region Public Properties

		public int Width
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		#endregion

		#region Internal Constructor, Dispose Method

		internal DepthTexture(
			GraphicsDevice graphicsDevice,
			int width,
			int height,
			bool mipMap,
			DepthFormat depthFormat,
			IntPtr renderBufferHandle
		) {
			GraphicsDevice = graphicsDevice;
			Width = width;
			Height = height;
			LevelCount = mipMap ? CalculateMipLevels(width, height) : 1;
			Format = (depthFormat == DepthFormat.Depth16 ?
				SurfaceFormat.UShortEXT : SurfaceFormat.Single);

			// Get a handle to the underlying Depth Buffer resource as a texture,
			// IMPORTANT: This handle is owned by RenderTarget2D so this class *mustn't* dispose of it
			texture = FNA3D.FNA3D_GetRenderbufferDepthTexture(
				graphicsDevice.GLDevice,
				renderBufferHandle
			);
		}

		protected override void Dispose(bool disposing)
		{
			// Forcibly null handle here to stop Texture.Dispose from destroying the non-owned texture
			Interlocked.Exchange(ref texture, IntPtr.Zero);
			base.Dispose(disposing);
		}

		#endregion
	}
}

