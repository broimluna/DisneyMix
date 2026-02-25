using System.IO;

namespace Disney.Mix.SDK.Internal
{
	public class PhotoStorage : IPhotoStorage
	{
		private const string PhotosDirectory = "photos";

		private readonly IFileSystem fileSystem;

		private readonly string localStorageDirPath;

		public PhotoStorage(IFileSystem fileSystem, string localStorageDirPath)
		{
			this.fileSystem = fileSystem;
			this.localStorageDirPath = localStorageDirPath;
		}

		public void Store(string mediaId, string photoFlavorId, byte[] contents, byte[] encryptionKey)
		{
			string photoDirectoryPath = GetPhotoDirectoryPath();
			EnsureDirectoryExists(photoDirectoryPath);
			string photoFilePath = GetPhotoFilePath(mediaId, photoFlavorId);
			WriteFile(photoFilePath, contents, encryptionKey);
		}

		public byte[] Load(string mediaId, string photoFlavorId, byte[] encryptionKey)
		{
			byte[] result = null;
			string photoFilePath = GetPhotoFilePath(mediaId, photoFlavorId);
			if (fileSystem.FileExists(photoFilePath))
			{
				result = fileSystem.ReadFile(photoFilePath);
			}
			return result;
		}

		private string GetPhotoFilePath(string photoId, string photoFlavorId)
		{
			return HashedPathGenerator.GetPath(GetPhotoDirectoryPath(), photoId + photoFlavorId);
		}

		private string GetPhotoDirectoryPath()
		{
			return Path.Combine(localStorageDirPath, "photos");
		}

		private void EnsureDirectoryExists(string path)
		{
			if (!fileSystem.DirectoryExists(path))
			{
				fileSystem.CreateDirectory(path);
			}
		}

		private void WriteFile(string path, byte[] contents, byte[] encryptionKey)
		{
			fileSystem.WriteFile(path, contents);
		}
	}
}
