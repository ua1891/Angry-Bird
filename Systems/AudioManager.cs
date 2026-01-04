using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace LaeeqFramwork.Systems
{
   
    public sealed class AudioManager : IDisposable
    {
        private static AudioManager _instance;
        public static AudioManager Instance => _instance ?? (_instance = new AudioManager());
        // ================= SFX =================

        // ================= MUSIC =================
        private IWavePlayer _musicOutput;
        private AudioFileReader _musicReader;
        private LoopStream _musicLoop;

        private readonly List<IWavePlayer> _activeSfxPlayers = new List<IWavePlayer>();
        private int _activeSfxCount = 0;

        public float MusicVolume { get; private set; } = 0.5f;
        public float SfxVolume { get; private set; } = 0.8f;

        private float _normalMusicVolume = 0.5f;
        private float _duckedMusicVolume = 0.2f;

        private AudioManager() { }

        public void PlayMusic(string relativePath, bool loop = true)
        {
            StopMusic();

            string fullPath = GetFullPath(relativePath);
            try {
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException("Music file not found", fullPath);

                _musicReader = new AudioFileReader(fullPath)
                {
                    Volume = _normalMusicVolume
                };

                ISampleProvider provider = _musicReader;

                if (loop)
                {
                    _musicLoop = new LoopStream(_musicReader);
                    provider = _musicLoop.ToSampleProvider();
                }

                _musicOutput = new WaveOutEvent();
                _musicOutput.Init(provider);
                _musicOutput.Play();
            }
            catch {
                Console.WriteLine("File Not found");

            }
            }
        public void StopMusic()
        {
            _musicOutput?.Stop();
            _musicOutput?.Dispose();
            _musicReader?.Dispose();

            _musicOutput = null;
            _musicReader = null;
            _musicLoop = null;
        }

        public void SetMusicVolume(float volume)
        {
            _normalMusicVolume = Clamp01(volume);
            MusicVolume = _normalMusicVolume;

            if (_musicReader != null)
                _musicReader.Volume = MusicVolume;
        }

        public void PlaySfx(string relativePath)
        {
            string fullPath = GetFullPath(relativePath);

            if (!File.Exists(fullPath))
                return; 

            var reader = new AudioFileReader(fullPath)
            {
                Volume = SfxVolume
            };

            var output = new WaveOutEvent();

            
            _activeSfxCount++;
            DuckMusic();

            output.Init(reader);
            output.Play();

            _activeSfxPlayers.Add(output);

            output.PlaybackStopped += (s, e) =>
            {
                output.Dispose();
                reader.Dispose();
                _activeSfxPlayers.Remove(output);

                _activeSfxCount--;

               
                if (_activeSfxCount <= 0)
                {
                    _activeSfxCount = 0;
                    RestoreMusic();
                }
            };
        }

        public void SetSfxVolume(float volume)
        {
            SfxVolume = Clamp01(volume);
        }

        // ================= DUCKING =================
        private void DuckMusic()
        {
            if (_musicReader != null)
                _musicReader.Volume = _duckedMusicVolume;
        }

        private void RestoreMusic()
        {
            if (_musicReader != null)
                _musicReader.Volume = _normalMusicVolume;
        }

        // ================= PATH + UTIL =================
        private string GetFullPath(string relativePath)
        {
            return Path.Combine(
                Application.StartupPath,
                relativePath
            );
        }

        private float Clamp01(float value)
        {
            if (value < 0f) return 0f;
            if (value > 1f) return 1f;
            return value;
        }

        public void Dispose()
        {
            StopMusic();
            foreach (var sfx in _activeSfxPlayers)
                sfx.Dispose();

            _activeSfxPlayers.Clear();
        }
    }

   
    public class LoopStream : WaveStream
    {
        private readonly WaveStream _sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            _sourceStream = sourceStream;
        }

        public override WaveFormat WaveFormat => _sourceStream.WaveFormat;
        public override long Length => long.MaxValue;

        public override long Position
        {
            get => _sourceStream.Position;
            set => _sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = _sourceStream.Read(buffer, offset, count);
            if (read == 0)
            {
                _sourceStream.Position = 0;
                read = _sourceStream.Read(buffer, offset, count);
            }
            return read;
        }
       

    }
}
