using System;
using System.IO;

namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Support for the creation and housekeeping of temporary files.
	/// </summary>
	public class TemporaryFileManager
	{
		Random _random = new Random();

		public TemporaryFileManager()
		{
		}

		public string CreateTemporaryFile()
		{
			string tempFilename = GenerateTemporaryFilename();
			while (File.Exists(tempFilename)) 
			{
				tempFilename = GenerateTemporaryFilename();
			}
			File.Create(tempFilename).Close();
			return tempFilename;
		}

		string GenerateTemporaryFilename()
		{
			return Path.Combine(Path.GetTempPath(), String.Format("DependancyAnaylser.{0:X}.tmp", _random.Next(int.MaxValue >> 4)));
		}

		public void DeleteAllTemporaryFiles()
		{
			string[] filenames = GetExistingTemporaryFilenames();
			foreach (string filename in filenames)
			{
				try
				{
					File.Delete(filename);
				}
				catch
				{
					// file is probably locked
				}
			}
		}

		public string[] GetExistingTemporaryFilenames()
		{
			FileInfo[] fileInfoArray = new DirectoryInfo(Path.GetTempPath()).GetFiles("DependancyAnaylser.*.tmp");
			string[] filenames = new string[fileInfoArray.Length];
			for (int index=0; index<fileInfoArray.Length; index++)
			{
				filenames[index] = fileInfoArray[index].FullName;
			}
			return filenames;
		}
	}
}