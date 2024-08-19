using Microsoft.AspNetCore.Mvc;
using backnc.Data.POCOEntities;
using backnc.Service;
using backnc.Common.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using backnc.Common.DTOs.JobDTO;
using backnc.Common.Response;
using backnc.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
	private readonly JobService _jobService;
	private readonly ProfileService _profileService;

	public JobController(JobService jobService, ProfileService profileService)
	{
		_jobService = jobService;
		_profileService = profileService;
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> CreateJob([FromForm] CreateJobDTO createJobDTO)
	{
		var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		var profile = await _profileService.GetProfileByUser(userId);

		if (profile == null)
		{
			return NotFound(new BaseResponse("Perfil no encontrado."));
		}

		var job = new Job
		{
			ProfileId = profile.Id,
			UserId = profile.UserId,
			Title = createJobDTO.Title,
			Description = createJobDTO.Description
		};

		if (createJobDTO.Image != null)
		{
			try
			{
				var imageUrl = await _jobService.SaveImageAsync(createJobDTO.Image);
				job.ImageUrl = imageUrl;
			}
			catch (Exception ex)
			{
				return StatusCode(500, new BaseResponse("Error al guardar la imagen.", ex.Message, true));
			}
		}

		try
		{
			await _jobService.CreateJob(job);
			return Ok(new BaseResponse("Trabajo creado correctamente."));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new BaseResponse("Error al crear el trabajo.", ex.Message, true));
		}
	}

	[Authorize]
	[HttpDelete("{jobId}")]
	public async Task<IActionResult> DeleteJob(int jobId)
	{
		var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		var profile = await _profileService.GetProfileByUser(userId);

		if (profile == null)
		{
			return NotFound(new BaseResponse("Perfil no encontrado."));
		}

		var job = await _jobService.GetJobByIdAsync(jobId);

		if (job == null)
		{
			return NotFound(new BaseResponse("Trabajo no encontrado."));
		}

		if (job.ProfileId != profile.Id)
		{
			return BadRequest(new BaseResponse("No tienes permiso para eliminar este trabajo."));
		}

		try
		{
			await _jobService.DeleteJobAsync(jobId);
			return Ok(new BaseResponse("Trabajo eliminado correctamente."));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new BaseResponse("Error al eliminar el trabajo.", ex.Message, true));
		}
	}

	//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

	[HttpGet]
	public async Task<IActionResult> GetAllJobs()
	{
		try
		{
			var jobs = await _jobService.GetAllJobs();
			if (jobs == null || !jobs.Any())
			{
				return NotFound(new BaseResponse("No se encontraron trabajos."));
			}

			return Ok(new BaseResponse(jobs));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new BaseResponse($"Error interno del servidor: {ex.Message}"));
		}
	}

	[HttpGet("{jobId}")]
	public async Task<IActionResult> GetJobById(int jobId)
	{
		try
		{
			var job = await _jobService.GetJobByIdAsync(jobId);
			if (job == null)
			{
				return NotFound(new BaseResponse("Trabajo no encontrado."));
			}

			return Ok(new BaseResponse(job));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new BaseResponse($"Error interno del servidor: {ex.Message}"));
		}
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetJobsByUserId(int userId)
	{
		try
		{
			var jobs = await _jobService.GetJobsByUserId(userId);
			if (jobs == null || !jobs.Any())
			{
				return NotFound(new BaseResponse("No se encontraron trabajos para este usuario."));
			}

			return Ok(new BaseResponse(jobs));
		}
		catch (Exception ex)
		{
			return StatusCode(500, new BaseResponse($"Error interno del servidor: {ex.Message}"));
		}
	}
}
