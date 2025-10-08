using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces; 
using Farmacheck.Application.Models.Sprints;
using Farmacheck.Application.Models.Tasks;
using Farmacheck.Infrastructure.Services;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading;

namespace Farmacheck.Controllers
{
    public class SprintController : Controller
    {
        public static List<SprintViewModel> sprints = new List<SprintViewModel>();
        public static List<UsuarioPorRolViewModel> usersByRole = new List<UsuarioPorRolViewModel>();
        public static List<OrigenDeTareaViewModel> origenesDeTarea = new List<OrigenDeTareaViewModel>();

        private readonly ISprintApiClient sprintApiClient;
        private readonly ITaskApiClient taskApiClient;
        private readonly IBusinessUnitApiClient businessUnitApiClient;
        private readonly ITaskPriorityApiClient priorityApiClient;
        private readonly ITaskOriginApiClient originApiClient;
        private readonly ICategoryApiClient categoryApiClient;
        private readonly IUserByRoleApiClient userByRoleApiClient;
        private readonly IRoleApiClient roleApiClient;
        private readonly IMapper _mapper;

        public SprintController(
            ISprintApiClient _sprintApiClient,
            ITaskApiClient _taskApiClient,
            IBusinessUnitApiClient _businessUnitApiClient,
            ITaskPriorityApiClient _priorityApiClient,
            ITaskOriginApiClient _originApiClient,
            ICategoryApiClient _categoryApiClient,
            IRoleApiClient _roleApiClient,
            IUserByRoleApiClient _userByRoleApiClient,
            IMapper mapper)
        {
            sprintApiClient = _sprintApiClient;
            taskApiClient = _taskApiClient;
            priorityApiClient = _priorityApiClient;
            originApiClient = _originApiClient;
            businessUnitApiClient = _businessUnitApiClient;
            categoryApiClient = _categoryApiClient;
            userByRoleApiClient = _userByRoleApiClient;
            roleApiClient = _roleApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            sprints = await ObtenerSprints();
            origenesDeTarea = await ObtenerOrigenesDeTarea();

            return View(sprints);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerSprintPorId(int id)
        {
            var sprintResponse = await sprintApiClient.GetSprintAsync(id);
            if (sprintResponse == null)
                return Json(new { success = false, error = "No encontrado" });

            var sprintDto = _mapper.Map<SprintDto>(sprintResponse);
            var sprint = _mapper.Map<SprintViewModel>(sprintDto);
            return Json(new { success = true, data = sprint });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTareaPorId(int sprintId, Guid id)
        {
            var tareaResponse = await taskApiClient.GetTaskAsync(sprintId, id);
            if (tareaResponse == null)
                return Json(new { success = false, error = "No encontrado" });

            var tareaDto = _mapper.Map<TareaDto>(tareaResponse);
            var tarea = _mapper.Map<TareaViewModel>(tareaDto);
            return Json(new { success = true, data = tarea });
        }

        [HttpPost]
        public async Task<IActionResult> GuardarSprint([FromBody] SprintViewModel model) 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Titulo))
                    return Json(new { success = false, error = "El título es obligatorio." });

                model.VigenciaDel = DateTime.Parse(model.VigenciaInicio);
                model.VigenciaAl = DateTime.Parse(model.VigenciaFin);

                if (model.Id == 0)
                {
                    if (model.Tareas is not null && model.Tareas.Any())
                    {
                        var origenPorSprintId = origenesDeTarea.SingleOrDefault(o => o.Nombre == "SPRINT")?.Id;
                        foreach (var tarea in model.Tareas)
                        {
                            tarea.VigenteDel = DateTime.Parse(tarea.VigenciaInicio);
                            tarea.VenceEl = DateTime.Parse(tarea.VigenciaFin);
                            tarea.OrigenId = origenPorSprintId ?? 0;
                        }
                    }

                    var sprintRequest = _mapper.Map<SprintRequest>(model);
                    var id = await sprintApiClient.CreateAsync(sprintRequest);
                    return Json(new { success = true, id, message = "Sprint Creado" });
                }

                var updateSprintRequest = _mapper.Map<UpdateSprintRequest>(model);
                var updated = await sprintApiClient.UpdateAsync(updateSprintRequest);
                
                if (updated)
                    return Json(new { success = true, id = model.Id });

                return Json(new { success = true, message = "Sprint Actualizado" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GuardarTarea([FromForm] TareaViewModel tarea)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tarea.Titulo))
                    return Json(new { success = false, error = "El título es obligatorio." });

                if (tarea.Id is null || tarea.Id == Guid.Empty)
                {
                    var origenPorSprintId = origenesDeTarea.SingleOrDefault(o => o.Nombre == "SPRINT")?.Id;
                    tarea.OrigenId = origenPorSprintId;
                    var tareaRequest = _mapper.Map<TaskRequest>(tarea);
                    var id = await taskApiClient.CreateAsync(tareaRequest);
                    return Json(new { success = true });
                }

                var updateTareaRequest = _mapper.Map<UpdateTaskRequest>(tarea);
                var updated = await taskApiClient.UpdateAsync(updateTareaRequest);

                if (updated)
                    return Json(new { success = true, id = tarea.Id });

                return Json(new { success = false, error = "No se pudo actualizar" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EliminarSprint(int id)
        {
            await sprintApiClient.DeleteAsync(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> EliminarTarea(int sprintId, Guid id)
        {
            var request = new RemoveTaskRequest() { Id = id, SprintId = sprintId };
            await taskApiClient.DeleteAsync(request);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> DescargarReporteSprints()
        {
            var base64 = await sprintApiClient.GetReport();
            var bytes = Convert.FromBase64String(base64);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteSprints.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ConfigurarSprints(int? id)
        {
            SprintViewModel sprint = new SprintViewModel();

            if (id.HasValue)
            {
                
                var sprintResponse = await sprintApiClient.GetSprintAsync(id);
                var sprintDto = _mapper.Map<SprintDto>(sprintResponse);
                sprint = _mapper.Map<SprintViewModel>(sprintDto);

                if (sprint != null)
                {
                    ViewData["Title"] = "Editar Sprint";
                    return View(sprint);
                }
            }

            ViewData["Title"] = "Crear Sprint";
            return View(sprint);
        }


        [HttpGet]
        public async Task<IActionResult> ConfigurarTareas(int id)
        {
            ViewBag.SprintId = id;
            var tasks = await ObtenerTareas(id);

            return View(tasks);
        }

        private async Task<List<SprintViewModel>> ObtenerSprints()
        {
            var sprintsResponse = await sprintApiClient.GetSprintsAsync();
            var sprintsDto = _mapper.Map<List<SprintDto>>(sprintsResponse);
            var sprints = _mapper.Map<List<SprintViewModel>>(sprintsDto);

            return sprints;
        }

        private async Task<List<TareaViewModel>> ObtenerTareas(int sprintId)
        {
            var tareasResponse = await taskApiClient.GetTasksAsync(sprintId);
            var tareasDto = _mapper.Map<List<TareaDto>>(tareasResponse);
            var tareas = _mapper.Map<List<TareaViewModel>>(tareasDto);

            return tareas;
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerUnidadesNegocio()
        {
            var apiData = await businessUnitApiClient.GetBusinessUnitsAsync();
            var dtos = _mapper.Map<List<BusinessUnitDto>>(apiData);
            var unidades = _mapper.Map<List<UnidadDeNegocio>>(dtos);
            return Json(new { success = true, data = unidades });
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerPrioridadesDeTarea()
        {
            var apiData = await priorityApiClient.GetTaskPrioritiesAsync();
            var dtos = _mapper.Map<List<PrioridadDeTareaDto>>(apiData);
            var prioridades = _mapper.Map<List<PrioridadDeTareaViewModel>>(dtos);
            return Json(new { success = true, data = prioridades });
        }

        [HttpGet]
        public async Task<List<OrigenDeTareaViewModel>> ObtenerOrigenesDeTarea()
        {
            var apiData = await originApiClient.GetTaskOriginsAsync();
            var dtos = _mapper.Map<List<OrigenDeTareaDto>>(apiData);
            var origenes = _mapper.Map<List<OrigenDeTareaViewModel>>(dtos);

            return origenes;
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerCategorias()
        {
            var apiData = await categoryApiClient.GetCategoriesAsync();
            var dtos = _mapper.Map<List<CategoriaDto>>(apiData);
            var categorias = _mapper.Map<List<CategoriaViewModel>>(dtos);
            return Json(new { success = true, data = categorias });
        }

        [HttpGet]
        public async Task<JsonResult> ListarRoles()
        {
            var apiData = await roleApiClient.GetRolesAsync();
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = dtos.Select(r => new { id = r.Id, nombre = r.Nombre });

            var apiDataUsers = await userByRoleApiClient.GetUsersByRolesAsync();
            var usersDtos = _mapper.Map<List<UsuarioPorRolDto>>(apiDataUsers);
            usersByRole = _mapper.Map<List<UsuarioPorRolViewModel>>(usersDtos);

            return Json(new { success = true, data = roles });
        }

        [HttpGet]
        public JsonResult ObtenerUsuariosPorRol(int rolId)
        {
            var users = usersByRole.Where(u => u.RolId == rolId).ToList();
            users = users.Any() ? users : new List<UsuarioPorRolViewModel>();
            return Json(new { success = true, data = users });
        }
    }
}
