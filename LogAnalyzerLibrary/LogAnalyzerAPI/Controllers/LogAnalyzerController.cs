using LogAnalyzerLibrary;
using Microsoft.AspNetCore.Mvc;

namespace LogAnalyzerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogAnalyzer _logAnalyzer;

        public LogController()
        {
            _logAnalyzer = new LogAnalyzer();
        }

        /*the get endpoint to search for all logs
        Note: to search for all logs the searchpattern is .log*/

        [HttpGet("search")]
        public ActionResult<IEnumerable<string>> SearchLogsInDirectories([FromQuery] IEnumerable<string> directories, [FromQuery] string searchPattern)
        {
            var logs = _logAnalyzer.SearchLogsInDirectories(directories, searchPattern);
            return Ok(logs);
        }

        // the end point to count unique errors

        [HttpGet("count-unique-errors")]
        public ActionResult<IDictionary<string, int>> CountUniqueErrors([FromQuery] IEnumerable<string> files)
        {
            var result = _logAnalyzer.CountUniqueErrors(files);
            return Ok(result);
        }

        //the end point to coun dupilcate errors

        [HttpGet("count-duplicated-errors")]
        public ActionResult<IDictionary<string, int>> CountDuplicatedErrors([FromQuery] IEnumerable<string> files)
        {
            var result = _logAnalyzer.CountDuplicatedErrors(files);
            return Ok(result);
        }

        // the end point to delete archives logs

        [HttpDelete("delete-archives")]
        public IActionResult DeleteArchivesFromPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _logAnalyzer.DeleteArchivesFromPeriod(directory, startDate, endDate);
            return NoContent();
        }

        // end point for posting/uploading Archive logs

        [HttpPost("archive-logs")]
        public IActionResult ArchiveLogsFromPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _logAnalyzer.ArchiveLogsFromPeriod(directory, startDate, endDate);
            return NoContent();
        }

        //end points for posting/uploading logs

        [HttpPost("upload-logs")]
        public async Task<IActionResult> UploadLogsToRemoteServer([FromQuery] IEnumerable<string> files, [FromQuery] string apiUrl)
        {
            await _logAnalyzer.UploadLogsToRemoteServer(files, apiUrl);
            return NoContent();
        }

        // end point to delete logs

        [HttpDelete("delete-logs")]
        public IActionResult DeleteLogsFromPeriod([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _logAnalyzer.DeleteLogsFromPeriod(directory, startDate, endDate);
            return NoContent();
        }

        //enfpoints to count total logs in directory

        [HttpGet("count-total-logs")]
        public ActionResult<int> CountTotalAvailableLogs([FromQuery] string directory, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var count = _logAnalyzer.CountTotalAvailableLogs(directory, startDate, endDate);
            return Ok(count);
        }

        //end point to search by size in kilobytes

        [HttpGet("search-by-size")]
        public ActionResult<IEnumerable<string>> SearchLogsBySize([FromQuery] string directory, [FromQuery] long minSize, [FromQuery] long maxSize)
        {
            var logs = _logAnalyzer.SearchLogsBySize(directory, minSize, maxSize);
            return Ok(logs);
        }

        //end point to search for logs by directory you have to pass the directorey and search pattern

        [HttpGet("search-by-directory")]
        public ActionResult<IEnumerable<string>> SearchLogsByDirectory([FromQuery] string directory, [FromQuery] string searchPattern)
        {
            var logs = _logAnalyzer.SearchLogsByDirectory(directory, searchPattern);
            return Ok(logs);
        }
    }
}