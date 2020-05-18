using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW01.Models;
using Microsoft.Data.SqlClient;

namespace HW01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosouniversityContext _context;

        public DepartmentsController(ContosouniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public ActionResult PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            //_context.Entry(department).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();

                #region Use stored procedure
                byte[] _rowVersion = _context.Department.Find(id).RowVersion;
                SqlParameter departmentID = new SqlParameter("@DepartmentID", department.DepartmentId);
                SqlParameter name = new SqlParameter("@Name", department.Name);
                SqlParameter budget = new SqlParameter("@Budget", department.Budget);
                SqlParameter startDate = new SqlParameter("@StartDate", department.StartDate);
                SqlParameter instructorID = new SqlParameter("@InstructorID", department.InstructorId);
                SqlParameter rowVersion = new SqlParameter("@RowVersion_Original", _rowVersion);
                _context.Database.ExecuteSqlRaw("execute Department_Update @DepartmentID,@Name,@Budget,@StartDate,@InstructorID,@RowVersion_Original",
                    departmentID, name, budget, startDate, instructorID, rowVersion);
                #endregion
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
            //_context.Department.Add(department);
            //await _context.SaveChangesAsync();

            #region Use stored procedure
            SqlParameter name = new SqlParameter("@Name", department.Name);
            SqlParameter budget = new SqlParameter("@Budget", department.Budget);
            SqlParameter startDate = new SqlParameter("@StartDate", department.StartDate);
            SqlParameter instructorID = new SqlParameter("@InstructorID", department.InstructorId);
            department.DepartmentId = _context.Department.FromSqlRaw("execute Department_Insert @Name,@Budget,@StartDate,@InstructorID",
                name, budget, startDate, instructorID).Select(c => c.DepartmentId).ToList().First();
            #endregion

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartment(int id)
        {
            //var department = await _context.Department.FindAsync(id);
            var department = _context.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            //_context.Department.Remove(department);
            //await _context.SaveChangesAsync();

            #region Use stored procedure
            SqlParameter departmentID = new SqlParameter("@DepartmentID", department.DepartmentId);
            SqlParameter rowVersion = new SqlParameter("@RowVersion_Original", department.RowVersion);
            _context.Database.ExecuteSqlRaw("execute Department_Delete @DepartmentID,@RowVersion_Original", departmentID, rowVersion);
            #endregion

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }


        [HttpGet("GetDepartmentCourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCount()
        {
            return await _context.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM [dbo].[vwDepartmentCourseCount]").ToListAsync();
        }
    }
}
