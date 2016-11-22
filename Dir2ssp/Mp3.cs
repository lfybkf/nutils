﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dir2ssp
{
	public class Mp3
	{
		enum MpegLayer
		{
			/// <summary>
			/// Reserved
			/// </summary>
			Reserved,
			/// <summary>
			/// Layer 3
			/// </summary>
			Layer3,
			/// <summary>
			/// Layer 2
			/// </summary>
			Layer2,
			/// <summary>
			/// Layer 1
			/// </summary>
			Layer1
		}//enum
		enum MpegVersion
		{
			/// <summary>
			/// Version 2.5
			/// </summary>
			Version25,
			/// <summary>
			/// Reserved
			/// </summary>
			Reserved,
			/// <summary>
			/// Version 2
			/// </summary>
			Version2,
			/// <summary>
			/// Version 1
			/// </summary>
			Version1
		}//enum
		enum ChannelMode
		{
			/// <summary>
			/// Stereo
			/// </summary>
			Stereo,
			/// <summary>
			/// Joint Stereo
			/// </summary>
			JointStereo,
			/// <summary>
			/// Dual Channel
			/// </summary>
			DualChannel,
			/// <summary>
			/// Mono
			/// </summary>
			Mono
		}//enum
		class Mp3Frame
		{
			private static readonly int[, ,] bitRates = new int[,,] {
            {
                // MPEG Version 1
                { 0, 32, 64, 96, 128, 160, 192, 224, 256, 288, 320, 352, 384, 416, 448 }, // Layer 1
                { 0, 32, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 384 }, // Layer 2
                { 0, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 }, // Layer 3
            },
            {
                // MPEG Version 2 & 2.5
                { 0, 32, 48, 56, 64, 80, 96, 112, 128, 144, 160, 176, 192, 224, 256 }, // Layer 1
                { 0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160 }, // Layer 2 
                { 0, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160 }, // Layer 3 (same as layer 2)
            }
        };

			private static readonly int[,] samplesPerFrame = new int[,] {
            {   // MPEG Version 1
                384,    // Layer1
                1152,   // Layer2
                1152    // Layer3
            },
            {   // MPEG Version 2 & 2.5
                384,    // Layer1
                1152,   // Layer2
                576     // Layer3
            }
        };

			private static readonly int[] sampleRatesVersion1 = new int[] { 44100, 48000, 32000 };
			private static readonly int[] sampleRatesVersion2 = new int[] { 22050, 24000, 16000 };
			private static readonly int[] sampleRatesVersion25 = new int[] { 11025, 12000, 8000 };

			private bool crcPresent;
			//private short crc;
			private const int MaxFrameLength = 16 * 1024;

			/// <summary>
			/// Reads an MP3 frame from a stream
			/// </summary>
			/// <param name="input">input stream</param>
			/// <returns>A valid MP3 frame, or null if none found</returns>
			public static Mp3Frame LoadFromStream(Stream input)
			{
				return LoadFromStream(input, true);
			}

			/// <summary>Reads an MP3Frame from a stream</summary>
			/// <remarks>http://mpgedit.org/mpgedit/mpeg_format/mpeghdr.htm has some good info
			/// also see http://www.codeproject.com/KB/audio-video/mpegaudioinfo.aspx
			/// </remarks>
			/// <returns>A valid MP3 frame, or null if none found</returns>
			public static Mp3Frame LoadFromStream(Stream input, bool readData)
			{
				byte[] headerBytes = new byte[4];
				int bytesRead = input.Read(headerBytes, 0, headerBytes.Length);
				if (bytesRead < headerBytes.Length)
				{
					// reached end of stream, no more MP3 frames
					return null;
				}
				Mp3Frame frame = new Mp3Frame();

				while (!IsValidHeader(headerBytes, frame))
				{
					// shift down by one and try again
					headerBytes[0] = headerBytes[1];
					headerBytes[1] = headerBytes[2];
					headerBytes[2] = headerBytes[3];
					bytesRead = input.Read(headerBytes, 3, 1);
					if (bytesRead < 1)
					{
						return null;
					}
				}
				/* no longer read the CRC since we include this in framelengthbytes
				if (this.crcPresent)
						this.crc = reader.ReadInt16();*/

				int bytesRequired = frame.FrameLength - 4;
				if (readData)
				{
					frame.RawData = new byte[frame.FrameLength];
					Array.Copy(headerBytes, frame.RawData, 4);
					bytesRead = input.Read(frame.RawData, 4, bytesRequired);
					if (bytesRead < bytesRequired)
					{
						// TODO: could have an option to suppress this
						//, although it does indicate a corrupt file
						// for now, caller should handle this exception
						//throw new EndOfStreamException("Unexpected end of stream before frame complete");
					}
				}
				else
				{
					// n.b. readData should not be false if input stream does not support seeking
					input.Position += bytesRequired;
				}

				return frame;
			}


			/// <summary>
			/// Constructs an MP3 frame
			/// </summary>
			private Mp3Frame()
			{

			}

			/// <summary>
			/// checks if the four bytes represent a valid header,
			/// if they are, will parse the values into Mp3Frame
			/// </summary>
			private static bool IsValidHeader(byte[] headerBytes, Mp3Frame frame)
			{
				if ((headerBytes[0] == 0xFF) && ((headerBytes[1] & 0xE0) == 0xE0))
				{
					// TODO: could do with a bitstream class here
					frame.MpegVersion = (MpegVersion)((headerBytes[1] & 0x18) >> 3);
					if (frame.MpegVersion == MpegVersion.Reserved)
					{
						//throw new FormatException("Unsupported MPEG Version");
						return false;
					}

					frame.MpegLayer = (MpegLayer)((headerBytes[1] & 0x06) >> 1);

					if (frame.MpegLayer == MpegLayer.Reserved)
					{
						return false;
					}
					int layerIndex = frame.MpegLayer == MpegLayer.Layer1 ? 0 : frame.MpegLayer == MpegLayer.Layer2 ? 1 : 2;
					frame.crcPresent = (headerBytes[1] & 0x01) == 0x00;
					int bitRateIndex = (headerBytes[2] & 0xF0) >> 4;
					if (bitRateIndex == 15)
					{
						// invalid index
						return false;
					}
					int versionIndex = frame.MpegVersion == MpegVersion.Version1 ? 0 : 1;
					frame.BitRate = bitRates[versionIndex, layerIndex, bitRateIndex] * 1000;
					if (frame.BitRate == 0)
					{
						return false;
					}
					int sampleFrequencyIndex = (headerBytes[2] & 0x0C) >> 2;
					if (sampleFrequencyIndex == 3)
					{
						return false;
					}

					if (frame.MpegVersion == MpegVersion.Version1)
					{
						frame.SampleRate = sampleRatesVersion1[sampleFrequencyIndex];
					}
					else if (frame.MpegVersion == MpegVersion.Version2)
					{
						frame.SampleRate = sampleRatesVersion2[sampleFrequencyIndex];
					}
					else
					{
						// mpegVersion == MpegVersion.Version25
						frame.SampleRate = sampleRatesVersion25[sampleFrequencyIndex];
					}

					bool padding = (headerBytes[2] & 0x02) == 0x02;
					bool privateBit = (headerBytes[2] & 0x01) == 0x01;
					frame.ChannelMode = (ChannelMode)((headerBytes[3] & 0xC0) >> 6);
					int channelExtension = (headerBytes[3] & 0x30) >> 4;
					bool copyright = (headerBytes[3] & 0x08) == 0x08;
					bool original = (headerBytes[3] & 0x04) == 0x04;
					int emphasis = (headerBytes[3] & 0x03);

					int nPadding = padding ? 1 : 0;

					frame.SampleCount = samplesPerFrame[versionIndex, layerIndex];
					int coefficient = frame.SampleCount / 8;
					if (frame.MpegLayer == MpegLayer.Layer1)
					{
						frame.FrameLength = (coefficient * frame.BitRate / frame.SampleRate + nPadding) * 4;
					}
					else
					{
						frame.FrameLength = (coefficient * frame.BitRate) / frame.SampleRate + nPadding;
					}

					if (frame.FrameLength > MaxFrameLength)
					{
						return false;
					}
					return true;
				}
				return false;
			}

			/// <summary>
			/// Sample rate of this frame
			/// </summary>
			public int SampleRate { get; private set; }

			/// <summary>
			/// Frame length in bytes
			/// </summary>
			public int FrameLength { get; private set; }

			/// <summary>
			/// Bit Rate
			/// </summary>
			public int BitRate { get; private set; }

			/// <summary>
			/// Raw frame data (includes header bytes)
			/// </summary>
			public byte[] RawData { get; private set; }

			/// <summary>
			/// MPEG Version
			/// </summary>
			public MpegVersion MpegVersion { get; private set; }

			/// <summary>
			/// MPEG Layer
			/// </summary>
			public MpegLayer MpegLayer { get; private set; }

			/// <summary>
			/// Channel Mode
			/// </summary>
			public ChannelMode ChannelMode { get; private set; }

			/// <summary>
			/// The number of samples in this frame
			/// </summary>
			public int SampleCount { get; private set; }
		}//class


		public string MediaFile { get; set; }
		public int Duration { get; set; }
		public string Name { get { return Path.GetFileNameWithoutExtension(MediaFile); } }
		static Mp3[] Empty = new Mp3[0];



		public static Mp3[] GetLib()
		{
			string pathMp3lib = Directory.EnumerateFiles(Environment.CurrentDirectory, "mp3*.txt").FirstOrDefault();
			IEnumerable<string> files;
			if (File.Exists(pathMp3lib))
			{
				files = File.ReadAllLines(pathMp3lib).TakeWhile(z => z.Trim().Any());
			}//if
			else
			{
				files = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.mp3");
			}//else
			return files
				.Select(z => Parse(z))
				.Where(z => z != null && z.Duration > 0)
				.ToArray();
		}//function

		static Mp3 Parse(string s)
		{
			Mp3 result = null;
			string[] ss = s.Split('\t', ';');
			if (ss.Length == 2)
			{
				TimeSpan ts;
				if (TimeSpan.TryParseExact(ss[1], @"m\:ss", null, out ts))
				{
					result = new Mp3 { MediaFile = ss[0], Duration = (int)ts.TotalMilliseconds };
				}//if
			}//if
			else if (ss.Length == 1)
			{
				var file = ss[0];
				if (File.Exists(file))
				{
					result = new Mp3 { MediaFile = file, Duration = (int)GetMediaDuration(file) };	
				}//if
			}//else
			return result;
		}//function

		public static double GetMediaDuration(string MediaFilename)
		{
			double duration = 0.0;

			try
			{
				#region my region
				using (FileStream fs = File.OpenRead(MediaFilename))
				{
					Mp3Frame frame = Mp3Frame.LoadFromStream(fs);
					while (frame != null)
					{
						if (frame.ChannelMode == ChannelMode.Mono)
						{
							duration += (double)frame.SampleCount * 2.0 / (double)frame.SampleRate;
						}
						else if (frame.ChannelMode == ChannelMode.JointStereo)
						{
							duration += (double)frame.SampleCount * 1.0 / (double)frame.SampleRate;
						}
						else
						{
							duration += (double)frame.SampleCount * 1.0 / (double)frame.SampleRate;
						}
						frame = Mp3Frame.LoadFromStream(fs);
					}
				}
				#endregion
			}//try
			catch (Exception exception)
			{
				File.AppendAllLines("GetMediaDuration.log"
					, new string[] { string.Format("{0} {1}", MediaFilename, exception.Message) });
			}//catch


			return duration *1000;
		}//function
	}//class
}//namespace