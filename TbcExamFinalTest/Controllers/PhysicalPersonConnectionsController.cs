using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataContract.Models;
using TbcExamFinalTest.Models.Paging;
using DAL;

namespace TbcExamFinalTest.Controllers
{
    public class PhysicalPersonConnectionsController : Controller
    {
        private static readonly int PageSize = 3;
        private readonly UnitOfWork _unitOfWork;

        public PhysicalPersonConnectionsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? pageNumber)
        {
            return View(PaginatedList<PhysicalPersonConnection>.Create(_unitOfWork.PhysicalPersonConnectionRepository.Get(includeProperties: "PhysicalPerson,ConnectedPhysicalPerson,PhysicalPersonConnectionType"), pageNumber ?? 1, PageSize));
        }

        public IActionResult Create(long? id)
        {
            var physicalPersons = _unitOfWork.PhysicalPersonRepository.Get();
            if (!id.HasValue)
            {
                ViewData["PhysicalPersonId"] = new SelectList(physicalPersons, "Id", "LastName");
            }
            else
            {
                ViewData["PhysicalPersonId"] = new SelectList(physicalPersons.Where(x => x.Id == id), "Id", "LastName");
            }
            ViewData["ConnectedPhysicalPersonId"] = new SelectList(physicalPersons, "Id", "LastName");
            ViewData["PhysicalPersonConnectionTypeId"] = new SelectList(_unitOfWork.PhysicalPersonConnectionTypeRepository.Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PhysicalPersonId,ConnectedPhysicalPersonId,PhysicalPersonConnectionTypeId")] PhysicalPersonConnection physicalPersonConnection)
        {
            _unitOfWork.PhysicalPersonConnectionRepository.Insert(physicalPersonConnection);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));            
        }

        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.PhysicalPersonConnectionRepository.Delete(id.Value);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
