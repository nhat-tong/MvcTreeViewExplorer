using MvcTreeViewExplorer.Areas.Explorer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace MvcTreeViewExplorer.Areas.Explorer.Bll
{
    public class ExplorerBll
    {
        public ExplorerBll()
        {
        }

        #region GetItemsByPath
        /// <summary>
        /// Récupérer les drives
        /// </summary>
        /// <returns></returns>
        public IExplorerItem GetItemsByPath(string path)
        {
            Debug.Print("Start: " + DateTime.Now.ToString());

            var root = new DirectoryItem
            {
                Name = "Root",
                Path = path,
                ChildItems = RecursiveDirectory(new DirectoryInfo(path))
            };

            /*
            var driveListOnComputer = Environment.GetLogicalDrives();
            foreach (var driveName in driveListOnComputer)
            {
            }
            */

            Debug.Print("End: " + DateTime.Now.ToString());

            return root;
        }
        #endregion

        #region GetChildItemsByDrive
        /// <summary>
        /// Récupérer les enfants d'un drive
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<IExplorerItem> GetChildItemsByDrive(string driveName)
        {
            DriveInfo driveInfo = new DriveInfo(driveName);
            var rootDirectory = driveInfo.RootDirectory;
            return RecursiveDirectory(rootDirectory);
        }
        #endregion

        #region RecursiveDirectory
        /// <summary>
        /// Récupérer les items au sein d'un répertoire
        /// </summary>
        /// <returns></returns>
        public List<IExplorerItem> RecursiveDirectory(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists) return null;

            var childItems = new List<IExplorerItem>();
            var fileSystemInfos = directoryInfo.GetFileSystemInfos();

            Parallel.ForEach(fileSystemInfos, (fileSystem) =>
            {
                if (!IsReadable(fileSystem) || !HasAccessControl(fileSystem)) return;

                if (fileSystem.Attributes.HasFlag(FileAttributes.Directory))
                {
                    var item = GetDirectoryItemInfo(fileSystem as DirectoryInfo);
                    if (item != null)
                    {
                        childItems.Add(item);
                    }
                }
                else
                {
                    childItems.Add(GetFileItemInfo(fileSystem as FileInfo));
                }
            });

            return (childItems.Count > 0 ? childItems : null);
        }
        #endregion

        #region GetFileItemInfo
        /// <summary>
        /// Récupérer les infos du fichier
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IExplorerItem GetFileItemInfo(FileInfo fileInfo)
        {
            return new FileItem
            {
                Path = fileInfo.FullName,
                Name = fileInfo.Name,
                ModifiedDate = fileInfo.LastWriteTime,
                Type = fileInfo.GetType().Name,
                Size = fileInfo.Length,
                ChildItems = null
            };
        }
        #endregion

        #region GetDirectoryItemInfo
        /// <summary>
        /// Récupérer les infos d'un directory
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public IExplorerItem GetDirectoryItemInfo(DirectoryInfo directoryInfo)
        {
            try
            {
                return new DirectoryItem
                {
                    Path = directoryInfo.FullName,
                    Name = directoryInfo.Name,
                    ModifiedDate = directoryInfo.LastWriteTime,
                    Type = directoryInfo.GetType().Name,
                    ChildItems = !directoryInfo.Attributes.HasFlag(FileAttributes.Archive) ? RecursiveDirectory(directoryInfo) : null
                };
            }
            catch (PathTooLongException)
            {
                return null;
            }
        }
        #endregion

        #region CheckAccessControl
        /// <summary>
        /// Obtient si l'accès au fichier a été accordé
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        public bool HasAccessControl(FileSystemInfo fileSystem)
        {
            try
            {
                FileSystemSecurity fileSystemSecurity = null;

                if (fileSystem.Attributes.HasFlag(FileAttributes.Directory))
                {
                    fileSystemSecurity = Directory.GetAccessControl(fileSystem.FullName);
                }
                else
                {
                    fileSystemSecurity = File.GetAccessControl(fileSystem.FullName);
                }
                var accessRules = fileSystemSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
                if (accessRules == null) return false;

                foreach (FileSystemAccessRule rule in accessRules)
                {
                    if ((rule.AccessControlType == AccessControlType.Allow) && (rule.FileSystemRights == FileSystemRights.FullControl
                        || rule.FileSystemRights == FileSystemRights.ReadAndExecute
                        || rule.FileSystemRights == FileSystemRights.Read))
                    {
                        return true;
                    }
                }
            }
            catch (PathTooLongException)
            {
                return false;
            }

            return false;
        }
        #endregion

        #region IsReadable
        /// <summary>
        /// Obtient si le FileSysteam a pu lire
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        public bool IsReadable(FileSystemInfo fileSystem)
        {
            if (((fileSystem.Attributes.HasFlag(FileAttributes.System) && fileSystem.Attributes.HasFlag(FileAttributes.Hidden)) &&
                (fileSystem.Attributes.HasFlag(FileAttributes.Directory) || fileSystem.Attributes.HasFlag(FileAttributes.Archive) || fileSystem.Attributes.HasFlag(FileAttributes.ReparsePoint))))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}