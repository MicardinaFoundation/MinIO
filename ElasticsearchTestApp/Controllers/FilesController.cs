using ElasticsearchTestApp.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/file")]
public class FilesController : ControllerBase
{

    private readonly FileStorageService _storage;

    public FilesController(FileStorageService storage)
    {
        _storage = storage;
    }

    /// <summary>
    /// Загрузка файлов в хранилище
    /// </summary>
    /// <param name="file">Файл для загрузки</param>
    /// <returns>Загрузка</returns>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Отсутствует файл");

        try
        {
            await using var stream = file.OpenReadStream();

            await _storage.UploadAsync(
                file.FileName,
                "files",
                stream,
                file.ContentType);

            return Ok(new { file = file.FileName });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при загрузке: {ex.Message}");
        }
    }

    /// <summary>
    /// Скачивание файлов с хранилища
    /// </summary>
    /// <param name="name">Имя файла</param>
    /// <returns></returns>
    [HttpGet("download/{name}")]
    public async Task<IActionResult> Download(string name)
    {
        try
        {
            var stream = await _storage.DownloadAsync(name, "files");

            return File(
                stream,
                "application/octet-stream",
                name);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при скачивании: {ex.Message}");
        }
    }

    /// <summary>
    /// Получение списка всех файлов в бакете
    /// </summary>
    /// <returns>Список файлов</returns>
    [HttpGet("list")]
    public async Task<IActionResult> ListFiles()
    {
        try
        {
            var files = await _storage.GetFilesListAsync("files");
            return Ok(files);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при получении списка: {ex.Message}");
        }
    }

    /// <summary>
    /// Получение временной ссылки на скачивание (Presigned URL)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="expiryInSeconds"></param>
    /// <returns></returns>
    [HttpGet("presigned-url/{name}")]
    public async Task<IActionResult> GetPresignedUrl(string name, [FromQuery] int expiryInSeconds = 3600)
    {
        try
        {
            var url = await _storage.GetPresignedUrlAsync("files", name, expiryInSeconds);

            return Ok(new
            {
                FileName = name,
                Url = url,
                ExpiresInSeconds = expiryInSeconds
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при генерации ссылки: {ex.Message}");
        }
    }
}