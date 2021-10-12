            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
                using (FileStream file = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    workbook.Write(file);
                    file.Close();
                }
            }
            else
            {
                using (FileStream file = File.Create(filePath))
                {
                    FileSecurity fileSecurity = File.GetAccessControl(filePath);
                    SecurityIdentifier userAccount = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    fileSecurity.AddAccessRule(new FileSystemAccessRule(userAccount, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(filePath, fileSecurity);
                    workbook.Write(file);
                    file.Close();
                }
            }

            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    WorkBook loadedWorkBook = WorkBook.Load(fileStream.Name);
                    string loadedWorkSheetName = loadedWorkBook.DefaultWorkSheet.Name;
                    loadedWorkBook.RemoveWorkSheet(loadedWorkSheetName);
                    workSheet.CopyTo(loadedWorkBook, loadedWorkSheetName);
                    loadedWorkBook.Save();
                    File.SetAttributes(filePath, FileAttributes.ReadOnly);
                }
            }

            workBook.SaveAs(filePath);
            FileSecurity fileSecurity = File.GetAccessControl(filePath);
            SecurityIdentifier userAccount = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            fileSecurity.AddAccessRule(new FileSystemAccessRule(userAccount, FileSystemRights.FullControl, AccessControlType.Allow));
            File.SetAccessControl(filePath, fileSecurity);
            File.SetAttributes(filePath, FileAttributes.ReadOnly);

            if (File.Exists(filePath))
            {
                WorkBook loadedWorkBook = WorkBook.Load(filePath);
                string loadedWorkSheetName = loadedWorkBook.DefaultWorkSheet.Name;
                loadedWorkBook.RemoveWorkSheet(loadedWorkSheetName);
                workSheet.CopyTo(loadedWorkBook, loadedWorkSheetName);
                loadedWorkBook.Save();
                return;
            }