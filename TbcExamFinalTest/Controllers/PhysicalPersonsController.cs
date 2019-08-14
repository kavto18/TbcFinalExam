using Microsoft.AspNetCore.Mvc;
using TbcExamFinalTest.ImageWriter.Handler;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataContract.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using TbcExamFinalTest.Models.Paging;
using System.Linq;
using System.Collections.Generic;
using TbcExamFinalTest.Models;
using DAL.Interface;
using DAL;

namespace TbcExamFinalTest.Controllers
{
    public class PhysicalPersonsController : Controller
    {
        private static readonly int PageSize = 3;
        private readonly IPhysicalPersonRepository _physicalPersonRepository;
        private readonly IIMageHandler _imageHandler;
        private readonly UnitOfWork _unitOfWork;

        public PhysicalPersonsController(IPhysicalPersonRepository physicalPersonRepository,
                                         IIMageHandler imageHandler,
                                         UnitOfWork unitOfWork)
        {
            _physicalPersonRepository = physicalPersonRepository;
            _imageHandler = imageHandler;
            _unitOfWork = unitOfWork;
        }

        // GET: PhysicalPersons
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            var data = _unitOfWork.PhysicalPersonRepository.Get();
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            var tbcFinalExamContext = _physicalPersonRepository.GetPhysicalPeople(searchString);
            return View(await PaginatedList<PhysicalPerson>.CreateAsync(tbcFinalExamContext, pageNumber ?? 1, PageSize));
        }

        // GET: PhysicalPersons/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var physicalPerson = _physicalPersonRepository.GetPhysicalPersonDetailById(id.Value).Result;
            if (physicalPerson == null)
            {
                return NotFound();
            }
            physicalPerson.ConnectionsReport = new List<PhysicalPersonConnetionReportContract>();
            foreach (var group in _unitOfWork.PhysicalPersonConnectionRepository.Get(x => x.PhysicalPersonId == id, includeProperties: "PhysicalPersonConnectionType").GroupBy(x => x.PhysicalPersonConnectionType.Name))
            {
                physicalPerson.ConnectionsReport.Add(new PhysicalPersonConnetionReportContract
                {
                    ConnectionTypeName = group.Key,
                    Count = group.Count()
                });
            }
            return View(physicalPerson);
        }

        // GET: PhysicalPersons/Create
        public IActionResult Create()
        {
            FillViewBag();
            return View();
        }

        // POST: PhysicalPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,LastName,GenderId,PersonalId,BirthDate,CityId,TelephoneTypeId,TelephoneNumber")] PhysicalPerson physicalPerson)
        {
            _unitOfWork.PhysicalPersonRepository.Insert(physicalPerson);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var physicalPerson = _unitOfWork.PhysicalPersonRepository.GetByID(id);
            if (physicalPerson == null)
            {
                return NotFound();
            }
            FillViewBag(physicalPerson.CityId, physicalPerson.GenderId, physicalPerson.TelephoneTypeId);
            return View(physicalPerson);
        }

        // POST: PhysicalPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Name,LastName,GenderId,PersonalId,BirthDate,CityId,TelephoneTypeId,TelephoneNumber")] PhysicalPerson physicalPerson)
        {
            if (id != physicalPerson.Id)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.PhysicalPersonRepository.Update(physicalPerson);
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PhysicalPersons/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var physicalPerson = _unitOfWork.PhysicalPersonRepository.GetByID(id.Value);
            if (physicalPerson == null)
            {
                return NotFound();
            }

            return View(physicalPerson);
        }

        // POST: PhysicalPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var physicalPerson = _unitOfWork.PhysicalPersonRepository.GetByID(id);
            _imageHandler.DeleteImage(physicalPerson.Photo);
            _unitOfWork.PhysicalPersonConnectionRepository.DeleteRange(_unitOfWork.PhysicalPersonConnectionRepository.Get(x => x.PhysicalPersonId == id || x.ConnectedPhysicalPersonId == id));
            _unitOfWork.PhysicalPersonRepository.Delete(physicalPerson);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProcessPicture(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var physicalPerson = _unitOfWork.PhysicalPersonRepository.GetByID(id.Value);
            if (physicalPerson == null)
            {
                return NotFound();
            }
            return View(new PhysicalPersonPictureViewModel
            {
                Id = physicalPerson.Id,
                LastName = physicalPerson.LastName,
                Name = physicalPerson.Name,
                ImagePath = physicalPerson.Photo
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPicture(long id, IFormFile file)
        {
            var physicalPerson = _unitOfWork.PhysicalPersonRepository.GetByID(id);
            if (physicalPerson == null)
            {
                return NotFound();
            }
            var fileName = _imageHandler.UploadImage(file, id);
            physicalPerson.Photo = fileName;
            _unitOfWork.PhysicalPersonRepository.Update(physicalPerson);
            _unitOfWork.Save();
            return RedirectToAction(nameof(ProcessPicture));
        }

        public IActionResult ProcessConnections(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var physicalPerson = _unitOfWork.PhysicalPersonRepository.GetByID(id.Value);
            if (physicalPerson == null)
            {
                return NotFound();
            }
            var view = new ProcessConnectionViewModel
            {
                PhysicalPerson = physicalPerson,
                Connections = _unitOfWork.PhysicalPersonConnectionRepository.Get(filter: x => x.PhysicalPersonId == id, includeProperties: "PhysicalPerson,ConnectedPhysicalPerson,PhysicalPersonConnectionType"),
            };
            return View(view);
        }

        private void FillViewBag(int? cityId = null, int? genderId = null, int? telephoneTypeId = null)
        {
            ViewData["CityId"] = GetSelectedCities(cityId);
            ViewData["GenderId"] = GetSelectedGenders(genderId);
            ViewData["TelephoneTypeId"] = GetSelectedTelephoneTypes();
        }

        private SelectList GetSelectedCities(int? id = null)
        {
            if (!id.HasValue)
            {
                return new SelectList(_unitOfWork.CityRepository.Get(), "Id", "Name");
            }
            return new SelectList(_unitOfWork.CityRepository.Get(), "Id", "Name", id);
        }

        private SelectList GetSelectedGenders(int? id = null)
        {
            if (!id.HasValue)
            {
                return new SelectList(_unitOfWork.GenderRepository.Get(), "Id", "Name");
            }
            return new SelectList(_unitOfWork.GenderRepository.Get(), "Id", "Name", id);
        }

        private SelectList GetSelectedTelephoneTypes(int? id = null)
        {
            if (!id.HasValue)
            {
                return new SelectList(_unitOfWork.TelephoneTypeRepository.Get(), "Id", "Name");
            }
            return new SelectList(_unitOfWork.TelephoneTypeRepository.Get(), "Id", "Name", id);
        }
    }
}
