using UnityEngine;

//-----------------------------------------------------------------------------
// Copyright 2015-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProVideo
{
	[System.Serializable]
	public class MediaPlayerEvent : UnityEngine.Events.UnityEvent<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>
	{
		public enum EventType
		{
			MetaDataReady,		// Called when meta data(width, duration etc) is available
			ReadyToPlay,		// Called when the video is loaded and ready to play
			Started,			// Called when the playback starts
			FirstFrameReady,	// Called when the first frame has been rendered
			FinishedPlaying,	// Called when a non-looping video has finished playing
			Closing,			// Called when the media is closed
			Error,				// Called when an error occurs

			// TODO: 
			//FinishedSeeking,	// Called when seeking has finished
			//StartLoop,			// Called when the video starts and is in loop mode
			//EndLoop,			// Called when the video ends and is in loop mode
			//Error,				// If there is an error with the playback, details provided on the error
		}
	}

	public interface IMediaPlayer
	{
		void Update();
		void Render();
	}

	public interface IMediaControl
	{
		// TODO: CanPreRoll() PreRoll()
		// TODO: audio panning

		bool	OpenVideoFromFile( string path );

        void    CloseVideo();

        void	SetLooping( bool bLooping );
		bool	IsLooping();

		bool	HasMetaData();
		bool	CanPlay();
		bool	IsPlaying();
		bool	IsSeeking();
		bool	IsPaused();
		bool	IsFinished();
		bool	IsBuffering();

		void	Play();
		void	Pause();
		void	Stop();
		void	Rewind();

		void	Seek(float timeMs);
		void	SeekFast(float timeMs);
		float	GetCurrentTimeMs();

		float	GetPlaybackRate();
		void	SetPlaybackRate(float rate);

		void	MuteAudio(bool bMute);
		bool	IsMuted();
		void	SetVolume(float volume);
		float	GetVolume();

		int		GetCurrentAudioTrack();
		void	SetAudioTrack(int index);

		float	GetBufferingProgress();

		ErrorCode GetLastError();

		void	SetTextureProperties(FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, int anisoLevel = 1);
	}

	public interface IMediaInfo
	{
		/// <summary>
		/// Returns media duration in milliseconds
		/// </summary>
		float	GetDurationMs();

		/// <summary>
		/// Returns video width in pixels
		/// </summary>
		int		GetVideoWidth();

		/// <summary>
		/// Returns video height in pixels
		/// </summary>
		int		GetVideoHeight();

		/// <summary>
		/// Returns the frame rate of the media.
		/// </summary>
		float GetVideoFrameRate();

		/// <summary>
		/// Returns the current achieved display rate in frames per second
		/// </summary>
		float	GetVideoDisplayRate();

		/// <summary>
		/// Returns true if the media has a visual track
		/// </summary>
		bool	HasVideo();

		/// <summary>
		/// Returns true if the media has a audio track
		/// </summary>
		bool	HasAudio();

		/// <summary>
		/// Returns the number of audio tracks contained in the media
		/// </summary>
		int GetAudioTrackCount();

		/*
		string GetMediaDescription();
		string GetVideoDescription();
		string GetAudioDescription();*/
		}

	public interface IMediaProducer
	{
		/// <summary>
		/// Returns the Unity texture containing the current frame image
		/// </summary>
		Texture	GetTexture();

		/// <summary>
		/// Returns a count of how many times the texture has been updated
		/// </summary>
		int		GetTextureFrameCount();

		/// <summary>
		/// Returns true if the image on the texture is upside-down
		/// </summary>
		bool	RequiresVerticalFlip();
	}

	// TODO: complete this?
	public interface IMediaConsumer
	{
	}

	public enum Platform
	{
		Windows,
		MacOSX,
		iOS,
		tvOS,
		Android,
		WindowsPhone,
		WindowsUWP,
		WebGL,
		Count = 8,
		Unknown = 100,
	}

	public enum StereoPacking
	{
		None,
		TopBottom,
		LeftRight,
	}

	public enum AlphaPacking
	{
		None,
		TopBottom,
		LeftRight,
	}

	public enum ErrorCode
	{
		None = 0,
		LoadFailed = 100,
	}

	public static class Helper
	{
		public const string ScriptVersion = "1.4.4";

		public static string GetName(Platform platform)
		{
			string result = "Unknown";
			/*switch (platform)
			{
				case Platform.Windows:
					break;
			}*/
			result = platform.ToString();
			return result;
		}

		public static string[] GetPlatformNames()
		{
			return new string[] { 
				GetName(Platform.Windows), 
				GetName(Platform.MacOSX),
				GetName(Platform.iOS),
				GetName(Platform.tvOS),
				GetName(Platform.Android),
				GetName(Platform.WindowsPhone),
				GetName(Platform.WindowsUWP),
				GetName(Platform.WebGL),
			};
		}

		public static string GetTimeString(float totalSeconds)
		{
			int hours = Mathf.FloorToInt(totalSeconds / (60f * 60f));
			float usedSeconds = hours * 60f * 60f;

			int minutes = Mathf.FloorToInt((totalSeconds - usedSeconds) / 60f);
			usedSeconds += minutes * 60f;

			int seconds = Mathf.RoundToInt(totalSeconds - usedSeconds);

			string result;
			if (hours <= 0)
			{
				result = string.Format("{0:00}:{1:00}", minutes, seconds);
			}
			else
			{
				result = string.Format("{2}:{0:00}:{1:00}", minutes, seconds, hours);
			}

			return result;
		}

		public static void SetupStereoMaterial(Material material, StereoPacking packing, bool displayDebugTinting)
		{
			material.DisableKeyword("STEREO_TOP_BOTTOM");
			material.DisableKeyword("STEREO_LEFT_RIGHT");
			material.DisableKeyword("MONOSCOPIC");

			// Enable the required mode
			switch (packing)
			{
				case StereoPacking.None:
					break;
				case StereoPacking.TopBottom:
					material.EnableKeyword("STEREO_TOP_BOTTOM");
					break;
				case StereoPacking.LeftRight:
					material.EnableKeyword("STEREO_LEFT_RIGHT");
					break;
			}

			material.DisableKeyword("STEREO_DEBUG_OFF");
			material.DisableKeyword("STEREO_DEBUG");

			if (displayDebugTinting)
			{
				material.EnableKeyword("STEREO_DEBUG");
			}
		}

		public static void SetupAlphaPackedMaterial(Material material, AlphaPacking packing)
		{
			material.DisableKeyword("ALPHAPACK_TOP_BOTTOM");
			material.DisableKeyword("ALPHAPACK_LEFT_RIGHT");
			material.DisableKeyword("ALPHAPACK_NONE");

			// Enable the required mode
			switch (packing)
			{
				case AlphaPacking.None:
					break;
				case AlphaPacking.TopBottom:
					material.EnableKeyword("ALPHAPACK_TOP_BOTTOM");
					break;
				case AlphaPacking.LeftRight:
					material.EnableKeyword("ALPHAPACK_LEFT_RIGHT");
					break;
			}
		}

		public static void DrawTexture(Rect screenRect, Texture texture, ScaleMode scaleMode, AlphaPacking alphaPacking, Material material)
		{
			if (Event.current.type == EventType.Repaint)
			{
				float textureWidth = texture.width;
				float textureHeight = texture.height;
				switch (alphaPacking)
				{
					case AlphaPacking.LeftRight:
						textureWidth *= 0.5f;
						break;
					case AlphaPacking.TopBottom:
						textureHeight *= 0.5f;
						break;
				}

				float aspectRatio = (float)textureWidth / (float)textureHeight;
				Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
				switch (scaleMode)
				{
					case ScaleMode.ScaleAndCrop:
						{
							float screenRatio = screenRect.width / screenRect.height;
							if (screenRatio > aspectRatio)
							{
								float adjust = aspectRatio / screenRatio;
								sourceRect = new Rect(0f, (1f - adjust) * 0.5f, 1f, adjust);
							}
							else
							{
								float adjust = screenRatio / aspectRatio;
								sourceRect = new Rect(0.5f - adjust * 0.5f, 0f, adjust, 1f);
							}
						}
						break;
					case ScaleMode.ScaleToFit:
						{
							float screenRatio = screenRect.width / screenRect.height;
							if (screenRatio > aspectRatio)
							{
								float adjust = aspectRatio / screenRatio;
								screenRect = new Rect(screenRect.xMin + screenRect.width * (1f - adjust) * 0.5f, screenRect.yMin, adjust * screenRect.width, screenRect.height);
							}
							else
							{
								float adjust = screenRatio / aspectRatio;
								screenRect = new Rect(screenRect.xMin, screenRect.yMin + screenRect.height * (1f - adjust) * 0.5f, screenRect.width, adjust * screenRect.height);
							}
						}
						break;
					case ScaleMode.StretchToFill:
						break;
				}
				Graphics.DrawTexture(screenRect, texture, sourceRect, 0, 0, 0, 0, GUI.color, material);
			}
		}

		// Converts a non-readable texture to a readable Texture2D.
		// "targetTexture" can be null or you can pass in an existing texture.
		// Remember to Destroy() the returned texture after finished with it
		public static Texture2D GetReadableTexture(Texture inputTexture, bool requiresVerticalFlip, Texture2D targetTexture)
		{
			Texture2D resultTexture = targetTexture;

			RenderTexture prevRT = RenderTexture.active;

			// Blit the texture to a temporary RenderTexture
			// This handles any format conversion that is required and allows us to use ReadPixels to copy texture from RT to readable texture
			RenderTexture tempRT = RenderTexture.GetTemporary(inputTexture.width, inputTexture.height, 0, RenderTextureFormat.ARGB32);

			if (!requiresVerticalFlip)
			{
				Graphics.Blit(inputTexture, tempRT);
			}
			else
			{
				// The above Blit can't flip unless using a material, so we use Graphics.DrawTexture instead
				GL.PushMatrix();
				RenderTexture.active = tempRT;
				GL.LoadPixelMatrix(0f, tempRT.width, 0f, tempRT.height);
				Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
				// NOTE: not sure why we need to set y to -1, without this there is a 1px gap at the bottom
				Rect destRect = new Rect(0f, -1f, tempRT.width, tempRT.height);

				Graphics.DrawTexture(destRect, inputTexture, sourceRect, 0, 0, 0, 0);
				GL.PopMatrix();
				GL.InvalidateState();
			}

			if (resultTexture == null)
			{
				resultTexture = new Texture2D(inputTexture.width, inputTexture.height, TextureFormat.ARGB32, false);
			}

			RenderTexture.active = tempRT;
			resultTexture.ReadPixels(new Rect(0f, 0f, inputTexture.width, inputTexture.height), 0, 0, false);
			resultTexture.Apply(false, false);
			RenderTexture.ReleaseTemporary(tempRT);

			RenderTexture.active = prevRT;

			return resultTexture;
		}		
	}
}