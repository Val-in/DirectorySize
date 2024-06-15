using System;
using System.IO;

namespace DirectoryExtentions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                WriteDriveInfo(drive);
                if (drive.IsReady)
                {
                    DirectoryInfo rootDir = drive.RootDirectory;
                    DirectoryInfo[] folders = rootDir.GetDirectories();
                    WriteFolderInfo(folders);
                }
            }
        }

        public static void WriteDriveInfo(DriveInfo drive)
        {
            Console.WriteLine($"Название: {drive.Name}");
            Console.WriteLine($"Тип: {drive.DriveType}");
            Console.WriteLine($"Метка {drive.VolumeLabel}");
        }

        public static void WriteFolderInfo(DirectoryInfo[] folders)
        {
            Console.WriteLine();
            Console.WriteLine("Папки: ");
            Console.WriteLine();

            foreach (DirectoryInfo folder in folders)
            try
            {
                Console.WriteLine(folder.Name + $" - {DirSize(folder)} bites");
            }
            catch (Exception e)
            {
                Console.WriteLine(folder.Name + $" - Error: {e.Message}");
            }
        }

        public static long DirSize(DirectoryInfo dir)
        {
            long size = 0;

            // Добавляем размер файлов в директории
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            // Добавляем размер поддиректорий
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in dirs)
            {
                size += DirSize(subdir); // вызывается рекурсивно
            }

            return size;
        }
    }
}
