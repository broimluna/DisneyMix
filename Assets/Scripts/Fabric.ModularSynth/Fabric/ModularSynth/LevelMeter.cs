using System;

namespace Fabric.ModularSynth
{
	public class LevelMeter : Module
	{
		private AudioInputPin audioInput = new AudioInputPin();

		private AudioOutputPin audioOutput = new AudioOutputPin();

		public VolumeMeterState volumeMeterState = new VolumeMeterState();

		public LevelMeter()
		{
			RegisterPin(audioInput, "Input");
			RegisterPin(audioOutput, "Output");
			for (int i = 0; i < 5; i++)
			{
				volumeMeterState.mHistory[i] = new VolumeMeterState.stSpeakers();
			}
		}

		public static void VolumeMeterProcess(ref VolumeMeterState outState, AudioBuffer audioBuffer)
		{
			VolumeMeterState.stSpeakers stSpeakers = outState.mHistory[outState.mHistoryIndex];
			outState.mHistoryIndex++;
			outState.mHistoryIndex %= 5;
			stSpeakers.Clear();
			int size = audioBuffer.Size;
			float num = 0f;
			for (int i = 0; i < 256; i += audioBuffer.NumChannels)
			{
				for (int j = 0; j < audioBuffer.NumChannels; j++)
				{
					float num2 = audioBuffer[i + j];
					float val = ((num2 < 0f) ? (0f - num2) : num2);
					float val2 = stSpeakers.mChannels[j];
					stSpeakers.mChannels[j] = Math.Max(val2, val);
					num += num2 * num2;
				}
			}
			if (size > 0)
			{
				num /= 256f;
				num = (float)Math.Sqrt(num);
				num = Math.Max(num, 0f);
				num = Math.Min(num, 1f);
			}
			stSpeakers.mRMS = num;
			outState.mPeaks.Clear();
			outState.mRMS = 0f;
			for (int k = 0; k < 5; k++)
			{
				VolumeMeterState.stSpeakers stSpeakers2 = outState.mHistory[k];
				outState.mRMS += stSpeakers2.mRMS;
				for (int l = 0; l < 2; l++)
				{
					outState.mPeaks.mChannels[l] += stSpeakers2.mChannels[l];
				}
			}
			float num3 = 0.2f;
			outState.mRMS *= num3;
			for (int m = 0; m < 2; m++)
			{
				outState.mPeaks.mChannels[m] *= num3;
			}
		}

		public override void OnAudioPinsUpdate()
		{
			if (audioInput.IsConnected() && audioOutput.HasBuffer())
			{
				AudioBuffer buffer = audioInput.GetBuffer();
				AudioBuffer buffer2 = audioOutput.GetBuffer();
				VolumeMeterProcess(ref volumeMeterState, buffer);
				buffer.CopyToBuffer(buffer2);
			}
		}
	}
}
