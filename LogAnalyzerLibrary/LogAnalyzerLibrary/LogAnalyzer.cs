using System.IO.Compression;

namespace LogAnalyzerLibrary
{
    public class LogAnalyzer
    {
        public IEnumerable<string> SearchLogsInDirectories(IEnumerable<string> directories, string searchPattern)
        {
            var files = new List<string>();
            foreach (var directory in directories)
            {
                if (Directory.Exists(directory))
                {
                    files.AddRange(Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories));
                }
            }
            return files.Where(file => File.ReadAllText(file).Contains(searchPattern));
        }

        public IDictionary<string, int> CountUniqueErrors(IEnumerable<string> files)
        {
            var errorCounts = new Dictionary<string, int>();
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file).Distinct();
                foreach (var line in lines)
                {
                    if (errorCounts.ContainsKey(line))
                        errorCounts[line]++;
                    else
                        errorCounts[line] = 1;
                }
            }
            return errorCounts;
        }

        public IDictionary<string, int> CountDuplicatedErrors(IEnumerable<string> files)
        {
            var errorCounts = new Dictionary<string, int>();
            try
            {
                foreach (var file in files)
                {
                    var lines = File.ReadAllLines(file);
                    var duplicates = lines.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key);
                    foreach (var line in duplicates)
                    {
                        if (errorCounts.ContainsKey(line))
                            errorCounts[line]++;
                        else
                            errorCounts[line] = 1;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return errorCounts;
        }

        public void DeleteArchivesFromPeriod(string directory, DateTime startDate, DateTime endDate)
        {
            var files = Directory.GetFiles(directory, "*.zip", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var creationTime = File.GetCreationTime(file);
                if (creationTime >= startDate && creationTime <= endDate)
                {
                    File.Delete(file);
                }
            }
        }

        public void ArchiveLogsFromPeriod(string directory, DateTime startDate, DateTime endDate)
        {
            var files = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
            var logsToArchive = files.Where(f => File.GetCreationTime(f) >= startDate && File.GetCreationTime(f) <= endDate).ToList();
            var archiveName = $"{startDate:dd_MM_yyyy}-{endDate:dd_MM_yyyy}.zip";
            using (var archive = ZipFile.Open(Path.Combine(directory, archiveName), ZipArchiveMode.Create))
            {
                foreach (var log in logsToArchive)
                {
                    archive.CreateEntryFromFile(log, Path.GetFileName(log));
                    File.Delete(log);
                }
            }
        }

        public async Task UploadLogsToRemoteServer(IEnumerable<string> files, string apiUrl)
        {
            using (var client = new HttpClient())
            {
                foreach (var file in files)
                {
                    var content = new MultipartFormDataContent();
                    var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(file));
                    content.Add(fileContent, "file", Path.GetFileName(file));
                    await client.PostAsync(apiUrl, content);
                }
            }
        }

        public void DeleteLogsFromPeriod(string directory, DateTime startDate, DateTime endDate)
        {
            var files = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var creationTime = File.GetCreationTime(file);
                if (creationTime >= startDate && creationTime <= endDate)
                {
                    File.Delete(file);
                }
            }
        }

        public int CountTotalAvailableLogs(string directory, DateTime startDate, DateTime endDate)
        {
            var files = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
            return files.Count(f => File.GetCreationTime(f) >= startDate && File.GetCreationTime(f) <= endDate);
        }

        public IEnumerable<string> SearchLogsBySize(string directory, long minSize, long maxSize)
        {
            var logs = new List<string>();
            var files = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var fileSize = new FileInfo(file).Length;
                if (fileSize >= minSize && fileSize <= maxSize)
                {
                    logs.Add(file);
                }
            }
            return logs;
        }

        public IEnumerable<string> SearchLogsByDirectory(string directory, string searchPattern)
        {
            var logs = new List<string>();
            var files = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (File.ReadAllText(file).Contains(searchPattern))
                {
                    logs.Add(file);
                }
            }
            return logs;
        }
    }
}